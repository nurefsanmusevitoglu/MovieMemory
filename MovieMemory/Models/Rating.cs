﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieMemory.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Star { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
    }
}