using IPhoto.Common.ApiResult;
using IPhoto.Models;
using IPhoto.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPhoto.Controllers
{
    public class PhotoController
    {

        private readonly IPhotoService _iPhotoService;
        private readonly IUserService _iUserService;
        private readonly IFileService _iFileService;

        public PhotoController(IPhotoService photoService, IUserService userService, IFileService fileService)
        {
            this._iPhotoService = photoService;
            this._iUserService = userService;
            this._iFileService = fileService;
        }

        [HttpGet]
        public ApiResult Photo(String Id)
        {
            Photo photo = _iPhotoService.GetAsync(Id).Result;
            photo.ApplicationUser = _iUserService.GetAsync(photo.UserId).Result;
            photo.File = _iFileService.GetAsync(photo.FileId).Result;
            return ApiResultHelper.Success(photo);
        }
    }
}
