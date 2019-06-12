using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WQH.Web
{
    public class Page
    {
        public static void clean_panel(Panel pan)
        {
            ControlCollection cc = pan.Controls;

            foreach (Control ct in cc)
            {
                if (ct is TextBox)
                {
                    ((TextBox)ct).Text = "";
                }
                else if (ct is HiddenField)
                {
                    ((HiddenField)ct).Value = "";
                }
                else if (ct is Image)
                {
                    ((Image)ct).ImageUrl = "";
                }
                else if (ct is CheckBox)
                {
                    ((CheckBox)ct).Checked = false;
                }
                else if (ct is GridView)
                {
                    ((GridView)ct).Visible = false;
                }
            }
        }
    }
}
