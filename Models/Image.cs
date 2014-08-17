using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySite.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string User { get; set; }
        public string Path { get; set; }

    }
}
