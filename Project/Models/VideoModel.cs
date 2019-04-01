using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class VideoModel
    {
        public int Id { get; set; }
        public string VideoLink { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
    }
}
