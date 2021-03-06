using Microsoft.AspNetCore.Identity;
using SqlSugar;

namespace IPhoto.Models
{
    [SugarTable("AspNetUsers")]
    public class ApplicationUser : IdentityUser
    {
        [SugarColumn(IsPrimaryKey = true)]
        public override string Id { get; set; }

        public string HeadPhotoFileId { get; set; }

        public int Gender { get; set; }
    }
}
