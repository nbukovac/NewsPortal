using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortal.Models.ViewModels
{
    public class AddCommentViewModel
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public Guid ArticleId { get; set; }
    }
}
