using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
        public bool IsFriend { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }
    }
}