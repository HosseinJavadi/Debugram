using Debugram.Common.Enums;
using Debugram.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace Debugram.Service.ModelValidation
{
    public class ApiModelValidation : ActionFilterAttribute, IFilterMetadata
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)       
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                var responseObj = new
                {
                    IsSuccess = false,
                    Message = ResultApiStatusCode.InValidParameters.ToDisplay(),
                    Errors = errors,
                    Status = ResultApiStatusCode.InValidParameters
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
