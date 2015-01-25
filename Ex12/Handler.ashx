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
        string guid = myAsyncResult._context.Request.QueryString["guid"];

        switch (command)
        {
            case "load":
                string endgame = myAsyncResult._context.Request.QueryString["endgame"];
                bool end;
                if (endgame == "true")
                    end = true;
                else
                    end = false;
                    
                AsyncServer.load(myAsyncResult,end,guid);
                myAsyncResult.CompleteRequest();
                break;
            case "Move":
                string ID = myAsyncResult._context.Request.QueryString["ID"];
                AsyncServer.Move(myAsyncResult,ID, guid);
                myAsyncResult.CompleteRequest();
                break;
            case "register":
                AsyncServer.RegisterClient(myAsyncResult);
                myAsyncResult.CompleteRequest();
                break;
            case "unregister":
                AsyncServer.UnregisterClient(myAsyncResult,guid);
                myAsyncResult.CompleteRequest();
                break;
            case "process":
                if (guid != null)
                {
                    AsyncServer.UpdateClient(myAsyncResult, guid);
                }
                break;
                
        }
        
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