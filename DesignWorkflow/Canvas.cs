using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;

namespace Designflow
{
    public enum ConnectPoint { TopPoint, BotPoint, RightPoint, LeftPoint }
    public class Task
    {
        public Guid id;
        public string Label;
        public Point P;
        public int h;
        public int w;
        public Point CPoint;
        public Point TPoint;
        public Point BPoint;
        public Point RPoint;
        public Point LPoint;
        public DataTable tbSecu;
        public DataTable tbSecuGroup;
        public Image Icon;
        private bool _isBegin;
         private  bool _isEnd;
        private bool _isCancel;
        public bool isBegin
        {
            get { return _isBegin; }
            set {
                _isBegin = value;
                if (value)
                {
                   
                    isCancel = false;
                    isEnd = false;
                }
            }
        }
        public bool isEnd
        {
            get { return _isEnd; }
            set
            {
                _isEnd = value;
                if (value)
                {
                    
                    isCancel = false;
                    isBegin = false;
                }
            }
        }
        public bool isCancel
        {
            get { return _isCancel; }
            set
            {
                _isCancel = value;
                if (value)
                {
                    isEnd = false;
                    isBegin = false;
                }
            }
        }
        public int ApprovedStt;
        public Task()
        {
            id = Guid.NewGuid();
            Label = "New Label";
            ApprovedStt = 0;
        }
        public Task(DataRow dr)
        {
           
            id = new Guid(dr["Id"].ToString());
            Label = dr["TaskLabel"].ToString();
            ApprovedStt = int.Parse(dr["ApprovedStt"].ToString());
            isBegin = bool.Parse(dr["isBegin"].ToString());
            isEnd = !(dr["isEnd"]==DBNull.Value || !bool.Parse(dr["isEnd"].ToString()));
            isCancel = !(dr["isCancel"] == DBNull.Value || !bool.Parse(dr["isCancel"].ToString()));
            string[] point = dr["Point"].ToString().Split(",".ToCharArray());
            if (point.Length == 2) P = new Point(int.Parse(point[0]), int.Parse(point[1]));
            h = int.Parse(dr["height"].ToString());
            w = int.Parse(dr["width"].ToString());
            getPoint();
            if (dr["Icon"] != DBNull.Value)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(dr["Icon"] as byte[]);
                if (ms != null)
                    Icon = Image.FromStream(ms);
            }
        }


        public string GetPString()
        {
            return P.X.ToString() + "," + P.Y.ToString();
        }
        public void getPoint()
        {
            CPoint = new Point(P.X + w / 2, P.Y + h / 2);
            TPoint = new Point(P.X + w / 2, P.Y);
            BPoint = new Point(P.X + w / 2, P.Y + h);
            RPoint = new Point(P.X + w, P.Y + h / 2);
            LPoint = new Point(P.X, P.Y + h / 2);
        }
    }

    public class Action
    {
        public List<Point> P = new List<Point>();

        public Task BT;
        public Task ET;
        public Guid Id;
        public string Name;
        public string Label;
        public Point EPoint;
        public Point BPoint;
        public ConnectPoint BPName;
        public ConnectPoint EPName;
        public bool isDesign;
        public int type;
        public Point[] ActiveLine = null;
        public List<Parameter> lPara;
        public Image Icon;
        public string Command;
        public string AfterUpdateCommand;
        public bool AutoDo;
        public string ShowCond;
        public bool isRefresh;
        public string Confirm = "";
        public string Message = "";
        public Action()
        {
            Name = "New Action";

            Label = "New Action Label";
            isDesign = true;
            Id = Guid.NewGuid();

            lPara = new List<Parameter>();

        }
        public Action(DataRow dr)
        {
            Name =  dr["CommandName"].ToString();
            Id = new Guid(dr["Id"].ToString());
            isDesign = true;
            AutoDo = bool.Parse(dr["AutoDo"].ToString());
            Condition = dr["Condition"].ToString();
            if (dr["Command"] != DBNull.Value)
                Command = dr["Command"].ToString();
            if (dr["AfterUpdate"] != DBNull.Value)
                AfterUpdateCommand = dr["AfterUpdate"].ToString();
            isRefresh = bool.Parse(dr["isRefresh"].ToString());
            if (dr["ShowCond"] != DBNull.Value)
                ShowCond = dr["ShowCond"].ToString();
            string[] lpoint = dr["P"].ToString().Split(" ".ToCharArray());
            foreach (string lpoint_i in lpoint)
            {
                string[] point = lpoint_i.Split(",".ToCharArray());
                if (point.Length == 2) P.Add(new Point(int.Parse(point[0]), int.Parse(point[1])));
            }
            if (P.Count >= 2)
            {
                BPoint = P[0]; EPoint = P[P.Count - 1];
                
            }
            if (dr.Table.Columns.Contains("Confirm") && dr["Confirm"] != DBNull.Value)
            {
                this.Confirm = dr["Confirm"].ToString();
            } 
            if (dr.Table.Columns.Contains("Message") && dr["Message"] != DBNull.Value)
            {
                this.Message = dr["Message"].ToString();
            }
            if (dr["Icon"] != DBNull.Value)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(dr["Icon"] as byte[]);
                if (ms != null)
                Icon = Image.FromStream(ms);
            }
            
        }
        public string GetPString()
        {
           string PString=string.Empty;
            foreach (Point p in P)
            {
                PString += p.X.ToString() + "," + p.Y.ToString() + " ";
            }
            if (PString.Length > 1) PString.Substring(0, PString.Length - 1);
            return PString;
        }
        public string Condition;


    }
    public struct Parameter
    {
        public string fieldName;
        public object value;
        public string Formular;


    }
}
