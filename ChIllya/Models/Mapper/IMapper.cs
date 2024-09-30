using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Models.Mapper
{
    public interface IMapper<TSource, TResult>
    {
        TResult Map(TSource source);
    }
}
