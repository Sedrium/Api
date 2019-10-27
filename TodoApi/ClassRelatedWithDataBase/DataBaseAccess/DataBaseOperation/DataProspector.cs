using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TodoApi.DataBaseOperation
{
    public class DataProspector<TypeOfClass> where TypeOfClass : new()
    {
        private List<PropertyInfo> allEntityProperties = typeof(TypeOfClass).GetProperties().ToList();
        private SQLiteDataReader reader = null;
        private List<TypeOfClass> ListOfEntity = new List<TypeOfClass>();

        public List<TypeOfClass> DownloadData()
        {
            var connection = DBConnection.GetConnection();
            connection.ExecuteQuery(CreateSQL(), MapDataToObject);
            return ListOfEntity;
        }

        private string CreateSQL()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            int loopCount = 0;
            foreach (var property in allEntityProperties)
            {
                loopCount++;
                sql.Append(string.Format(@"{0} ""{1}"" ", property.Name, property.Name));
                if (loopCount != allEntityProperties.Count)
                    sql.Append(",");
            }
            sql.Append(string.Format("FROM {0}", typeof(TypeOfClass).Name));
            return sql.ToString();
        }

        private List<TypeOfClass> MapDataToObject(SQLiteDataReader reader)
        {
            while (reader.Read())
            {
                TypeOfClass newEntity = new TypeOfClass(); ;
                foreach (var property in allEntityProperties)
                {
                    var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                    property.SetValue(newEntity, value);
                }
                ListOfEntity.Add(newEntity);
            }
            return ListOfEntity;
        }
    }
}