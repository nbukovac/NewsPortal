using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Models.ViewModels
{
    public class FrontPageArticlesViewModel
    {
        public Category Category { get; set; }
        public List<Article> Trending { get; set; }
        public List<Article> Newest { get; set; }
    }
}
