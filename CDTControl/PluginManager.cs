using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.IO;
using System.Runtime.Remoting;
using System.Xml;
using CDTLib;
using System.Windows.Forms;
namespace CDTControl
{
    public class PluginManager
    {
        private string _pluginPath = "";
        //if (Config.GetValue("Reportspath") !=null)
        //System.Windows.Forms.Application.StartupPath + "\\Plugins\\" + Config.GetValue("Package").ToString() + "\\";
        private List<ICustomData> _lstICustomData = new List<ICustomData>();
        private List<ICustomControl> _lstICustomControl = new List<ICustomControl>();

        public List<ICustomControl> LstICustomControl
        {
            get { return _lstICustomControl; }
            set { _lstICustomControl = value; }
        }
        private List<ICustom> _lstICustom = new List<ICustom>();
        public PluginManager()
        {
                    }
        public string PluginPath
        {
            get 
            {
                if (_pluginPath == "")
                {
                    if (Config.GetValue("DuongDanPlugins") != null)
                        _pluginPath = Config.GetValue("DuongDanPlugins").ToString() + "\\" + Config.GetValue("Package").ToString() + "\\";
                    else
                        _pluginPath = Config.GetValue("StartupPath").ToString() + "\\Plugins\\" + Config.GetValue("Package").ToString() + "\\";

                }
                return _pluginPath;
            }
        }
        public List<ICustomData> LstICustomData
        {
            get { return _lstICustomData; }
            set { _lstICustomData = value; }
        }
        public List<ICustom> LstICustom
        {
            get { return _lstICustom; }
            set { _lstICustom = value; }
        }
        private List<StructInfo> _lstStructInfo = new List<StructInfo>();

        public List<StructInfo> LstStructInfo
        {
            get { return _lstStructInfo; }
            set { _lstStructInfo = value; }
        }

        public void AddICustom()
        {
            if (!Directory.Exists(PluginPath))
                return;
            string[] dllFiles = Directory.GetFiles(PluginPath, "*.dll");
            foreach (string str in dllFiles)
            {
                FileInfo f = new FileInfo(str);
                string t = f.Name.Split(".".ToCharArray())[0];
                string pluginName = t + "." + t;
                ObjectHandle oh = Activator.CreateComInstanceFrom(str, pluginName);
                ICustom pluginClass = oh.Unwrap() as ICustom;
                if (pluginClass != null)
                {
                    if (!_lstICustom.Contains(pluginClass))
                    {
                        string xmlString = PluginPath + t + ".xml";
                        pluginClass.ListStructInfo = GetStructInfo(xmlString, pluginName);
                        _lstICustom.Add(pluginClass);
                        _lstStructInfo.AddRange(pluginClass.ListStructInfo);
                    }
                }
            }
        }

        public void AddICustomData()
        {
            if (!Directory.Exists(PluginPath))
                return;
            string[] dllFiles = Directory.GetFiles(PluginPath, "*.dll");
            foreach (string str in dllFiles)
            {
                FileInfo f = new FileInfo(str);
                string t = f.Name.Split(".".ToCharArray())[0];
                string pluginName = t + "." + t;
                ObjectHandle oh = Activator.CreateComInstanceFrom(str, pluginName);
                ICustomData pluginClass1 = oh.Unwrap() as ICustomData;
                if (pluginClass1 != null)
                {
                    if (!_lstICustomData.Contains(pluginClass1))
                        _lstICustomData.Add(pluginClass1);
                }
            }
        }

        public void AddICustomControl()
        {
            if (!Directory.Exists(PluginPath))
                return;
            string[] dllFiles = Directory.GetFiles(PluginPath, "*.dll");
            foreach (string str in dllFiles)
            {
                FileInfo f = new FileInfo(str);
                string t = f.Name.Split(".".ToCharArray())[0];
                string pluginName = t + "." + t;
                ObjectHandle oh = Activator.CreateComInstanceFrom(str, pluginName);
                ICustomControl pluginClass2 = oh.Unwrap() as ICustomControl;
                if (pluginClass2 != null)
                {
                    if (!_lstICustomControl.Contains(pluginClass2))
                        _lstICustomControl.Add(pluginClass2);
                }
            }
        }

        private List<StructInfo> GetStructInfo(string xmlString, string dllName)
        {
            List<StructInfo> listStructInfo = new List<StructInfo>();
            XmlDocument xdInformation = new XmlDocument();
            xdInformation.Load(xmlString);
            XmlNodeList ListData = xdInformation.GetElementsByTagName("Data");
            for (int j = 0; j < ListData.Count; j++)
            {
                XmlNodeList ValueList = ListData[j].ChildNodes;
                StructInfo si = new StructInfo();
                for (int i = 0; i < ValueList.Count; i++)
                {
                    int result;
                    if (ValueList[i].Name == "MenuID" && Int32.TryParse(ValueList[i].InnerText, out result))
                    {
                        si.MenuId = result;
                        continue;
                    }
                    if (ValueList[i].Name == "MenuName")
                    {
                        si.MenuName = ValueList[i].InnerText;
                        continue;
                    }
                    int result2;
                    if (ValueList[i].Name == "MenuIdParent" && Int32.TryParse(ValueList[i].InnerText, out result2))
                    {
                        si.MenuIdParent = result2;
                    }
                }
                si.DllName = dllName;
                listStructInfo.Add(si);
            }
            return listStructInfo;
        }


        public Form  Execute(int menuID, string pluginName)
        {
            Form f = new Form();
            foreach (ICustom iCustom in _lstICustom)
                if (iCustom.ToString() == pluginName)
                {

                    //try
                    //{
                        f = iCustom.Execute(menuID);

                   // }
                   // catch(Exception ex) { }
                }
            return f;
        }
    }
}
