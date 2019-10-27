using System;
using System.Collections.Generic;
using TodoApi.Model;

namespace TodoApi.Servies
{
    public interface IProductService
    {
        List<ProductCreateInputModel> GetProductCollection();
        ProductCreateInputModel GetProduct(Guid id);
        Guid SetProduct(ProductCreateInputModel model);
        void EditProduct(ProductCreateInputModel model);
        void RemoveProduct(Guid id);
    }
}