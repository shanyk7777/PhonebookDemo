using NotificationCenterFE.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace NotificationCenterFE.Controllers
{
    public class HomeController : Controller
    {
        //public async Task<ActionResult> Index([FromBody] Token tok)
        //{
        //    try
        //    {
        //        ViewBag.MyData = "shany";
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        //ViewBag.aa = (string)HttpContext.Session["_identityJson"];
        //        return View("~/Views/Error/Index.cshtml");
        //    }
        //}


        //[EnableCors("*")]
        [EnableCors(origins:"*",headers:"*",methods:"*")]
        [HttpPost]
        public async Task<ActionResult> Index([FromBody] Azure_ad_token tok) // diff from old service - " [FromBody] Token tok "
       // public async Task<ActionResult> Index(string tok) 
        {
            try
            {

                //if (tok.token == null)
                //{
                //    tok.token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Imwzc1EtNTBjQ0g0eEJWWkxIVEd3blNSNzY4MCJ9.eyJhdWQiOiIwZWJjNDVhYi0wZDk2LTRiODItYTAwOS1lOTY4NjcxZDk0MDciLCJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vZDlkM2QzZmYtNmMwOC00MGNhLWE0YTktYWVmYjg3M2VjMDIwL3YyLjAiLCJpYXQiOjE2Mzg4MDMzMDAsIm5iZiI6MTYzODgwMzMwMCwiZXhwIjoxNjM4ODA3NTYzLCJhaW8iOiJBVlFBcS84VEFBQUFXZHladDVCVnVsTmJ3K1E5ZWNrMnZEODVPYkV3UzBnWTdIekRVd1d5cy9XK0VtNVFGNDFzSURmeExPbWl5VHdKdHRHclh2UXA5OVFLNkxqYlJTbW54enoyZFVZWGxmK09zQkhIeFpsSHVrdz0iLCJhenAiOiIxZmVjOGU3OC1iY2U0LTRhYWYtYWIxYi01NDUxY2MzODcyNjQiLCJhenBhY3IiOiIwIiwibmFtZSI6IlNIQU5ZIEtBUkVMSVRaIiwib2lkIjoiMDkyZjQ5ZTgtNWRlZC00MmM5LTgyNTEtNzQ1MWM4NGNhMTEwIiwicHJlZmVycmVkX3VzZXJuYW1lIjoiU0hBTllLQHJhZmFlbC5jby5pbCIsInJoIjoiMC5BVWdBXzlQVDJRaHN5a0NrcWE3N2h6N0FJSGlPN0Jfa3ZLOUtxeHRVVWN3NGNtUklBQ1kuIiwic2NwIjoiYWNjZXNzX2FzX3VzZXIiLCJzdWIiOiJpc2UydWJLRG05b1oxcWh1REdER2RlenM5S3JOWm9NWlV6WUNTc096a0FJIiwidGlkIjoiZDlkM2QzZmYtNmMwOC00MGNhLWE0YTktYWVmYjg3M2VjMDIwIiwidXRpIjoib2ZTSGNUbWt6ay1xLURmVUE5MjVBQSIsInZlciI6IjIuMCJ9.ZRIlhjminE16srok5dj-btz8FKv2WFysl0z3X71hHxeG0L68vW6cdo68SaxFt9UBlGYtTKccmYEh9nIIctVGlwbeYiK4sUbIgQmWnPPojvVI3GO7HVskrt1dHSuZslZIG1k3igRNNMCQdkz92OYAgtpYz5_vlmhdgooFNA-_rzt2EuiR34nzpvI0ZUGfqQ26eueUMyYWDKXUKtUVEXnZRfYxV_UY4HJT4QISdnVZM5WfipFVD2lOBijRg32QnjUGX9B1VhLSZn0IT8lvRPaI2o7pTliZoIe6Sj2Oi4ZpgkyJr-dkh-Cb-Zd2rH2iOpUT5A4mD_3p5-PQ42884Vl-xQ";
                if(!string.IsNullOrEmpty(tok.token))
                {
                    Azure_ad_token aat = new Azure_ad_token { token = tok.token };

                    string trex2af_url = ConfigurationManager.AppSettings["trex2af_url"];
                    string function = ConfigurationManager.AppSettings["function"];

                    using (var httpClient = new HttpClient())
                    {
                        //httpClient.DefaultRequestHeaders.Add("x-functions-key", "LA0uQAHPFX6BuaSR2AHkCUxVtNp5PUj4/vzlSF8beSCJq8OppsqbHw==");
                        httpClient.DefaultRequestHeaders.Add("x-functions-key", function);
                        //var response = await httpClient.PostAsync("https://trex2af.azurewebsites.net/api/validate",
                        var response = await httpClient.PostAsync(trex2af_url, aat, new JsonMediaTypeFormatter());
                        //Response.AppendHeader("Access-Control-Allow-Origin", "*");
                        //response.setHeader("Access-Control-Allow-Origin", "*");
                        var result = response.Content.ReadAsStringAsync().Result;

                        Session["userName"] = result;
                        ViewBag.MyData = result;
                    }


                    //var v = tok.preferred_username;
                    //var userName = await Trex2_ConnectedUser(tok); // Azure AD Auth



                    //var userName = "SHANYK";
                    //var userName = ConnectedUser(); // On Prem
                    //Session["userName"] = userName;
                    //ViewBag.MyData = userName;
                    ////if(string.IsNullOrEmpty(userName))
                    ////{
                    ////    //ViewBag.aa = (string)HttpContext.Session["_identityJson"];
                    ////    return View("~/Views/Error/Index.cshtml");
                    ////}
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.aa = (string)HttpContext.Session["_identityJson"];
                return View("~/Views/Error/Index.cshtml");
            }
        }


        //private async Task<string> Trex2_ConnectedUser(Token t) // diff from old service
        //{
        //    try
        //    {
        //        //Token t = new Token();
        //        //t.preferred_username = "SHANYK1@rafael.co.il";
        //        var result = "FAILED";
        //        string trex_url = ConfigurationManager.AppSettings["trex_url"];

        //        if (t.preferred_username != null)
        //        {
        //            using (var httpClient = new HttpClient())
        //            {
        //                var response = await httpClient.PostAsync(trex_url, t, new JsonMediaTypeFormatter());
        //                result = response.Content.ReadAsStringAsync().Result;
        //                Session["userName"] = result;
        //                ViewBag.MyData = result;
        //            }
        //        }
        //        //Session["userName"] = result;
        //        //ViewBag.MyData = result;
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "FAILED - catch"; // todo: handle exception
        //    }
        //}


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}