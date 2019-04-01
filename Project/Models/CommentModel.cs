using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
