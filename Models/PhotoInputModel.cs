using System.ComponentModel.DataAnnotations;

namespace IPhoto.Models
{
    public partial class PhotoInputModel
    {
        public class UploadModel
        {
            [Required(ErrorMessage = "请输入一个 Title 吧！")]
            [Display(Name = "Title")]
            public string Title { get; set; }

            [Required(ErrorMessage = "请上传图片！")]
            [Display(Name = "图片")]
            public string FileId { get; set; }

            public string FileInput { get; set; }
        }
    }
}
