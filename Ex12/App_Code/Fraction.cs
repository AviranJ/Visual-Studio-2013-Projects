using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Fraction
/// </summary>

[Serializable]
public class Fraction
{
    public int textvalue;
    public int R, G, B;
    public bool EndGame;
    public int HiddenButton;
    public string guid;

    public Fraction()
    {
    }
    public Fraction(int n, int r, int g, int b)
    {
        EndGame = false;
        HiddenButton = 0;
        textvalue = n; R = r; G = g; B = b;
    }

    public void setEndGame()
    {
        EndGame = true;
    }

    public bool getEndGame()
    {
        return EndGame;
    }

    public void setHiddenButton(int num)
    {
        HiddenButton = num;
    }
}

