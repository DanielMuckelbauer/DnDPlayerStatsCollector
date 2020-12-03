using Microsoft.AspNetCore.Mvc;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.PresentationLayer.Controllers
{
    public class Roll20ControllerBase : ControllerBase
    {
        protected ActionResult CreateResponse<T>(ResponseWrapper<T> responseWrapper)
        {
            if (!responseWrapper.HasError)
                return Ok(responseWrapper.Response);
            HttpContext.Response.StatusCode = responseWrapper.StatusCode;
            return new JsonResult(new { Error = responseWrapper.ErrorMessage });
        }
    }
}
