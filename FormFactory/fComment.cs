using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using DataFactory;
using CDTLib;
using System.Data.SqlClient;

namespace FormFactory
{
    public partial class fComment : DevExpress.XtraEditors.XtraForm
    {
        CDTData data;
       string MTID;
        DataTable tb;
        Database dbSt = Database.NewStructDatabase();
        public fComment(CDTData _data, int Index)
        {
            InitializeComponent();
            data = _data;
            
            DataRow drMaster = _data.DsData.Tables[0].Rows[Index];
            MTID = drMaster[_data.PkMaster.FieldName].ToString();
            data.DrCurrentMaster = drMaster;
        }
        
        private void getcomment(string MTID)
        {
            string sql = "select a.*, b.UserName, b.FullName, c.TaskLabel from syscomment a left join sysUser b on a.sysuserid=b.sysUserID left join sysTask c on a.taskid=c.id  where MTID='" + MTID + "' order by hDate desc";
            tb = dbSt.GetDataTable(sql);
            vScrollBar1.Scroll += new ScrollEventHandler(vScrollBar1_Scroll);
        }

        void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
           if(panel1.Height<370) return;
           double top = e.NewValue * (panel1.Height - 370) / 100;
           top = Math.Round(top, 0);
           panel1.Top = 0 -int.Parse(top.ToString());
        }

        private void fComment_Load(object sender, EventArgs e)
        {
            getcomment(MTID);
            genComment();
        }

        private void genComment()
        {
            int y = 10;
            panel1.Controls.Clear();
            for (int i=0; i<tb.Rows.Count;i++)
            {
                DataRow dr = tb.Rows[i];
                LabelControl l = new LabelControl();
                l.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
                l.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
                l.Appearance.Options.UseFont = true;
                l.Appearance.Options.UseForeColor = true;
                l.Text = dr["UserName"].ToString() + ": ";
                l.Location = new Point(3, y);
                panel1.Controls.Add(l);
                RichTextBox rt = new RichTextBox();
                rt.BorderStyle = System.Windows.Forms.BorderStyle.None;
                rt.BackColor = Color.White;
                rt.Location = new System.Drawing.Point(l.Width+6, y);
                rt.ReadOnly = true;
                rt.Text = dr["content"].ToString().Replace("~!", "'");
                rt.Width = 330;
                using (Graphics g = CreateGraphics())
                {
                    rt.Height = (int)g.MeasureString(rt.Text, rt.Font, rt.Width).Height+20;
                    y += rt.Height;
                }
                panel1.Controls.Add(rt);

            }
            panel1.Height = y + 1000;
        }

        private void vScrollBar1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "") return;
            richTextBox1.Text = richTextBox1.Text.Replace("'", "~!");
            dbSt.BeginMultiTrans();
            string sql = "insert into syscomment (sysUserID, hdate, content, mtid, taskID) values (@sysUserID, @hdate, @conten, @mtid, @taskID)";
            dbSt.UpdateDatabyPara(sql, new string[] { "@sysUserID", "@hdate", "@conten", "@mtid", "@taskID" }, new object[]{
                Config.GetValue("sysUserID").ToString(),DateTime.Now, richTextBox1.Text ,MTID,  data.DrCurrentMaster["TaskID"]==DBNull.Value? Guid.NewGuid() :data.DrCurrentMaster["TaskID"]});
            //dbSt.UpdateByNonQuery(sql);
            dbSt.EndMultiTrans();
            richTextBox1.Text = "";
            getcomment(MTID);
            genComment();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                int i = tb.Rows.Count;
                timer1.Enabled = false;
                getcomment(MTID);
                if (tb.Rows.Count > i) genComment();
                timer1.Enabled = true;
            }
            catch { }
        }
    }
}