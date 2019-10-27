using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TodoApi.DataBaseOperation
{
    public class DataScribe<TypeOfClass> where TypeOfClass : new()
    {
        private List<PropertyInfo> allEntityProperties = typeof(TypeOfClass).GetProperties().ToList();
        private TypeOfClass entityToSave;

        public void SaveDate(TypeOfClass entityToSave)
        {
            this.entityToSave = entityToSave;
            var connection = DBConnection.GetConnection();
            connection.ExecuteQuery(CreateSQL());
        }

        private string CreateSQL()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append(string.Format("{0}", typeof(TypeOfClass).Name));
            sql.Append(" (");
            int loopIndex = 0;
            foreach (var property in allEntityProperties)
            {
                ++loopIndex;
                sql.Append(string.Format("{0}", property.Name));
                if (loopIndex != allEntityProperties.Count)
                    sql.Append(", ");
            }
            sql.Append(") VALUES(");
            loopIndex = 0;
            foreach (var property in allEntityProperties)
            {
                loopIndex++;
                var value = property.GetValue(entityToSave);
                if (value.GetType().Name != "String")
                    value = value.ToString().Replace(',', '.');
                    sql.Append(string.Format("'{0}'", value));
                if (loopIndex != allEntityProperties.Count)
                    sql.Append(", ");
            }
            sql.Append(")");
            return sql.ToString();
        }
    }
}
