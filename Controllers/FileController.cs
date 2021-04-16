using IPhoto.Common.ApiResult;
using IPhoto.Models;
using IPhoto.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace IPhoto.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _iFileService;
        private readonly IUserService _iUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileController(IFileService fileService, 
            IUserService userService,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, 
            IWebHostEnvironment hostingEnvironment)
        {
            _iFileService = fileService;
            _iUserService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
        }

        public ApiResult Upload(IFormFile fileinput)
        {
            if(fileinput == null)
            {
                return ApiResultHelper.Error("请选择文件后上传！");
            }
            var filePath = "/Photos/" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + fileinput.FileName;
            fileinput.CopyTo(new FileStream(_hostingEnvironment.WebRootPath + filePath, FileMode.Create));

            Models.File file = new();
            file.Name = filePath[8..];
            file.ContentType = fileinput.ContentType;
            file.Path = filePath;
            file.UserId = _userManager.GetUserId(User);

            if (_iFileService.InsertAsync(file).Result)
            {
                return ApiResultHelper.Success();
            }
            return ApiResultHelper.Error("文件上传失败！");
        }

        public ApiResult UploadHeadPhoto(IFormFile fileinput)
        {
            if (fileinput == null)
            {
                return ApiResultHelper.Error("请选择文件后上传！");
            }

            ApplicationUser user = _userManager.GetUserAsync(User).Result;

            var filePath = "/Photos/" + new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + fileinput.FileName;
            fileinput.CopyTo(new FileStream(_hostingEnvironment.WebRootPath + filePath, FileMode.Create));

            Models.File file = new();
            file.Id = Guid.NewGuid().ToString();
            file.Name = filePath[8..];
            file.ContentType = fileinput.ContentType;
            file.Path = filePath;
            file.UserId = user.Id;

            user.HeadPhotoFileId = file.Id;

            if (_iFileService.InsertAsync(file).Result && _iUserService.UpdateAsync(user).Result)
            {
                return ApiResultHelper.Success();
            }
            return ApiResultHelper.Error("文件上传失败！");
        }
    }
}
