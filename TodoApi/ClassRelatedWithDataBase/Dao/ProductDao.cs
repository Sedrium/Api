using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DataBaseAccess;
using TodoApi.Dto;

namespace TodoApi.Dao
{
    public class ProductDao
    {
        public ProductDao()
        {
            _dataAccess = new DataAccess<Product>();
        }
        private DataAccess<Product> _dataAccess;

        internal IEnumerable<Product> GetProductList()
        {
            return _dataAccess.Read();
        }
    }
}
