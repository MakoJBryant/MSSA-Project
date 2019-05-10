using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    // JH- Added model validation 
    public class VideoModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Owner { get; set; }

        [Required]
        public string VideoLink { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        // JH- Set property to set itself to the time of the Http request
        public string DatePosted { get; set; }
        public ICollection<CommentModel> Comments { get; set; }
    }
}
