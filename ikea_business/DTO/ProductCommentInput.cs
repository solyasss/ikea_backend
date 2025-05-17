using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikea_business.DTO
{
    public record ProductCommentInput(
         int ProductId,
         int UserId,
         string CommentText,
         int? Rating
     );
}
