using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        public class UploadCustomerImageModel
        {
            public string Description { get; set; }
            public string ImageData { get; set; }
        }
        [HttpPost("get-image")]
        public FileContentResult UploadCustomerImage([FromBody] UploadCustomerImageModel model)
        {
            //Depending on if you want the byte array or a memory stream, you can use the below. 
            var imageDataByteArray = Convert.FromBase64String(model.ImageData);

            //When creating a stream, you need to reset the position, without it you will see that you always write files with a 0 byte length. 
            var imageDataStream = new MemoryStream(imageDataByteArray);
            imageDataStream.Position = 0;

            //Go and do something with the actual data.
            //_customerImageService.Upload([...])

            //For the purpose of the demo, we return a file so we can ensure it was uploaded correctly. 
            //But otherwise you can just return a 204 etc. 
            return File(imageDataByteArray, "image/png");
        }
    }
}