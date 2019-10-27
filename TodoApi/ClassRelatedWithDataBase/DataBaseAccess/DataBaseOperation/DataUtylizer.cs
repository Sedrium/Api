using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApi.DataBaseOperation
{
    public class DataUtylizer <TypeOfClass> where TypeOfClass : new()
    {
        private string indexOfTheEntityToDelete;
        public void DeleteData(string indexOfTheEntityToDelete)
        {
            this.indexOfTheEntityToDelete = indexOfTheEntityToDelete;
            var connection = DBConnection.GetConnection();
            connection.ExecuteQuery(CreateSQL());
        }

        private string CreateSQL()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM ");
            sql.Append(string.Format("{0}", typeof(TypeOfClass).Name));
            sql.Append(" WHERE ");
            var idProperty = typeof(TypeOfClass).GetProperties().ToList().Find(property => property.Name.ToLower().Equals("id"));
            if(idProperty.PropertyType.Name.Equals("String"))
            sql.Append(string.Format("{0} = '{1}'", idProperty.Name, indexOfTheEntityToDelete));
            else
            sql.Append(string.Format("{0} = {1}", idProperty.Name, indexOfTheEntityToDelete));
            return sql.ToString();
        }
    }
}
