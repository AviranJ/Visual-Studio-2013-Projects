using System;
using System.Collections.Generic;

using System.Web.Script.Serialization;

public class AsyncServer
{
    
    public static void load(AsyncResult state)
    {
        Fraction[] arrFraction = new Fraction[16];
        Random r = new Random();
        int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        int x;
        for (int i = 0; i < 15; i++)
        {
            x = r.Next(15);
            while (num[x] == 0)
                x = r.Next(15);
            num[x] = 0;
            x++;
            arrFraction[i] = new Fraction(x, r.Next(255), r.Next(255), r.Next(255));
        }
        arrFraction[15]=new Fraction(0, 0, 0, 0);

        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string resultStr = myJavaScriptSerializer.Serialize(arrFraction);
        state._context.Response.Write(resultStr);
    }

    public static void Move(AsyncResult state, Fraction[] A, string ID)
    //       int index = Int32.Parse(((Button)sender).ID);
    //      string temp = arrButtons[0].Text;
    //     arrButtons[0].Text = arrButtons[index].Text;
    //     arrButtons[index].Text = temp;
    {
        int num = Convert.ToInt16(ID);
        int n = 4;
        A[16].setHiddenButton(num);
        if (0 <= num - n)
            if (A[num - n].textvalue==0)
            {
                {
                    A[num - n].R = A[num].R;
                    A[num - n].G = A[num].G;
                    A[num - n].B = A[num].B;
                    A[num - n].textvalue = A[num].textvalue;
                    A[num].textvalue = 0;
                    if (checkwin(A)) A[16].setEndGame();
                    JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
                    string resultStr = myJavaScriptSerializer.Serialize(A);
                    state._context.Response.Write(resultStr);
                    
                    return;
                }
            }
        if (num + n < 16)
        {
            if (A[num + n].textvalue == 0)
            {
                A[num + n].R = A[num].R;
                A[num + n].G = A[num].G;
                A[num + n].B = A[num].B;
                A[num + n].textvalue = A[num].textvalue;
                A[num].textvalue = 0;
                if (checkwin(A)) A[16].setEndGame();
                JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
                string resultStr = myJavaScriptSerializer.Serialize(A);
                state._context.Response.Write(resultStr);
                return;
            }
        }
        if (num + 1 < 16 && num % 4 != 3)
        {
            if (A[num + 1].textvalue == 0)
            {
                A[num + 1].R = A[num].R;
                A[num + 1].G = A[num].G;
                A[num + 1].B = A[num].B;
                A[num + 1].textvalue = A[num].textvalue;
                A[num].textvalue = 0;
                if (checkwin(A)) A[16].setEndGame();
                JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
                string resultStr = myJavaScriptSerializer.Serialize(A);
                state._context.Response.Write(resultStr);
                return;
            }
        }
        if (0 <= num - 1 && num % 4 != 0)
        {
            if (A[num - 1].textvalue == 0)
            {
                A[num - 1].R = A[num].R;
                A[num - 1].G = A[num].G;
                A[num - 1].B = A[num].B;
                A[num - 1].textvalue = A[num].textvalue;
                A[num].textvalue = 0;
                if (checkwin(A)) A[16].setEndGame();
                JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
                string resultStr = myJavaScriptSerializer.Serialize(A);
                state._context.Response.Write(resultStr);
                return;
            }
        }
        JavaScriptSerializer myJavaScriptSerializer1 = new JavaScriptSerializer();
        string resultStr1 = myJavaScriptSerializer1.Serialize(A);
        state._context.Response.Write(resultStr1);


    }

    protected static bool checkwin(Fraction[] A)
    {

        return (A[0].textvalue == 1 && A[1].textvalue == 2);
    }



}
