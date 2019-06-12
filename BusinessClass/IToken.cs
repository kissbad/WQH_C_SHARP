using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessClass
{
    public interface IToken
    {
        string user_id { get; set; }
        string pass_word { get; set; }
    }
}
