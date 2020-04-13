using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace doingit2_repo.Models
{
    public class Board : Document
    {
        public string? TrelloBoardId { get; set; }
        public string[] EveryDay { get; set; } = new string[] { };
        public string[] WeekDay { get; set; } = new string[] { };
        public string[] Monday { get; set; } = new string[] { };
        public string[] Tuesday { get; set; } = new string[] { };
        public string[] Wednesday { get; set; } = new string[] { };
        public string[] Thursday { get; set; } = new string[] { };
        public string[] Friday { get; set; } = new string[] { };
        public string[] Saturday { get; set; } = new string[] { };
        public string[] Sunday { get; set; } = new string[] { };

    }
}
