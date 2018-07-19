using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace Plugins
{
    public interface ICustom
    {
        List<StructInfo> ListStructInfo { get;set;}
        Form Execute(int menuId);
    }
}
