using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
