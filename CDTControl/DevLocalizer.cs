using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraReports.Localization;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;

using CDTLib;

namespace CDTControl
{
    public class DevLocalizer
    {
        public class MyGridLocalizer : GridLocalizer
        {
            Hashtable hash = Translation.GetDictionary("GRD");
            public override string GetLocalizedString(GridStringId id)
            {
                int k = id.GetHashCode();
                string str = string.Empty;
                if (hash.Contains(k))
                    return hash[k].ToString();
                else
                {
                    Translation.updateDictionary("GRD", k.ToString(), base.GetLocalizedString(id).Replace("'", "`"));
                    hash.Add(k, base.GetLocalizedString(id));
                    return base.GetLocalizedString(id);
                }
            }
        }

        public class MyLocalizer : Localizer
        {

            Hashtable hash = Translation.GetDictionary("EDT");
            public override string GetLocalizedString(StringId id)
            {
                int k = id.GetHashCode();
                if (hash.Contains(k))
                    return hash[k].ToString();
                else
                {
                    Translation.updateDictionary("EDT", k.ToString(), base.GetLocalizedString(id).Replace("'", "`"));
                    hash.Add(k, base.GetLocalizedString(id));
                    return base.GetLocalizedString(id);
                }
            }
        }

        public class MyReportLocalizer : ReportLocalizer
        {

            Hashtable hash = Translation.GetDictionary("UDRPT");
            public override string GetLocalizedString(ReportStringId id)
            {
                int k = id.GetHashCode();
                if (hash.Contains(k))
                    return hash[k].ToString();
                else
                {
                    Translation.updateDictionary("UDRPT", k.ToString(), base.GetLocalizedString(id).Replace("'","`"));
                    hash.Add(k, base.GetLocalizedString(id));
                    return base.GetLocalizedString(id);
                }
            }
        }
           
        public class MyPreviewLocalizer : PreviewLocalizer
        {

            Hashtable hash = Translation.GetDictionary("PRV");
            public override string GetLocalizedString(PreviewStringId id)
            {
                int k = id.GetHashCode();
                if (hash.Contains(k))
                    return hash[k].ToString();
                else
                {
                    Translation.updateDictionary("PRV", k.ToString(), base.GetLocalizedString(id).Replace("'", "`"));
                    hash.Add(k, base.GetLocalizedString(id));
                    return base.GetLocalizedString(id);
                }
            }
        }

        public static void Translate(Control control)
        {
            if (control.HasChildren)
            {
                if ((control as XtraForm) != null)
                    control.Text = UIDictionary.Translate(control.Text);
                if (control.GetType() == typeof(LayoutControl))
                {
                    LayoutControl lc = control as LayoutControl;
                    for (int i = 0; i < lc.Items.Count; i++)
                        if (lc.Items[i].GetType() == typeof(LayoutControlItem))
                            lc.Items[i].Text = UIDictionary.Translate(lc.Items[i].Text);
                }
                if (control.GetType() == typeof(GridControl))
                {
                    GridControl gc = control as GridControl;
                    GridView gv = gc.Views[0] as GridView;
                    for (int i = 0; i < gv.Columns.Count; i++)
                        gv.Columns[i].Caption = UIDictionary.Translate(gv.Columns[i].Caption);
                }
                if (control.GetType() == typeof(LookUpEdit))
                    (control as LookUpEdit).Properties.NullText = UIDictionary.Translate((control as LookUpEdit).Properties.NullText);
                try
                {
                    control.Text = UIDictionary.Translate(control.Text);
                }
                catch { }
                foreach (Control child in control.Controls)
                    Translate(child);
            }
            else
            {
                Type ctrlType = control.GetType();
                if (ctrlType == typeof(SimpleButton) || ctrlType == typeof(CheckEdit) || ctrlType == typeof(GroupControl) || ctrlType == typeof(LabelControl) || ctrlType == typeof(Label) || ctrlType == typeof(Button))
                    control.Text = UIDictionary.Translate(control.Text);
                if (ctrlType == typeof(RadioGroup))
                {
                    RadioGroup rg = control as RadioGroup;
                    for (int i = 0; i < rg.Properties.Items.Count; i++)
                        rg.Properties.Items[i].Description = UIDictionary.Translate(rg.Properties.Items[i].Description);
                }
            }
            
        }
    }
}
