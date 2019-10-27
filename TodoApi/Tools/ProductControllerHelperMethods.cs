using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Controllers;
using TodoApi.Model;
using TodoApi.Validators;

namespace TodoApi.Tools
{
    public static class ProductControllerHelperMethods
    {
        public static void ValidModel(ProductCreateInputModel model, bool idShouldBeNull)
        {
            var validateErrors = (new ProductCreateInputModelValidator(idShouldBeNull)).Validate(model);
            StringBuilder errorMessage = new StringBuilder();
            if (!validateErrors.IsValid)
            {
                foreach (var error in validateErrors.Errors)
                {
                    errorMessage.Append(error.ErrorMessage + "|");
                }
                throw new Exception("Error400Passed model is not valid " + errorMessage);
            }
        }

        public static void ValidIdProperty(Guid id)
        {
            if (id == null || id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                throw new Exception("Error400Passed id is not initialized or is empty");
        }
    }
}
