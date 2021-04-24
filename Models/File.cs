using SqlSugar;
using System;

namespace IPhoto.Models
{
    [SugarTable("File")]
    public class File
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string ContentType { get; set; }

        [SqlSugar.SugarColumn(IsTranscoding = true)]
        public string Name { get; set; }

        public string Path { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
