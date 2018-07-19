using System;
using System.Collections.Generic;
using System.Text;

namespace Plugins
{
    public class StructInfo
    {
        private int _menuId = -1;

        public int MenuId
        {
            get { return _menuId; }
            set { _menuId = value; }
        }

        private string _menuName = string.Empty;

        public string MenuName
        {
            get { return _menuName; }
            set { _menuName = value; }
        }

        private int _menuIdParent = 0;

        public int MenuIdParent
        {
            get { return _menuIdParent; }
            set { _menuIdParent = value; }
        }

        private string _dllName;

        public string DllName
        {
            get { return _dllName; }
            set { _dllName = value; }
        }

        public StructInfo()
        {
        }
    }
}
