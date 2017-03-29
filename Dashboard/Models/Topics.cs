﻿using System;
using System.Collections.Generic;

namespace Dashboard.Models
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public IEnumerable<Reply> Replies { get; set; }
    }
}