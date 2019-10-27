using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TodoApi.DataBaseOperation
{
    public class DataChanger<TypeOfClass> where TypeOfClass : new()
    {
        private List<PropertyInfo> allEntityProperties = typeof(TypeOfClass).GetProperties().ToList();
        private TypeOfClass entityToUpdate;

        public void UpdateData(TypeOfClass entityToUpdate)
        {
            this.entityToUpdate = entityToUpdate;
            var connection = DBConnection.GetConnection();
            connection.ExecuteQuery(CreateSQL());
        }

        private string CreateSQL()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append(string.Format("{0} ", typeof(TypeOfClass).Name));
            sql.Append("SET ");
            bool isFirst = true;
            foreach (var property in allEntityProperties)
            {
                if (property.Name.ToLower().Equals("id"))
                    continue;
                if (!isFirst)
                    sql.Append(", ");
                var value = property.GetValue(entityToUpdate);
                if (!value.GetType().Name.Equals("String"))
                    value = value.ToString().Replace(',', '.');
                sql.Append(string.Format("{0} = '{1}'", property.Name, value));
                isFirst = false;
            }
            sql.Append(" WHERE ");
            var idProperty = allEntityProperties.Find(property => property.Name.ToLower().Equals("id"));
            if (idProperty.PropertyType.Name.Equals("String"))
                sql.Append(string.Format("{0} = '{1}'", idProperty.Name, idProperty.GetValue(entityToUpdate)));
            else
                sql.Append(string.Format("{0} = {1}", idProperty.Name, idProperty.GetValue(entityToUpdate)));
            return sql.ToString();
        }
    }
}
