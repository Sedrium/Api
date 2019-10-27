﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DataBaseAccess;
using TodoApi.Dto;
using TodoApi.Model;

namespace TodoApi.Servies
{
    public class ProductService
    {
        public ProductService()
        {
            _dataAccess = new DataAccess<Product>();
        }


        private DataAccess<Product> _dataAccess;


        public List<ProductCreateInputModel> GetProductCollection()
        {
            var listOfProductDB = GetProductList();
            return MapListOfDataProductToModelProduct(listOfProductDB);
        }

        public ProductCreateInputModel GetProduct(Guid id)
        {
            List<Product> listOfProductDB = GetProductList();
            var foundItem = GetProductIfExist(id);
            return MapDataProductToModelProduct(foundItem);
        }

        public Guid SetProduct(ProductCreateInputModel model)
        {
            model.Id = Guid.NewGuid();
            Product modelProduct = MapModelProductToDataProduct(model);
            _dataAccess.Create(modelProduct);
            return model.Id;
        }

        public void EditProduct(ProductCreateInputModel model)
        {
            GetProductIfExist(model.Id);
            Product modelProduct = MapModelProductToDataProduct(model);
            _dataAccess.Update(modelProduct);
        }

        public void RemoveProduct(Guid id)
        {
            GetProductIfExist(id);
            _dataAccess.Delete(id.ToString());
        }


        private List<Product> GetProductList()
        {
            var listOfProductDB = _dataAccess.Read();
            if (listOfProductDB.Count == 0)
                throw new Exception("Error204Table is empty");
            return listOfProductDB;
        }

        private Product GetProductIfExist(Guid id)
        {
            var listOfProductDB = _dataAccess.Read();
            var foundProduct = listOfProductDB.Find(product => product.Id == id.ToString());
            if (foundProduct == null)
                throw new Exception("Error404There is no record");
            return foundProduct;
        }

        private List<ProductCreateInputModel> MapListOfDataProductToModelProduct(List<Product> dataProduct)
        {
            List<ProductCreateInputModel> ProductModelList = new List<ProductCreateInputModel>();
            foreach (var product in dataProduct)
            {
                ProductModelList.Add(MapDataProductToModelProduct(product));
            }
            return ProductModelList;
        }

        private ProductCreateInputModel MapDataProductToModelProduct(Product dataProduct)
        {
            return new ProductCreateInputModel() { Id = Guid.Parse(dataProduct.Id), Name = dataProduct.Name, Price = dataProduct.Price };
        }

        private Product MapModelProductToDataProduct(ProductCreateInputModel modelProduct)
        {
            return new Product() { Id = modelProduct.Id.ToString(), Name = modelProduct.Name, Price = modelProduct.Price };
        }
    }
}
