using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected Button[] arrButtons;
    protected int n = 4;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Table Table1 = new Table();
        this.form1.Controls.Add(Table1);
        arrButtons = new Button[16];
        short i;
        for (i = 0; i < 16; i++)
        {
            arrButtons[i] = new Button();
            arrButtons[i].ID = i.ToString();
            arrButtons[i].Font.Size = new FontUnit("X-Large");
            arrButtons[i].Visible = true;
            this.form1.Controls.Add(arrButtons[i]);
        }
        
    }

}