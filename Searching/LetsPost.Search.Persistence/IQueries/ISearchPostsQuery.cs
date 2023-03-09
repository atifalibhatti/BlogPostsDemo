using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsPost.Search.Persistence.IQueries;
public interface ISearchPostsQuery
{
    public string Searchterm { get; set; }
}
