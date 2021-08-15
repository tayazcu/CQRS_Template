using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.Core.Resources.Resources;
using Project.Framework;
using Project.Framework.Resources;
using Project.Framework.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace Project.Endpoints.WebFramework.Attributes
{
    //public class ValidateModelStateAttribute : ActionFilterAttribute
    //{
    //    private readonly IResourceManager _resourceManager;
    //    public ValidateModelStateAttribute(IResourceManager resourceManager)
    //    {
    //        _resourceManager = resourceManager;
    //    }
    //    public override void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        if (!context.ModelState.IsValid)
    //        {
    //            ModelStateDictionary modelStateEntries = context.ModelState;
    //            List<string> errors = new List<string>();
    //            foreach (KeyValuePair<string, ModelStateEntry> item in modelStateEntries)
    //            {
    //                foreach (ModelError error in item.Value.Errors)
    //                {
    //                    errors.Add($"{_resourceManager.GetName(item.Key)} {error.ErrorMessage}");
    //                }
    //            }
    //            ApiResult resultObject = new ApiResult(false, StatusCode.BadRequest, _resourceManager.GetName(SharedResource.BadRequest), errors);
    //            context.Result = new JsonResult(resultObject);
    //        }
    //    }
    //}
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                var modelStateEntries = context.ModelState.Values;
                foreach (var item in modelStateEntries)
                {
                    foreach (var error in item.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                ApiResult resultObject = new ApiResult(false, StatusCode.BadRequest, SharedResource.BadRequest, errors);
                context.Result = new JsonResult(resultObject);
            }
        }
    }
}
