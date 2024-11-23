using HHADotNetCore.MiniKpayDomain.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;

namespace HHADotNetCore.MiniKpayRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult Execute(object model)
        {
            JObject jObj = JObject.Parse(JsonConvert.SerializeObject(model));
            if (jObj.ContainsKey("Response"))
            {
                BaseResponseModel baseResponseModel = 
                    JsonConvert.DeserializeObject<BaseResponseModel>(
                        jObj["responseModel"]!.ToString()!)!;

                if (baseResponseModel.RespType == EnumRespType.ValidationError)
                    return BadRequest(model);

                if (baseResponseModel.RespType == EnumRespType.SystemError)
                    return StatusCode(500, model);

                return Ok(model);
            }

            return StatusCode(500, "Invalid Response Model. Please add BaseResponseModel to your ResponseModel.");
        }
    }
}
