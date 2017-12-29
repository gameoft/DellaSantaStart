using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DellaSanta.Web.HttpModules
{
    //public class PrePostProcessingModule : IHttpModule
    //{
    //    public PrePostProcessingModule()
    //    {
    //        HttpContext.Current.Cache["DateTime"] = DateTime.Now.ToLongTimeString();
    //    }

    //    public void Dispose()
    //    {
            
    //    }

    //    public void Init(HttpApplication context)
    //    {
    //        //https://msdn.microsoft.com/en-us/library/ms227673.aspx
    //        context.BeginRequest += (new EventHandler(this.Application_BeginRequest));
    //        context.EndRequest += (new EventHandler(this.Application_EndRequest));
    //        context.AuthenticateRequest += new EventHandler(authentication_request);
    //        //Matches the HTTP request to a route, retrieves the handler for that route, and sets the handler as the HTTP handler for the current request.
    //        context.PostResolveRequestCache += new EventHandler(Application_Resolve_Request_Cache);

    //        //initiate session
    //        context.AcquireRequestState += new EventHandler(AcquireRequestState);    

    //        HttpContext.Current.Items.Add("M1", "This is module HelloWorld");
    //    }

    //    public void authentication_request(Object sender, EventArgs e)
    //    {
    //        HttpContext Context = ((HttpApplication)sender).Context;
    //        var request = HttpContext.Current.Request;

    //        if (Context.User == null)
    //        {
    //            //User is not authenticated user. So drop request here.
    //            //Context.Response.Write("<h2><font color=red>" +
    //            //                    "Please login before go ahead" +
    //            //                    "</font></h2><hr>");
    //        }

    //        string header = request.Headers["HTTP_AUTHORIZATION"];

    //        string headerAuthToken = request.Headers["X-Mobile"];
    //        if (header != null && header.StartsWith("tokenvalid"))
    //        {
    //            request.Headers.Add("Profile", "Mobile");
    //            HttpContext.Current.Items.Add("MOBILE", "This is mobile");
    //        }
        

    //        //Context.Response.StatusCode = 401;
    //        //Context.Response.StatusCode = 400;
    //        //Context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"Test\"");
    //        //Context.Response.Write("You must authenticate");
    //        //Context.Response.End();

    //        //if (header != null && header.StartsWith("Basic "))
    //        //{
    //        //    // Header is good, let's check username and password
    //        //    string username = DecodeFromHeader(header, "username");
    //        //    string password = DecodeFromHeader(header, "password");

    //        //    if (Validate(username, password)
    //        //    {
    //        //        // Create a custom IPrincipal object to carry the user's identity
    //        //        HttpContext.Current.User = new BasicPrincipal(username);
    //        //    }
    //        //    else
    //        //    {
    //        //        Protect();
    //        //    }
    //        //}
    //        //else
    //        //{
    //        //    Protect();
    //        //}


    //    }

    //    private void Application_BeginRequest(Object source, EventArgs e)
    //    {
    //        // Create HttpApplication and HttpContext objects to access request and response properties.
    //        HttpApplication application = (HttpApplication)source;
    //        HttpContext context = application.Context;
    //        string filePath = context.Request.FilePath;

    //        //string fileExtension = VirtualPathUtility.GetExtension(filePath);
    //        //if (fileExtension.Equals(".aspx"))
    //        //{
    //        //    context.Response.Write("<h1><font color=red>" +
    //        //        "HelloWorldModule: Beginning of Request" +
    //        //        "</font></h1><hr>");
    //        //}
    //        //context.Response.Write("<h1><font color=red>" +
    //        //     "HelloWorldModule: Beginning of Request" +
    //        //     "</font></h1><hr>");

    //        //context.Response.Write("PrePostProcessingModule: Beginning of Request");
    //    }

    //    private void Application_EndRequest(Object source, EventArgs e)
    //    {
    //        HttpApplication application = (HttpApplication)source;
    //        HttpContext context = application.Context;
    //        //string filePath = context.Request.FilePath;
    //        //string fileExtension =
    //        //    VirtualPathUtility.GetExtension(filePath);
    //        //if (fileExtension.Equals(".aspx"))
    //        //{
    //        //    context.Response.Write("<hr><h1><font color=red>" +
    //        //        "HelloWorldModule: End of Request</font></h1>");
    //        //}
    //        //context.Response.Write("<hr><h1><font color=red>" +
    //        //      "HelloWorldModule: End of Request</font></h1>");

    //        //context.Response.Write("PrePostProcessingModule: End of Request");
    //    }

    //    public void Application_Resolve_Request_Cache(Object sender, EventArgs e)
    //    {

    //        HttpContext Context = ((HttpApplication)sender).Context;
    //        //Context.Response.Write("Time is:-" + HttpContext.Current.Cache["DateTime"]);
    //    }

    //    public void AcquireRequestState(Object sender, EventArgs e)
    //    {
    //        HttpContext Context = ((HttpApplication)sender).Context;
    //        //Context.Session.Add("Item", "Session Data");

    //        //Context.Items.Add("Item", "Item Data");

    //        //nel process request dell'httphandler
    //        //if (HttpContext.Current.Items["Item"] != null)
    //        //{
    //        //    context.Response.Write("Page from HttpHandler with Item data " +
    //        //    HttpContext.Current.Items["Item"]);
    //        //}
    //    }

    //}
}