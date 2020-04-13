using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace doingit2_repo.Models
{
    public class Settings : Document
    {
        public string? Key { get; set; }
        public string? Secret { get; set; }
        public string? Token { get; set; }
    }
}

