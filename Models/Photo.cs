using SqlSugar;
using System;

namespace IPhoto.Models
{
    [SugarTable("Photo")]
    public class Photo
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        public string UserId { get; set; }

        [SqlSugar.SugarColumn(IsTranscoding = true)]
        public string Title { get; set; }

        public string FileId { get; set; }

        public int LikeCount { get; set; }

        public int DownloadCount { get; set; }

        public DateTime CreateAt { get; set; }

        [SugarColumn(IsIgnore = true)]
        public ApplicationUser ApplicationUser { get; set; }

        [SugarColumn(IsIgnore = true)]
        public File File { get; set; }
    }
}
