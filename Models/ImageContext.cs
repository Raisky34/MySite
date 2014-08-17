using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MySite.Models
{
    public class ImageContext : DbContext
    {
        public ImageContext() : base("DefaultConnection") { }
        public DbSet<Image> Images { get; set; }
        //public DbSet<SlideshowImage> SlideshowImages { get; set; }
    }
}
