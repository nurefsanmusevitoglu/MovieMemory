using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class Friendship
    {
        public Friendship()
        {
            Friends = new List<User>();
            WaitingFriends = new List<User>();
            RequestedFriends = new List<User>();
        }

        public List<User> Friends { get; set; }
        public List<User> WaitingFriends { get; set; }
        public List<User> RequestedFriends { get; set; }
    }
}