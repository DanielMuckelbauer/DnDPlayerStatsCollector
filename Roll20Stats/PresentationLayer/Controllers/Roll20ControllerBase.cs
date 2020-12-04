using Microsoft.AspNetCore.Mvc;
using Roll20Stats.PresentationLayer.DataTransferObjects;

namespace Roll20Stats.PresentationLayer.Controllers
{
    public class Roll20ControllerBase : ControllerBase
    {
        protected ActionResult CreateResponse<T>(ResponseWithMetaData<T> responseWithMetaData)
        {
            if (!responseWithMetaData.HasError)
                return Ok(responseWithMetaData.Response);
            HttpContext.Response.StatusCode = responseWithMetaData.StatusCode;
            return new JsonResult(new { Error = responseWithMetaData.ErrorMessage });
        }
    }
}
