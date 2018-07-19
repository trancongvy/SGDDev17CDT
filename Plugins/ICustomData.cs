using System;
using System.Collections.Generic;
using System.Text;

namespace Plugins
{
    public interface ICustomData
    {
        bool Result {get;}
        StructPara Info {set;}
        void ExecuteBefore();
        void ExecuteAfter();
    }
}
