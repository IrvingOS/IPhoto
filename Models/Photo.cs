using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace IPhoto.Models
{
    [SugarTable("Photo")]
    public class Photo
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string FileId { get; set; }
        public int LikeCount { get; set; }
        public int DownloadCount { get; set; }

        [SugarColumn(IsIgnore = true)]
        public ApplicationUser ApplicationUser { get; set; }

        [SugarColumn(IsIgnore = true)]
        public File File { get; set; }
    }
}
