using System;
using System.ComponentModel.DataAnnotations;

namespace Dashboard.Models
{
    public class Reply
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Body")]
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public Guid TopicId { get; set; }
    }
}