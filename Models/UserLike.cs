using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPhoto.Models
{
    [SugarTable("UserLike")]
    public class UserLike
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string PhotoId { get; set; }

        public DateTime CreateAt { get; set; }

        [SugarColumn(IsIgnore = true)]
        public Photo Photo { get; set; }
    }
}
