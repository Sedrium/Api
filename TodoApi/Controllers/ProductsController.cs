﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DataBaseAccess;
using TodoApi.Dto;
using System.Net;
using Microsoft.AspNetCore.Http;
using TodoApi.Validators;
using TodoApi.Model;
using TodoApi.Servies;
using FluentValidation.Results;
using System.Text;
using TodoApi.Tools;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController(IProductService _IProductService)
        {
            this._IProductService = _IProductService;
        }
        private readonly IProductService _IProductService;
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ProductCreateInputModel>> Get()
        {
            List<ProductCreateInputModel> collectionResult = null;
            try
            {
                collectionResult = _IProductService.GetProductCollection();
            }
            catch (Exception e)
            {
                return SendRightStatus(e);
            }
            return Ok(collectionResult);
        }

        // GET api/Products/5
        [HttpGet("{id}")]
        public ActionResult<ProductCreateInputModel> Get(Guid id)
        {
            ProductCreateInputModel valueResult = null;
            try
            {
                ProductControllerHelperMethods.ValidIdProperty(id);
                valueResult = _IProductService.GetProduct(id);
            }
            catch (Exception e)
            {
                return SendRightStatus(e);
            }

            return Ok(valueResult);
        }

        // POST api/Products
        [HttpPost()]
        public ActionResult<Guid> Post([FromBody] ProductCreateInputModel model)
        {
            Guid idOfCreatedObject = new Guid();
            try
            {
                ProductControllerHelperMethods.ValidModel(model, true);
                idOfCreatedObject = _IProductService.SetProduct(model);
            }
            catch (Exception e)
            {
                return SendRightStatus(e);
            }
            return StatusCode(StatusCodes.Status201Created, idOfCreatedObject);
        }

        // PUT api/Products
        [HttpPut()]
        public IActionResult Put([FromBody] ProductCreateInputModel model)
        {
            try
            {
                ProductControllerHelperMethods.ValidModel(model, false);
                _IProductService.EditProduct(model);
            }
            catch (Exception e)
            {
                return SendRightStatus(e);
            }
            return Ok();
        }

        // DELETE api/Products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                ProductControllerHelperMethods.ValidIdProperty(id);
                _IProductService.RemoveProduct(id);
            }
            catch (Exception e)
            {
                return SendRightStatus(e);
            }
            return NoContent();
        }

        private ActionResult SendRightStatus(Exception e)
        {
            try
            {
                int statusCode = int.Parse(e.Message.Substring(5, 3));
                return StatusCode(statusCode, e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
