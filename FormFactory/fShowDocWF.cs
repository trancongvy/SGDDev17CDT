using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DataFactory;
using CDTDatabase;
using DesignWorkflow;
using Designflow;
namespace FormFactory
{
    public partial class fShowDocWF : DevExpress.XtraEditors.XtraForm
    {
        Color BgC;
        Font font = new Font("Time New Roman", 10);
        int linewidth = 1;

        Brush brush = new SolidBrush(Color.Blue);

        Brush brushSl = new SolidBrush(Color.Red);
        public List<Task> lTask = new List<Task>();
        public List<Designflow.Action> lAction = new List<Designflow.Action>();
        WF wF;
        DataMasterDetail _Data;
        Graphics graph;
        public fShowDocWF(DataMasterDetail _data)
        {
            InitializeComponent();
            graph = Pic.CreateGraphics();
            wF = new WF(_data.DrTable["sysTableID"].ToString());
            lTask = wF.lTask;
            lAction = wF.lAction;
            BgC = this.BackColor;
            _Data = _data;
            this.Paint += new PaintEventHandler(fShowDocWF_Paint);
            this.Resize += new EventHandler(fShowDocWF_Resize);
            this.ResizeEnd += new EventHandler(fShowDocWF_ResizeEnd);
            this.MaximumSizeChanged += new EventHandler(fShowDocWF_MaximumSizeChanged);
            this.ResizeRedraw = true;
            this.Activated += new EventHandler(fShowDocWF_Activated);
            Pic.Resize += new EventHandler(Pic_Resize);
            Pic.Paint += new PaintEventHandler(Pic_Paint);
            //DrawAll();
        }

        void fShowDocWF_Activated(object sender, EventArgs e)
        {
            DrawAll();
        }

        void Pic_Paint(object sender, PaintEventArgs e)
        {
            DrawAll();
        }

        void Pic_Resize(object sender, EventArgs e)
        {
            DrawAll();
        }

        void fShowDocWF_Resize(object sender, EventArgs e)
        {
            //DrawAll();
        }

        void fShowDocWF_MaximumSizeChanged(object sender, EventArgs e)
        {
           // DrawAll();
        }



        void fShowDocWF_ResizeEnd(object sender, EventArgs e)
        {
           //DrawAll();
        }



        void fShowDocWF_Paint(object sender, PaintEventArgs e)
        {
           //DrawAll();
        }
        private void DrawAll()
        {
           
            graph.Clear(BgC);
            
            foreach (Task T in lTask)
            {
                DrawTask(T, true);

            }
            foreach (Designflow.Action A in lAction)
            {
                DrawAction(A, true);

            }
        }
        private void DrawTask(Task T, bool isText)
        {
            Pen pen = new Pen(brush, linewidth);
            if (_Data != null && _Data.DrCurrentMaster != null && _Data.DrCurrentMaster.Table.Columns.Contains("TaskID"))
            {
                if (_Data.DrCurrentMaster["TaskID"].ToString() == T.id.ToString())
                    pen = new Pen(brushSl, linewidth + 1);
            }
           // Graphics graph = Pic.CreateGraphics();
            graph.DrawRectangle(pen, new Rectangle(T.P, new Size(T.w, T.h)));
            if (isText) graph.DrawString(T.Label, font, brush, new PointF(T.P.X + 10, T.P.Y + T.h / 2 - 6));
            
            //graph1.DrawRectangle(pen, new Rectangle(T.P, new Size(T.w, T.h)));
           // if (isText) graph1.DrawString(T.Label, font, brush, new PointF(T.P.X + T.w / 2 - 30, T.P.Y + T.h / 2 - 6));
            T.getPoint();
            //T.CPoint = T.getCPoint();
            //graph.DrawEllipse(pen, T.CPoint.X - 5, T.CPoint.Y - 5, 10, 10);
            //graph1.DrawEllipse(pen, T.CPoint.X - 5, T.CPoint.Y - 5, 10, 10);
        }
        private void DrawAction(Designflow.Action A, bool isText)
        {
            Pen pen = new Pen(brush, linewidth);
            DrawAction(A, false, pen);

        }
        private void DrawArrow(Pen pen, Point P, int des)
        {
            Graphics graph = Pic.CreateGraphics();
           
            for (int i = -4; i < 5; i++)
            {
                switch (des)
                {
                    case 0: graph.DrawLine(pen, P.X + i, P.Y + 3 + Math.Abs(i), P.X + i, P.Y + Math.Abs(i)); break;
                    case 1: graph.DrawLine(pen, P.X + i, P.Y - 3 - Math.Abs(i), P.X + i, P.Y - Math.Abs(i)); break;
                    case 2: graph.DrawLine(pen, P.X + 3 + Math.Abs(i), P.Y + i, P.X + Math.Abs(i), P.Y + i); break;
                    case 3: graph.DrawLine(pen, P.X - 3 - Math.Abs(i), P.Y + i, P.X - Math.Abs(i), P.Y + i); break;

                }
                //switch (des)
                //{
                //    case 0: graph1.DrawLine(pen, P.X + i, P.Y + 3 + Math.Abs(i), P.X + i, P.Y + Math.Abs(i)); break;
                //    case 1: graph1.DrawLine(pen, P.X + i, P.Y - 3 - Math.Abs(i), P.X + i, P.Y - Math.Abs(i)); break;
                //    case 2: graph1.DrawLine(pen, P.X + 3 + Math.Abs(i), P.Y + i, P.X + Math.Abs(i), P.Y + i); break;
                //    case 3: graph1.DrawLine(pen, P.X - 3 - Math.Abs(i), P.Y + i, P.X - Math.Abs(i), P.Y + i); break;
                   
                //}
            }
        }
        private void DrawAction(Designflow.Action A, bool isText, Pen pen)
        {



            Graphics graph = Pic.CreateGraphics();
           
            for (int i = 1; i < A.P.Count; i++)
            {
                if (i == A.P.Count - 1)
                {
                    // pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    int des = 0;
                    graph.DrawLine(pen, A.P[i - 1], A.P[i]);
                    //graph1.DrawLine(pen, A.P[i - 1], A.P[i]);
                    if (A.P[i - 1].X == A.P[i].X)
                    {
                        if (A.P[i - 1].Y > A.P[i].Y) des = 0;
                        else des = 1;
                    }
                    else if (A.P[i - 1].Y == A.P[i].Y)
                    {
                        if (A.P[i - 1].X > A.P[i].X) des = 2;
                        else des = 3;
                    }
                    DrawArrow(pen, A.P[i], des);
                }
                else
                {
                    graph.DrawLine(pen, A.P[i - 1], A.P[i]);
                    //graph1.DrawLine(pen, A.P[i - 1], A.P[i]);
                }
                if (pen.Color == Color.Red || pen.Color == this.BgC)
                {
                    if (A.P[i].X == A.P[i - 1].X)
                    {
                        graph.DrawRectangle(pen, new Rectangle(new Point(A.P[i].X - 2, (A.P[i].Y + A.P[i - 1].Y) / 2), new Size(4, 4)));
                    }
                    else if (A.P[i].Y == A.P[i - 1].Y)
                    {
                        graph.DrawRectangle(pen, new Rectangle(new Point((A.P[i - 1].X + A.P[i].X) / 2, A.P[i].Y - 2), new Size(4, 4)));
                    }
                    if (i == 1) graph.DrawRectangle(pen, new Rectangle(new Point(A.P[i - 1].X - 2, A.P[i - 1].Y - 2), new Size(4, 4)));
                }
            }
        }

        private void Pic_Click(object sender, EventArgs e)
        {
            DrawAll();
        }

        private void fShowDocWF_Load(object sender, EventArgs e)
        {
            DrawAll();
        }
    }
}