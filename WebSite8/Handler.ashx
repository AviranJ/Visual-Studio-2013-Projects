<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Threading;

using System.Web.Script.Serialization;

public class Handler : IHttpAsyncHandler {

    public IAsyncResult BeginProcessRequest(HttpContext ctx, AsyncCallback cb, Object obj)
    {
        AsyncResult currentAsyncState = new AsyncResult(ctx, cb, obj);

        ThreadPool.QueueUserWorkItem(new WaitCallback(RequestWorker), currentAsyncState);

        return currentAsyncState;
    }

    private void RequestWorker(Object obj)
    {
        // obj - second parametr in ThreadPool.QueueUserWorkItem()
        AsyncResult myAsyncResult = obj as AsyncResult;
        string command = myAsyncResult._context.Request.QueryString["cmd"];

        switch (command)
        {
            case "load":
                AsyncServer.load(myAsyncResult);
                break;
            case "Move":
                Fraction[] A = new Fraction[17];
                for (int i = 0; i < 16; i++)
                {
                    string JsonString_0 = myAsyncResult._context.Request.QueryString["JSON_Object_"+i];
                    JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
                    A[i] = (Fraction)myJavaScriptSerializer.Deserialize(JsonString_0, typeof(Fraction));
                }
                A[16] = new Fraction();
                string ID = myAsyncResult._context.Request.QueryString["ID"];
                AsyncServer.Move(myAsyncResult, A, ID);
                break;
                
        }
        myAsyncResult.CompleteRequest();
    }



    public void EndProcessRequest(IAsyncResult ar)
    {
    }

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
    }

}