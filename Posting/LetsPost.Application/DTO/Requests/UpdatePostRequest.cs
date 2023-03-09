using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPost.Application.DTO.Requests
{
    public class UpdatePostRequest : CreatePostRequest
    {
        public int Id { get; set; }
    }
}
