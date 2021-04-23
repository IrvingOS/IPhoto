using IPhoto.Common.ApiResult;
using IPhoto.Models;
using IPhoto.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;

namespace IPhoto.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _iFileService;
        private readonly IUserService _iUserService;
        private readonly IPhotoService _iPhotoService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileController(IFileService fileService, 
            IUserService userService,
            IPhotoService photoService,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, 
            IWebHostEnvironment hostingEnvironment)
        {
            _iFileService = fileService;
            _iUserService = userService;
            _iPhotoService = photoService;
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
            fileinput.CopyTo(new FileStream(_hostingEnvironment.ContentRootPath + "/publish/wwwroot" + filePath, FileMode.Create));

            Models.File file = new();
            file.Id = "file" + Guid.NewGuid().ToString()[4..];
            file.Name = filePath[8..];
            file.ContentType = fileinput.ContentType;
            file.Path = filePath;
            file.UserId = _userManager.GetUserId(User);
            file.CreateAt = DateTime.Now;
            
            if (_iFileService.InsertAsync(file).Result)
            {
                return ApiResultHelper.Success(file.Id);
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
            fileinput.CopyTo(new FileStream(_hostingEnvironment.ContentRootPath + "/publish/wwwroot" + filePath, FileMode.Create));

            Models.File file = new();
            file.Id = "file" + Guid.NewGuid().ToString()[4..];
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

        public IActionResult DownLoad(string id)
        {
            Models.File file = _iFileService.GetAsync(id).Result;

            var fileStream = new FileStream(_hostingEnvironment.ContentRootPath + "/publish/wwwroot" + file.Path, FileMode.Open, FileAccess.ReadWrite);
            return File(fileStream, "application/octet-stream", file.Name);
        }
        
        public IActionResult DownLoadPhoto(string id)
        {
            Models.File file = _iFileService.GetAsync(id).Result;
            Photo photo = _iPhotoService.GetAsync(p => p.FileId == id).Result;
            if (photo != null)
            {
                photo.DownloadCount += 1;
                _iPhotoService.UpdateAsync(photo);
            }

            var fileStream = new FileStream(_hostingEnvironment.ContentRootPath + "/publish/wwwroot" + file.Path, FileMode.Open, FileAccess.ReadWrite);
            return File(fileStream, "application/octet-stream", file.Name);
        }
    }
}
