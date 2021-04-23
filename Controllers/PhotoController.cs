using IPhoto.Common.ApiResult;
using IPhoto.Models;
using IPhoto.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPhoto.Controllers
{
    public class PhotoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPhotoService _iPhotoService;
        private readonly IUserService _iUserService;
        private readonly IUserLikeService _iUserLikeService;
        private readonly IFileService _iFileService;

        public PhotoController(UserManager<ApplicationUser> userManager, IPhotoService photoService, IUserService userService, IUserLikeService userLikeService, IFileService fileService)
        {
            this._userManager = userManager;
            this._iPhotoService = photoService;
            this._iUserService = userService;
            this._iUserLikeService = userLikeService;
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

        [Authorize]
        [HttpGet]
        public ApiResult Like(string Id)
        {
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            UserLike userLike = _iUserLikeService.GetAsync(u => u.PhotoId == Id && u.UserId == user.Id).Result;
            if (userLike == null)
            {
                userLike = new();
                userLike.Id = "like" + System.Guid.NewGuid().ToString()[4..];
                userLike.PhotoId = Id;
                userLike.UserId = user.Id;
                userLike.CreateAt = DateTime.Now;
                _iUserLikeService.InsertAsync(userLike);
                Photo photo = _iPhotoService.GetAsync(Id).Result;
                photo.LikeCount += 1;
                _iPhotoService.UpdateAsync(photo);
                return ApiResultHelper.Success("已添加到我的收藏列表！");
            }
            return ApiResultHelper.Success("已存在于我的收藏列表！");
        }

        [Authorize]
        [HttpGet]
        public ApiResult Title(string Id, string Title)
        {
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            Photo photo = _iPhotoService.GetAsync(Id).Result;
            if (user.Id != photo.UserId)
            {
                return ApiResultHelper.Error("错误请求！");
            }
            photo.Title = Title;
            _iPhotoService.UpdateAsync(photo);
            return ApiResultHelper.Success("已存在于我的收藏列表！");
        }
    }
}
