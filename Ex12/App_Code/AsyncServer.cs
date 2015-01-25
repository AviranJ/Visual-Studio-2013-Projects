using System;
using System.Collections.Generic;

using System.Web.Script.Serialization;

public class AsyncServer
{
    private static Object _lock = new Object();

    private static List<AsyncResult> _clientStateList = new List<AsyncResult>();

    private static Fraction[] arrFraction = new Fraction[17];

    public static void load(AsyncResult state, bool end, string guid)
    {

        lock (_lock)
        {
            if (arrFraction[0] == null || end) {

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
            arrFraction[15] = new Fraction(0, 0, 0, 0);
            arrFraction[16] = new Fraction();

            Update(state, guid);
            }

            JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
            string resultStr = myJavaScriptSerializer.Serialize(arrFraction);
            state._context.Response.Write(resultStr);

        }


     //   state._context.Response.Write(resultStr);
    }



    public static void Move(AsyncResult state, string ID ,string guid)
    {
        JavaScriptSerializer myJavaScriptSerializer;
        string resultStr;
        int num = Convert.ToInt16(ID);
        int n = 4;
        arrFraction[16].setHiddenButton(num);
        if (0 <= num - n)
            if (arrFraction[num - n].textvalue == 0)
            {
                {
                    arrFraction[num - n].R = arrFraction[num].R;
                    arrFraction[num - n].G = arrFraction[num].G;
                    arrFraction[num - n].B = arrFraction[num].B;
                    arrFraction[num - n].textvalue = arrFraction[num].textvalue;
                    arrFraction[num].textvalue = 0;
                    if (checkwin(arrFraction)) { arrFraction[16].setEndGame(); arrFraction[16].guid = guid; }
                    myJavaScriptSerializer = new JavaScriptSerializer();
                    resultStr = myJavaScriptSerializer.Serialize(arrFraction);
                    state._context.Response.Write(resultStr);
                    if (!checkwin(arrFraction)) Update(state, guid);
                    return;
                }
            }
        if (num + n < 16)
        {
            if (arrFraction[num + n].textvalue == 0)
            {
                arrFraction[num + n].R = arrFraction[num].R;
                arrFraction[num + n].G = arrFraction[num].G;
                arrFraction[num + n].B = arrFraction[num].B;
                arrFraction[num + n].textvalue = arrFraction[num].textvalue;
                arrFraction[num].textvalue = 0;
                if (checkwin(arrFraction)) { arrFraction[16].setEndGame(); arrFraction[16].guid = guid; }
                myJavaScriptSerializer = new JavaScriptSerializer();
                resultStr = myJavaScriptSerializer.Serialize(arrFraction);
                state._context.Response.Write(resultStr);

                if (!checkwin(arrFraction)) Update(state, guid);
                return;
            }
        }
        if (num + 1 < 16 && num % 4 != 3)
        {
            if (arrFraction[num + 1].textvalue == 0)
            {
                arrFraction[num + 1].R = arrFraction[num].R;
                arrFraction[num + 1].G = arrFraction[num].G;
                arrFraction[num + 1].B = arrFraction[num].B;
                arrFraction[num + 1].textvalue = arrFraction[num].textvalue;
                arrFraction[num].textvalue = 0;
                if (checkwin(arrFraction)) { arrFraction[16].setEndGame(); arrFraction[16].guid = guid; }
                myJavaScriptSerializer = new JavaScriptSerializer();
                resultStr = myJavaScriptSerializer.Serialize(arrFraction);
                state._context.Response.Write(resultStr);
                if (!checkwin(arrFraction)) Update(state, guid);
                return;
            }
        }
        if (0 <= num - 1 && num % 4 != 0)
        {
            if (arrFraction[num - 1].textvalue == 0)
            {
                arrFraction[num - 1].R = arrFraction[num].R;
                arrFraction[num - 1].G = arrFraction[num].G;
                arrFraction[num - 1].B = arrFraction[num].B;
                arrFraction[num - 1].textvalue = arrFraction[num].textvalue;
                arrFraction[num].textvalue = 0;
                if (checkwin(arrFraction)) { arrFraction[16].setEndGame(); arrFraction[16].guid = guid; }
                myJavaScriptSerializer = new JavaScriptSerializer();
                resultStr = myJavaScriptSerializer.Serialize(arrFraction);
                state._context.Response.Write(resultStr);
                if (!checkwin(arrFraction)) Update(state, guid);
                return;
            }
        }
        myJavaScriptSerializer = new JavaScriptSerializer();
        resultStr = myJavaScriptSerializer.Serialize(arrFraction);
        state._context.Response.Write(resultStr);
        Update(state, guid);


    }
    protected static bool checkwin(Fraction[] A)
    {

        return (A[0].textvalue == 1 && A[1].textvalue == 2);
    }
    public static void Update(AsyncResult state, String guid)
    {
        for (int i = 0; i < _clientStateList.Count; i++)
        {
            if (_clientStateList[i] != null)
            {
                try
                {
                    JavaScriptSerializer myJavaScriptSerializer1 = new JavaScriptSerializer();
                    string resultStr1 = myJavaScriptSerializer1.Serialize(arrFraction);
                    _clientStateList[i]._context.Response.Write(resultStr1);
                    _clientStateList[i].CompleteRequest();
                }
                catch { }
            }
        }
    }

    public static void UpdateClient(AsyncResult state, String guid)
    {
        lock (_lock)
        {
            AsyncResult clientState = _clientStateList.Find(
                delegate(AsyncResult cs)
                {
                    return cs.ClientGuid == guid;
                }
            );
            if (clientState!=state && clientState != null)
            {
                clientState._context = state._context;
                clientState._state = state._state;
                clientState._callback = state._callback;
            }



        }
    }


    public static void RegisterClient(AsyncResult state)
    {
        lock (_lock)
        {
            state.ClientGuid = Guid.NewGuid().ToString();
            _clientStateList.Add(state);
            state._context.Response.Write(state.ClientGuid.ToString());


        }
    }

    public static void UnregisterClient(AsyncResult state, string guid)
    {
        lock (_lock)
        {
            if (_clientStateList.Count > 0)
                for (int i = 0; i < _clientStateList.Count;i++ )
                {
                    if (_clientStateList[i].ClientGuid == guid)
                        _clientStateList.Remove(_clientStateList[i]);
                }
        }
    }



}
