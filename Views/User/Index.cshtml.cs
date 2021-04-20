using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPhoto.Views.User
{
    public partial class IndexModel : PageModel
    {

        [BindProperty]
        public UploadModel Upload { get; set; }

        public class UploadModel
        {
            [Required(ErrorMessage = "请输入一个 Title 吧！")]
            [Display(Name = "Title")]
            public string Title { get; set; }

            public string FileId { get; set; }
        }
    }
        
}
