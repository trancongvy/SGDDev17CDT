using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CDTLib;
using System.Net.Mail;
namespace ErrorManager
{
	//****************************************************************************
	//
	// Mục đích:	tạo và hiển thị hộp thoại báo lỗi chương trình
	// Chức năng:	tạo hộp thoại với chuỗi mô tả lỗi và chi tiết lỗi
	//				hiển thị hộp thoại lên màn hình theo dạng custom detail
	// Tham số:		- strMoTaLoi: lỗi được mô tả theo kiểu hỗ trợ cho user
	//				- strChiTietLoi: lỗi được mô tả chi tiết hỗ trợ cho debugger
	//
	//****************************************************************************
	internal class FrmErrorMessage : System.Windows.Forms.Form
	{
		private string _StrMoTaLoi = null;
		private string _StrChiTietLoi = null;
		private bool _BlnChiTiet = false;
			
		private System.Windows.Forms.Button BtnChiTiet;
		private System.Windows.Forms.Button BtnThoat;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label LblMoTaLoi;
        private RichTextBox richTextBoxChiTietLoi;
        private Button BtnRestart;
        private Button BtnExit;
        private CheckBox checkBoxEmail;
		private System.ComponentModel.Container components = null;
        public int retu=0;
		public FrmErrorMessage(string strMoTaLoi,string strChiTietLoi)
		{
			InitializeComponent();
			_StrMoTaLoi = strMoTaLoi;
			_StrChiTietLoi = GetInfoError() + strChiTietLoi;
		}



        private string GetInfoError()
        {
            string s = string.Empty;
            object menuID = Config.GetValue("sysMenuID");
            object menuName = Config.GetValue("MenuName");
            object operation = Config.GetValue("Operation");
            if (menuID != null)
                s += "MenuID: " + menuID.ToString() + "\n";
            if (menuName != null)
                s += "MenuName: " + menuName.ToString() + "\n";
            if (operation != null)
                s += "Operation: " + operation.ToString() + "\n";
            return s;
        }

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
            if (checkBoxEmail.CheckState == CheckState.Checked)
                SendError(_StrMoTaLoi, _StrChiTietLoi);
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmErrorMessage));
            this.BtnChiTiet = new System.Windows.Forms.Button();
            this.BtnThoat = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LblMoTaLoi = new System.Windows.Forms.Label();
            this.richTextBoxChiTietLoi = new System.Windows.Forms.RichTextBox();
            this.BtnRestart = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.checkBoxEmail = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnChiTiet
            // 
            this.BtnChiTiet.Location = new System.Drawing.Point(112, 84);
            this.BtnChiTiet.Name = "BtnChiTiet";
            this.BtnChiTiet.Size = new System.Drawing.Size(80, 24);
            this.BtnChiTiet.TabIndex = 1;
            this.BtnChiTiet.Text = "&Chi tiết >>";
            this.BtnChiTiet.Click += new System.EventHandler(this.BtnChiTiet_Click);
            // 
            // BtnThoat
            // 
            this.BtnThoat.Location = new System.Drawing.Point(198, 84);
            this.BtnThoat.Name = "BtnThoat";
            this.BtnThoat.Size = new System.Drawing.Size(80, 24);
            this.BtnThoat.TabIndex = 0;
            this.BtnThoat.Text = "&Tiếp tục";
            this.BtnThoat.Click += new System.EventHandler(this.BtnThoat_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // LblMoTaLoi
            // 
            this.LblMoTaLoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMoTaLoi.Location = new System.Drawing.Point(120, 9);
            this.LblMoTaLoi.Name = "LblMoTaLoi";
            this.LblMoTaLoi.Size = new System.Drawing.Size(326, 42);
            this.LblMoTaLoi.TabIndex = 3;
            this.LblMoTaLoi.Text = "label1";
            this.LblMoTaLoi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBoxChiTietLoi
            // 
            this.richTextBoxChiTietLoi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBoxChiTietLoi.Location = new System.Drawing.Point(0, 13);
            this.richTextBoxChiTietLoi.Name = "richTextBoxChiTietLoi";
            this.richTextBoxChiTietLoi.ReadOnly = true;
            this.richTextBoxChiTietLoi.Size = new System.Drawing.Size(458, 109);
            this.richTextBoxChiTietLoi.TabIndex = 5;
            this.richTextBoxChiTietLoi.Text = "";
            this.richTextBoxChiTietLoi.Visible = false;
            // 
            // BtnRestart
            // 
            this.BtnRestart.Location = new System.Drawing.Point(284, 84);
            this.BtnRestart.Name = "BtnRestart";
            this.BtnRestart.Size = new System.Drawing.Size(80, 24);
            this.BtnRestart.TabIndex = 6;
            this.BtnRestart.Text = "&Khởi động lại";
            this.BtnRestart.Click += new System.EventHandler(this.BtnRestart_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(370, 84);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(80, 24);
            this.BtnExit.TabIndex = 7;
            this.BtnExit.Text = "T&hoát";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // checkBoxEmail
            // 
            this.checkBoxEmail.AutoSize = true;
            this.checkBoxEmail.Checked = true;
            this.checkBoxEmail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEmail.Location = new System.Drawing.Point(370, 61);
            this.checkBoxEmail.Name = "checkBoxEmail";
            this.checkBoxEmail.Size = new System.Drawing.Size(69, 17);
            this.checkBoxEmail.TabIndex = 8;
            this.checkBoxEmail.Text = "Gởi email";
            this.checkBoxEmail.UseVisualStyleBackColor = true;
            // 
            // FrmErrorMessage
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(458, 122);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxEmail);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnRestart);
            this.Controls.Add(this.LblMoTaLoi);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BtnThoat);
            this.Controls.Add(this.BtnChiTiet);
            this.Controls.Add(this.richTextBoxChiTietLoi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmErrorMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông báo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmErrorMessage_FormClosing);
            this.Load += new System.EventHandler(this.FrmErrorMessage_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmErrorMessage_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void BtnThoat_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}

		private void FrmErrorMessage_Load(object sender, System.EventArgs e)
		{
            LblMoTaLoi.Text = _StrMoTaLoi;
            richTextBoxChiTietLoi.Text = _StrChiTietLoi;
		}

		private void BtnChiTiet_Click(object sender, System.EventArgs e)
		{
			if (_BlnChiTiet)
			{
				BtnChiTiet.Text = "&Chi tiết >>";
				_BlnChiTiet = false;
			}
			else
			{
				BtnChiTiet.Text = "<< &Chi tiết";
				_BlnChiTiet = true;
			}
            if (_BlnChiTiet)
            {
                richTextBoxChiTietLoi.Visible = true;
                this.Height = 260;
            }
            else
            {
                richTextBoxChiTietLoi.Visible = false;
                this.Height = 150;
            }
		}

		private void FrmErrorMessage_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Dispose();
		}

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
            retu = 1;
            
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            retu = 2;
            
        }

        private void SendError(string moTaLoi, string chiTietLoi)
        {
            try
            {
                string H_KEY = CDTLib.Config.GetValue("H_KEY").ToString();
                string productName = CDTLib.Config.GetValue("ProductName").ToString();
                string companyName = Microsoft.Win32.Registry.GetValue(H_KEY, "CompanyName", string.Empty).ToString();

                MailMessage mail = new MailMessage("SendErr@sgdsoft.com", "Error@sgdsoft.com");
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.sgdsoft.com";
                mail.Subject = "Error of " + productName + " from " + companyName;
                mail.Body = moTaLoi + "\n Người dùng" + Config.GetValue("UserName").ToString() + "\n Tên máy: " + Config.GetValue("ComputerName").ToString() + "\n" + chiTietLoi;
                if (mail.Body.Contains("Cannot insert duplicate")) return;
                client.Send(mail); 
              


            }
            catch
            {
            }
        }

        private void FrmErrorMessage_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
	}


	public sealed class ErrMessageBox
	{
		public ErrMessageBox()
		{
		}
		public static int Show(string strMoTaLoi,string strChiTietLoi)
		{
			FrmErrorMessage frmErrMsg = new FrmErrorMessage(strMoTaLoi,strChiTietLoi);
			frmErrMsg.Show();
            return 0;
		}
	}
}
