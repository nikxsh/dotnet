using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class Topic
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Body")]
        public string Body { get; set; }

        public DateTime Created { get; set; }

        public IEnumerable<Reply> Replies { get; set; }
    }
}