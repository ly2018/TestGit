using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static Vic.Core.Utils.Enum.AM_Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ruiec.WebUI.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<UploadController> _logger;

        public UploadController(IHostingEnvironment hostingEnvironment, ILogger<UploadController> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ContentResult UploadImgageNew(ImgPathEnum ImgPathEnum, bool IsFullPath = false)
        {
            string url = Vic.Core.Utils.Common.UploadImgageReturnPath(this.HttpContext, ImgPathEnum, IsFullPath);
            return Content(url);
        }

        /// <summary>
        /// XHEditor编辑器文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public JsonResult UploadEditImgage(ImgPathEnum ImgPathEnum, bool IsFullPath = false)
        {
            string url = Vic.Core.Utils.Common.UploadImgageReturnPath(this.HttpContext, ImgPathEnum, IsFullPath);
            return Json(new { err = "", msg = url });
        }

        // 1. We disable the form value model binding here to take control of handling potentially large files.
        // 2. Typically antiforgery tokens are sent in request body, but since we do not want to read the request body
        //    early, the tokens are made to be sent via headers. The antiforgery token filter first looks for tokens
        //    in the request header and then falls back to reading the body.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImgage(ImgPathEnum ImgPathEnum, bool IsFullPath = false)
        {
            var multipartBoundary = Request.GetMultipartBoundary();
            if (string.IsNullOrEmpty(multipartBoundary))
            {
                return BadRequest($"Expected a multipart request, but got '{Request.ContentType}'.");
            }

            // Used to accumulate all the form url encoded key value pairs in the request.
            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = null;

            var reader = new MultipartReader(multipartBoundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                // This will reparse the content disposition header
                // Create a FileMultipartSection using it's constructor to pass
                // in a cached disposition header
                var fileSection = section.AsFileSection();
                if (fileSection != null)
                {
                    var fileName = fileSection.FileName;

                    targetFilePath = Path.Combine(_hostingEnvironment.ContentRootPath, Guid.NewGuid().ToString());
                    using (var targetStream = System.IO.File.Create(targetFilePath))
                    {
                        await fileSection.FileStream.CopyToAsync(targetStream);

                        _logger.LogInformation($"Copied the uploaded file '{fileName}' to '{targetFilePath}'.");
                    }
                }
                else
                {
                    var formSection = section.AsFormDataSection();
                    if (formSection != null)
                    {
                        var name = formSection.Name;
                        var value = await formSection.GetValueAsync();

                        formAccumulator.Append(name, value);

                        if (formAccumulator.ValueCount > FormReader.DefaultValueCountLimit)
                        {
                            throw new InvalidDataException(
                                $"Form key count limit {FormReader.DefaultValueCountLimit} exceeded.");
                        }
                    }
                }

                // Drains any remaining section body that has not been consumed and
                // reads the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            return Json(targetFilePath);
        }
    }
}
