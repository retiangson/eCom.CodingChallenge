using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Buffers;

namespace eCom.CodingChallenge.Attributes
{
    public class CustomSerializationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext ctx)
        {
            if (ctx.Result is ObjectResult objectResult)
            {
                objectResult.Formatters.Add(new NewtonsoftJsonOutputFormatter(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver()
                    },
                    ctx.HttpContext.RequestServices.GetRequiredService<ArrayPool<char>>(),
                    ctx.HttpContext.RequestServices.GetRequiredService<IOptions<MvcOptions>>().Value));
            }
        }
    }
}