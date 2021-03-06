using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using System.Data;
using CDTLib;
namespace CusAccounting
{
   public class DbTaoPTT
    {
       public DataTable MT31;
       public DataTable dmVt1;
       public DataTable dmVt2;
       public DataTable MT37;
       public DataTable DtThucUong;
       public DataTable DtThucAn;
       private DataRow drmaster;
       public DataRow drMaster
       {
           get { return drmaster; }
           set {
               drmaster = value;
               tinhTile();
           }
       }
       public Database db = Database.NewDataDatabase();
       public double TileTU=70;
       public double CurTileTU=0;
       public double CurTileTA = 0;
       public double tileTU = 0;
       public double tileTA = 0;
       public double TongtileTU = 0;
       public double TongtileTA = 0;
       public event EventHandler sumChange;
       private double sumtu;
       private double sumta;
       public double SumPNTA
       {
           get { return sumta; }
           set {  sumta=value; }

       }
       public double SumPNTU
       {
           get { return sumtu; }
           set { sumtu = value; }
           
       }
       public DbTaoPTT()
       {
           string sql = "select * from wTonkhotucthoi where isDV=0 and tkkho like '156%'";
           dmVt1= db.GetDataTable(sql);
           sql = "select * from wTonkhotucthoi where isDV=1";
           dmVt2 = db.GetDataTable(sql);

       }
       public void Getdata()
       {
           string sql = "select * from wmt31";
           MT31 = db.GetDataTable(sql);
           if (MT31.Rows.Count > 0) drMaster = MT31.Rows[0];
           sql = "select * from dt37 where 1=0";
           DtThucUong = db.GetDataTable(sql);
           DtThucAn = db.GetDataTable(sql);
           DtThucAn.ColumnChanged += new DataColumnChangeEventHandler(DtThucAn_ColumnChanged);
           DtThucAn.RowDeleted += new DataRowChangeEventHandler(DtThucAn_RowDeleted);
           DtThucAn.Columns["PSNT"].DefaultValue = 0;
           DtThucAn.Columns["Soluong"].DefaultValue = 0;
           DtThucAn.Columns["GiaNT"].DefaultValue = 0;
           DtThucUong.ColumnChanged += new DataColumnChangeEventHandler(DtThucUong_ColumnChanged);
           DtThucUong.RowDeleted += new DataRowChangeEventHandler(DtThucUong_RowDeleted);
           DtThucUong.Columns["PSNT"].DefaultValue = 0;
           DtThucUong.Columns["Soluong"].DefaultValue = 0;
           DtThucUong.Columns["GiaNT"].DefaultValue = 0;
       }

       void DtThucUong_RowDeleted(object sender, DataRowChangeEventArgs e)
       {
           sumtu = 0;
           foreach (DataRow dr in DtThucUong.Rows)
           {
               sumtu += double.Parse(dr["PSNT"].ToString());
           }
           tinhTile();
           sumChange(sumtu, new EventArgs());
       }

       void DtThucAn_RowDeleted(object sender, DataRowChangeEventArgs e)
       {
           sumta = 0;
           foreach (DataRow dr in DtThucAn.Rows)
           {
               sumta += double.Parse(dr["PSNT"].ToString());
           }
           tinhTile();
           sumChange(sumta, new EventArgs());
       }



       void DtThucUong_ColumnChanged(object sender, DataColumnChangeEventArgs e)
       {
           try
           {
               if (e.Column.ColumnName.ToUpper() == "SOLUONG" || e.Column.ColumnName.ToUpper() == "GIANT")
               {
                   e.Row["PSNT"] = double.Parse(e.Row["Soluong"].ToString()) * double.Parse(e.Row["GiaNT"].ToString());
               }
               if (e.Column.ColumnName.ToUpper() == "MAVT")
               {
                   DataRow[] lstdr = dmVt1.Select("MaVT='" + e.Row["MaVT"].ToString() + "'");
                   if (lstdr.Length > 0)
                   {
                       e.Row["GiaNT"] = double.Parse(lstdr[0]["GiaBan"].ToString());
                   }
               }
               if (e.Column.ColumnName.ToUpper() == "PSNT")
               {
                 
                   if(e.Row.RowState==DataRowState.Detached) DtThucUong.Rows.Add(e.Row);
                   sumtu = 0;
                   e.Row.EndEdit();
                   foreach (DataRow dr in DtThucUong.Rows)
                   {
                       sumtu += double.Parse(dr["PSNT"].ToString());
                       
                   }
                   tinhTile();
                   
                  
               }
           }
           catch { }
       }

       void DtThucAn_ColumnChanged(object sender, DataColumnChangeEventArgs e)
       {
           try
           {
               if (e.Column.ColumnName.ToUpper() == "SOLUONG" || e.Column.ColumnName.ToUpper() == "GIANT")
               {
                   e.Row["PSNT"] = double.Parse(e.Row["Soluong"].ToString()) * double.Parse(e.Row["GiaNT"].ToString());
               }
               if (e.Column.ColumnName.ToUpper() == "MAVT")
               {
                   DataRow[] lstdr = dmVt2.Select("MaVT='" + e.Row["MaVT"].ToString() + "'");
                   if (lstdr.Length > 0)
                   {
                       e.Row["GiaNT"] = double.Parse(lstdr[0]["GiaBan"].ToString());
                   }
               }
               if (e.Column.ColumnName.ToUpper() == "PSNT")
               {
                   if (e.Row.RowState == DataRowState.Detached) DtThucAn.Rows.Add(e.Row);
                   sumta = 0;
                   e.Row.EndEdit();
                   foreach (DataRow dr in DtThucAn.Rows)
                   {
                       sumta += double.Parse(dr["PSNT"].ToString());
                   }
                   tinhTile();
                  
               }
           }
           catch { }
       }
       public void tinhTile()
       {
           if (sumta + sumtu != 0)
           {
               tileTU = 100 * sumtu / (sumtu + sumta);
               tileTA = 100 * sumta / (sumtu + sumta);
           }
           else
           {
               tileTA = 0;
               tileTU = 0;
           }
           try
           {
               TongtileTA = 100 * (double.Parse(drmaster["TienTA"].ToString()) + sumta) / double.Parse(drmaster["TTien"].ToString());
               TongtileTU = 100 * (double.Parse(drmaster["TienTU"].ToString()) + sumtu) / double.Parse(drmaster["TTien"].ToString());

           }
           catch { }
           CurTileTA = 100 * sumta / double.Parse(drMaster["TTien"].ToString());
           CurTileTU = 100 * sumtu / double.Parse(drMaster["TTien"].ToString());
           if(sumChange!=null) 
           sumChange(sumta, new EventArgs());
       }
       public bool CreatePTT()
       {
           string sql = "";
           db.BeginMultiTrans();

           Guid id=Guid.NewGuid();

           db.UpdateDatabyStore("TaoPTT", new string[] { "@mt31id", "@TTien", "@id" }, new object[] { drMaster["MT31ID"].ToString(), sumta + sumtu, id });

           //sql = "select mt37id from mt37 where mt31id='" + drMaster["MT31ID"].ToString() +"'";
           //object o = db.GetValue(sql);
           //if (o == null)
           //{
           //    db.RollbackMultiTrans();
          //     db.HasErrors = false;
          //     return false;
           //}
           foreach (DataRow dr in DtThucAn.Rows)
           {
               db.UpdateDatabyStore("taoChitietPTT", new string[] { "@mt37id", "@MaVT", "@Soluong", "@Giant", "@psnt" },
                    new object[] { id.ToString(), dr["MaVT"], dr["Soluong"], dr["GiaNT"], dr["PSNT"] });
               if (db.HasErrors)
               {
                 // db.HasErrors = false;
                   db.RollbackMultiTrans();
                   return false;
               }
           }
           foreach (DataRow dr in DtThucUong.Rows)
           {
               db.UpdateDatabyStore("taoChitietPTT", new string[] { "@mt37id", "@MaVT", "@Soluong", "@Giant", "@psnt" },
                    new object[] { id.ToString(), dr["MaVT"], dr["Soluong"], dr["GiaNT"], dr["PSNT"] });
               if (db.HasErrors)
               {
                  // db.HasErrors = false;
                   db.RollbackMultiTrans();
                   return false;
               }
           }
           if (db.HasErrors)
           {
               //db.HasErrors = false;
               db.RollbackMultiTrans();
               return false;
           }
           else
           {
              // db.HasErrors = false;
               db.EndMultiTrans();
               drmaster["DataoPTT"] = double.Parse(drmaster["DataoPTT"].ToString()) + sumta + sumtu;
               drmaster["TienTA"] = double.Parse(drmaster["TienTA"].ToString()) + sumta;
               drmaster["TienTU"] = double.Parse(drmaster["TienTU"].ToString()) + sumtu;
               drmaster["Conlai"] = double.Parse(drmaster["Ttien"].ToString())- double.Parse(drmaster["DataoPTT"].ToString()) ;
               return true;
           }
           //insert vào phiếu xuất mt37
           //insert vào dt37
           //insert vào bltk
           //insert v
       }

       internal bool checkRule()
       {
          if(bool.Parse(drMaster["Banle"].ToString()))
          {
              if (sumta + sumtu >= 200000) return false;
          }
          else
           if(double.Parse(drMaster["TTien"].ToString())!=sumta+sumtu) return false;
           foreach (DataRow dr in DtThucUong.Rows)
           {
               if (dr["MaVT"] == DBNull.Value)
                   dr.SetColumnError("MaVT", "Phải nhập");
               else dr.SetColumnError("MaVT", string.Empty);
               if (dr["Soluong"] == DBNull.Value || double.Parse(dr["Soluong"].ToString()) == 0)
                   dr.SetColumnError("Soluong", "Phải nhập");
               else dr.SetColumnError("Soluong", string.Empty);
               if (dr["GiaNT"] == DBNull.Value || double.Parse(dr["GiaNT"].ToString()) == 0)
                   dr.SetColumnError("GiaNT", "Phải nhập");
               else dr.SetColumnError("GiaNT", string.Empty);
               if (dr["PSNT"] == DBNull.Value || double.Parse(dr["PSNT"].ToString()) == 0)
                   dr.SetColumnError("PSNT", "Phải nhập");
               else dr.SetColumnError("PSNT", string.Empty);
              
           }
           if (DtThucUong.HasErrors) return false;
           foreach (DataRow dr in DtThucAn.Rows)
           {
               if (dr["MaVT"] == DBNull.Value)
                   dr.SetColumnError("MaVT", "Phải nhập");
               else dr.SetColumnError("MaVT", string.Empty);
               if (dr["Soluong"] == DBNull.Value || double.Parse(dr["Soluong"].ToString()) == 0)
                   dr.SetColumnError("Soluong", "Phải nhập");
               else dr.SetColumnError("Soluong", string.Empty);
               if (dr["GiaNT"] == DBNull.Value || double.Parse(dr["GiaNT"].ToString()) == 0)
                   dr.SetColumnError("GiaNT", "Phải nhập");
               else dr.SetColumnError("GiaNT", string.Empty);
               if (dr["PSNT"] == DBNull.Value || double.Parse(dr["PSNT"].ToString()) == 0)
                   dr.SetColumnError("PSNT", "Phải nhập");
               else dr.SetColumnError("PSNT", string.Empty);

           }
           if (DtThucAn.HasErrors) return false;
           return true;
       }
   }
}
