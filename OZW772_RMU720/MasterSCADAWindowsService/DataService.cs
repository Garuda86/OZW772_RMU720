using Newtonsoft.Json;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;

namespace MasterSCADAWindowsService
{


    class OZW772_RMU7201
    {

        const string CacheFolder = @"C:\Program Files (x86)\InSAT\MasterSCADA\OZW772_RMU720\MasterSCADAWindowsService\bin\Debug\Cache\";

        private string uAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
        private string Acpt = "application/json, text/javascript, */*; q=0.01";
        private string googies="";
        //private string URLs = null;
        //private string URI = null; 
        private string URLs1 = "http://10.0.60.2/main.app?SessionId=8d82670f-41e2-4c9f-8602-fe2d4b3ac3a1&section=auth";
        private string log = "Administrator";
        private string pass1 = "Password";
        private string pass = "a987123!";
        string sessionId = "";
        private string sessionIP = null;
        private Boolean b =false;
        private Hashtable hash = new Hashtable();
        //string[] IDs = null;
        public RequestObject requestObject = null;
        SCADAObject[] data;

        object update_lock = new object();
        System.Timers.Timer myTimer = null;


        public Dictionary<string, Hashtable> postWriteValueHash;

        public Dictionary<string, string> postWriteValueResult;

        public bool IsInitialized = false;
        public bool IsInitializing = false;

        class TrustAllCertificatePolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
            {
                return true;
            }
        }



        public void myTimer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            //lock (update_lock)
            {
                if (!IsRefreshing)
                    Parse(requestObject);
            }
        }



        public void Init(string sessionIP, RequestObject requestObject)
        {

            if (IsInitialized || requestObject.IDs == null || requestObject.IDsForRequests == null)
                 return;

            if (requestObject.IDs == null || requestObject.IDsForRequests == null)
            {
                throw new Exception("requestObject params error");
            }

             IsInitializing = true;

             lock (update_lock)
             {

                 this.sessionIP = sessionIP;
                 this.requestObject = requestObject;

                 hash = new Hashtable();

                 ServicePointManager.ServerCertificateValidationCallback
                                 = (obj, certificate, chain, errors) => true;

                 System.Net.ServicePointManager.Expect100Continue = false;

                 System.Net.ServicePointManager.DefaultConnectionLimit = 10;

                 //System.Net.ServicePointManager.SetTcpKeepAlive(false,100,100);

                 ServicePointManager.ServerCertificateValidationCallback
                                            = (obj, certificate, chain, errors) => true;

                 sessionId = posting(requestObject.URLs, googies, uAgent, Acpt, log, pass);


                 Uri uri = new Uri(requestObject.URLs);

                 string preffix = requestObject.URLs.Substring(0, requestObject.URLs.IndexOf("://") + 3);
                 string IP = uri.Host;


                 Hashtable notFound = new Hashtable();
                 var param = "";
                 foreach (string n in requestObject.IDs)
                 {
                     //stranica

                     string result = "";
                     string fileName = CacheFolder + "init-" + preffix.Replace("://","") + IP + "-" + n;
                     
                     if (File.Exists(fileName))
                     {
                         using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                         {
                             byte[] array = new byte[fileStream.Length];
                             // считываем данные
                             fileStream.Read(array, 0, array.Length);
                             // декодируем байты в строку
                             result = System.Text.Encoding.Default.GetString(array);
                         }
                     }
                     
                     if (string.IsNullOrEmpty(result))
                     {
                         result = geting(preffix + IP + "/main.app?SessionId=" + sessionId + "&section=popcard&id=" + n + "&idtype=4", googies, uAgent, Acpt, requestObject.URI);

                         using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                         {
                             var bytes = System.Text.Encoding.Default.GetBytes(result);
                             fileStream.Write(bytes, 0, bytes.Length);
                         }

                     }


                     Regex rst = new Regex(@"id=""dp([^""]+)"">\s*([^""]+)\<\/div\>", RegexOptions.Singleline);

                     foreach (Match m in rst.Matches(result))
                     {
                         if (requestObject.IDsForRequests != null && requestObject.IDsForRequests.Contains(m.Groups[1].Value))
                             hash.Add(m.Groups[1].Value, m.Groups[2].Value);
                         else
                             param += m.Groups[1].Value + ",";
                     }

                 }

                 //if (param != "")
                 //   Console.WriteLine("url=" + requestObject.URLs + "not found params:" + param);

                 int refreshPeriod = 30000;
                 if (myTimer == null)
                 {
                     update_lock = new object();
                     myTimer = new System.Timers.Timer();
                     myTimer.Interval = refreshPeriod;
                     myTimer.AutoReset = true;
                     myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
                     myTimer.Enabled = false;
                 }


                 IsInitialized = true;

                 postWriteValueHash = new Dictionary<string, Hashtable>();
                 postWriteValueResult = new Dictionary<string, string>();


                 Console.WriteLine("sessionIP = " + sessionIP + " initialized");

             }

             IsInitializing = false;

         }

        public void StartUpdating()
        {
            if (!IsInitialized || myTimer.Enabled)
                return;
            myTimer.Enabled = true;
            Console.WriteLine("sessionIP = " + sessionIP + " updating is started");
        }

        public void StopUpdating()
        {
            if (!IsInitialized || !myTimer.Enabled)
                return;
            myTimer.Enabled = false;
            Console.WriteLine("sessionIP = " + sessionIP + " updating is stoped");
        }

        public SCADAObject[] UpdateData(string sessionIP, RequestObject requestObject)
        {
            try
            {
                Hashtable r = outputs(requestObject, requestObject.URL);
                List<SCADAObject> result = new List<SCADAObject>();
                foreach (var key in r.Keys)
                {
                    result.Add(new SCADAObject() { key = key.ToString(), value = r[key].ToString() });
                }

                return result.ToArray();
            } catch (Exception e)
            {
                b = true;
            }
            return null;
        }


        private static void CallHttpWebRequestASyncDataParallelAndWaitOnAll(IEnumerable<RequestState> workItems)
        {
            var coreCount = Environment.ProcessorCount;
            var itemCount = workItems.Count();
            var batchSize = itemCount / coreCount;

            var pending = itemCount;
            using (var mre = new ManualResetEvent(false))
            {
                //for (int batchCount = 0; batchCount < coreCount; batchCount++)
                {
                    //var lower = batchCount * batchSize;
                    //var upper = (batchCount == coreCount - 1) ? itemCount : lower + batchSize;
                    //var workItemsChunk = workItems.Skip(lower).Take(upper).ToArray();
                    var workItemsChunk = workItems;
                    //workItems.First().eventsLog.WriteEntry("  MapService.GetFeatureInfo start sending WFS requests", workItems.First().context.UserName, EventLogEntryType.Information);
                    foreach (var workItem in workItemsChunk)
                    {
                        GetAsync(/*workItem, */workItem.url, workItem.sLocation, workItem.ua, workItem.cooky, workItem.acp, null, callbackState =>
                        {
                            var hwrcbs = (HttpWebRequestCallbackState)callbackState;
                            using (var responseStream = hwrcbs.ResponseStream)
                            {
                                try
                                {
                                    var reader = new StreamReader(responseStream);
                                    //((RequestState)hwrcbs.State).ResponseData = reader.ReadToEnd();
                                    //string ResponseData = reader.ReadToEnd();
                                    workItem.responseData = reader.ReadToEnd(); ;
                                    

                                    RespCallbackSync(workItem, workItem.responseData);
                                }
                                catch(Exception ex)
                                {
                                    if (ex.ToString().ToLower().Contains("denied"))
                                    {
                                        workItem.sessinNeedUpdate = true;
                                    }
                                    Console.WriteLine("url=" + workItem.url  + ":" + ex.ToString());
                                }
                                if (Interlocked.Decrement(ref pending) <= 0)
                                    mre.Set();
                            }
                        }, workItem);
                    }

                    //workItems.First().eventsLog.WriteEntry("  MapService.GetFeatureInfo end sending WFS requests", workItems.First().context.UserName, EventLogEntryType.Information);
                }
                mre.WaitOne();
            }
        }

        private static void RespCallbackSync(RequestState rs, string responseString)
        {
            
            var resultat = responseString;
            rs.hash = new Hashtable();
            Regex r = new Regex(@"plantItemId"":""(\d+)"",""value"":""([^""]+)", RegexOptions.Singleline);
            // "id=""dp([^""]+)"">\s*([^""]+)\<\/div\>"
            foreach (Match m in r.Matches(resultat))
            {
                rs.hash.Add(m.Groups[1].Value, m.Groups[2].Value);
            }            
        }       


        class HttpWebRequestCallbackState
        {
            public Stream ResponseStream { get; private set; }
            public Exception Exception { get; private set; }
            public Object State { get; set; }

            public HttpWebRequestCallbackState(Stream responseStream, object state)
            {
                ResponseStream = responseStream;
                State = state;
            }

            public HttpWebRequestCallbackState(Exception exception)
            {
                Exception = exception;
            }
        }

        static HttpWebRequest CreateHttpWebRequest(string url, string httpMethod, string contentType)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //httpWebRequest.KeepAlive = true;
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = httpMethod;
            return httpWebRequest;
        }

        static void BeginGetRequestStreamCallback(IAsyncResult asyncResult)
        {
            Stream requestStream = null;
            HttpWebRequestAsyncState asyncState = null;
            try
            {
                asyncState = (HttpWebRequestAsyncState)asyncResult.AsyncState;
                //requestStream = asyncState.HttpWebRequest.EndGetRequestStream(asyncResult);
                //requestStream.Write(asyncState.RequestBytes, 0, asyncState.RequestBytes.Length);
                //requestStream.Close();
                asyncState.HttpWebRequest.BeginGetResponse(BeginGetResponseCallback,
                  new HttpWebRequestAsyncState
                  {
                      HttpWebRequest = asyncState.HttpWebRequest,
                      ResponseCallback = asyncState.ResponseCallback,
                      State = asyncState.State
                  });
            }
            catch (Exception ex)
            {
                if (asyncState != null)
                    asyncState.ResponseCallback(new HttpWebRequestCallbackState(ex));
                else
                    throw;
            }
            finally
            {
                if (requestStream != null)
                    requestStream.Close();
            }
        }

        static void BeginGetResponseCallback(IAsyncResult asyncResult)
        {
            WebResponse webResponse = null;
            Stream responseStream = null;
            HttpWebRequestAsyncState asyncState = null;
            try
            {
                asyncState = (HttpWebRequestAsyncState)asyncResult.AsyncState;
                webResponse = asyncState.HttpWebRequest.EndGetResponse(asyncResult);
                
                responseStream = webResponse.GetResponseStream();
                var webRequestCallbackState = new HttpWebRequestCallbackState(responseStream, asyncState.State);
                asyncState.ResponseCallback(webRequestCallbackState);
                responseStream.Close();
                responseStream = null;
                webResponse.Close();
                webResponse = null;
            }
            catch (Exception ex)
            {
                if (asyncState != null)
                    asyncState.ResponseCallback(new HttpWebRequestCallbackState(ex));
                else
                    throw;
            }
            finally
            {
                
                if (responseStream != null)
                    responseStream.Close();
                if (webResponse != null)
                    webResponse.Close();
                
            }
        }

        class HttpWebRequestAsyncState
        {
            //public byte[] RequestBytes { get; set; }
            public HttpWebRequest HttpWebRequest { get; set; }
            public Action<HttpWebRequestCallbackState> ResponseCallback { get; set; }
            public Object State { get; set; }
        }

        static void GetAsync(/*RequestState rs, */string url, string sLocation, string ua, string cooky, string acp, byte[] data, Action<HttpWebRequestCallbackState> responseCallback, object state = null,
                      string contentType = "application/x-www-form-urlencoded")
        {
                        //var myHttpWebRequest = CreateHttpWebRequest(url, "GET", contentType);
            
            HttpWebRequest myHttpWebRequest = null;
            if (myHttpWebRequest == null)
            {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                //rs.request1 = myHttpWebRequest;
                myHttpWebRequest.Method = "GET";
                //myHttpWebRequest.KeepAlive = false;
            }
            
            
            //myHttpWebRequest.KeepAlive = true;
            //myHttpWebRequest.Referer = sLocation;
            //myHttpWebRequest.UserAgent = ua;
            //myHttpWebRequest.Accept = "application/json";
            //myHttpWebRequest.Headers.Add("Accept-Language", "ru");
//            myHttpWebRequest.ContentType = "text/plain";
            if (!String.IsNullOrEmpty(cooky))
            {
                myHttpWebRequest.Headers.Add(HttpRequestHeader.Cookie, cooky);
            }


            //var requestBytes = data;
            //httpWebRequest.ContentLength = requestBytes.Length;

            myHttpWebRequest.BeginGetResponse(BeginGetRequestStreamCallback,
              new HttpWebRequestAsyncState()
              {
                  //RequestBytes = requestBytes,
                  HttpWebRequest = myHttpWebRequest,
                  ResponseCallback = responseCallback,
                  State = state
              });
        }


        //Hashtable hash_r = new Hashtable();


        List<RequestState> requestStates = new List<RequestState>();
 
        ////////////////////////////////////////////////////////////////
        Hashtable outputs(RequestObject requestObject, string ip/*, string uris*/) 
        {
            var hash_r = new Hashtable();
            allDone = new ManualResetEvent(false);
            
            allRequests = hash.Keys.Count;

            //Console.WriteLine("Start update: URL=" + requestObject.URLs);
            DateTime t1 = DateTime.Now;

            string preffix = requestObject.URLs.Substring(0, requestObject.URLs.IndexOf("://") + 3);
                foreach (DictionaryEntry de in hash)
                {
                    if (requestStates.Count < hash.Keys.Count)
                    {
                        RequestState requestState = new RequestState();
                        requestState.id = de.Key.ToString();
                          
                        // Put the request into the state object so it can be passed around.
                        requestState.url = preffix + ip + "/ajax.app?SessionId=" + sessionId + "&service=getDp&plantItemId=" + de.Key;
                        //requestState.sLocation = uris;
                        //requestState.ua = uAgent;
                        //requestState.acp = Acpt;

                        requestStates.Add(requestState);
                    }
                }

                
                /*
                while (allRequests>0)
                {

                }
            */

                


                CallHttpWebRequestASyncDataParallelAndWaitOnAll(requestStates);

                foreach (RequestState resultrequestState in requestStates)
                {
                    try
                    {
                        if (resultrequestState.responseData == null)
                            continue;

                        /*
                        Regex r = new Regex(@"plantItemId"":""(\d+)"",""value"":""([^""]+)", RegexOptions.Singleline);
                        // "id=""dp([^""]+)"">\s*([^""]+)\<\/div\>"
                        foreach (Match m in r.Matches(resultrequestState.responseData))
                        {
                            hash_r.Add(m.Groups[1].Value, m.Groups[2].Value);
                        }
                        */

                        
                        foreach (var r in resultrequestState.hash.Keys)
                        {
                            if (r.ToString() == "476")
                            {
                                Console.WriteLine(resultrequestState.hash[r].ToString().Trim());
                            }

                            if (!hash_r.ContainsKey(r))
                                hash_r.Add(r, resultrequestState.hash[r]);
                        }
                        
                        if (resultrequestState.sessinNeedUpdate)
                        {
                            sessionId = posting(requestObject.URLs, googies, uAgent, Acpt, log, pass);
                            resultrequestState.sessinNeedUpdate = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }

                //Console.WriteLine("End update: URL=" + requestObject.URLs + "time=" + (DateTime.Now - t1));
                
            return hash_r;
        }

        ///////////////////////
       void outputs1()

       {
           float f = 0;
           ServicePointManager.ServerCertificateValidationCallback
                           = (obj, certificate, chain, errors) => true;


           string sessionId = posting(URLs1, googies, uAgent, Acpt, log, pass1);
           string[] IDs = new string[] { "2506", "2521", "2524", "2530", "2645", "2741" };
                                         //inputs; vent;  pump;   kontrol  rejim    PID
           foreach (string n in IDs)
           {

               return;

               //stranica
               string result = geting("http://10.0.60.2/main.app?SessionId=" + sessionId + "&section=popcard&id=2506&idtype=4", googies, uAgent, Acpt, "http://10.0.60.2/main.app?SessionId=092a2169-6ead-4ed7-95ea-cc676197a314&section=popcard&id=2506&idtype=4");
               List<string> results = new List<string>();
               List<float> res = new List<float>();
               int pos = -1;
               pos = result.IndexOf("id=\"dp");
               while (pos >= 0)
               {

                   int pos1 = result.IndexOf("\"", pos);
                   string id = result.Substring(pos + "id=\"dp".Length, pos1 - pos + 1);
                   results.Add(id);
                   pos = result.IndexOf("id=\"dp", pos1);

               }

               foreach (string id in results)
               {
                   result = geting("http://10.0.60.2/ajax.app?SessionId=" + sessionId + "&service=getDp&plantItemId=" + id, googies, uAgent, Acpt, "http://10.0.60.2/main.app?SessionId=092a2169-6ead-4ed7-95ea-cc676197a314&section=popcard&id=2506&idtype=4");
                   
                   string[] par_ei = result.Split(',');
                   string[] param_ei1 = par_ei[3].Split('"', '"');

                   
                   string[] param = result.Split(',');
                   string[] param1 = param[2].Split('"', '"', ':');
                   
                   try
                   {
                       if (param1[3] != "----" && param1[3] != null && !string.IsNullOrEmpty(param1[param1.Length - 2].Trim()))
                       {
                           f = float.Parse(param1[param1.Length - 2].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                           res.Add(f);
                          
                       }
                   }
                   catch (FormatException e) { Console.WriteLine("Формат!:" + param1[param1.Length - 2].Trim()); }
                   foreach (string s in param1)
                   {
                       
                   }

               }


           }
          
       }
      

        //==============================================================
        string geting(string uris, string cooky, string ua, string acp, string sLocation)
        {


            string uri = uris;
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);         
            myHttpWebRequest.Referer = sLocation;
            myHttpWebRequest.UserAgent = ua;
            myHttpWebRequest.Accept = acp;
            myHttpWebRequest.Headers.Add("Accept-Language", "ru");
            myHttpWebRequest.ContentType = "text/html";
            myHttpWebRequest.KeepAlive = true;
            if (!String.IsNullOrEmpty(cooky))
            {
                myHttpWebRequest.Headers.Add(HttpRequestHeader.Cookie, cooky);
            }


            //writeToLog("Start uris=" + uris);

            // выполняем запрос
            using (HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
            
            {
                using (StreamReader myStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), Encoding.GetEncoding(1251)))
                {
                    var result = myStreamReader.ReadToEnd();
                    //writeToLog("End uris=" + uris);
                    return result;
                }
            }




        }

        class RequestState
        {            
            public RequestState()
            {
                hash = new Hashtable();
                sessinNeedUpdate = false;
            }


            public HttpWebRequest request1;
            public HttpWebResponse response;

            public string id;

            /// <summary>
            /// 
            /// </summary>
            public string url;
            /// <summary>
            /// 
            /// </summary>
            public byte[] data;

            /// <summary>
            /// 
            /// </summary>
            public string responseData;        

            public string cooky;
            public string ua;
            public string acp;
            public string sLocation;

            public bool sessinNeedUpdate;
   
            public Hashtable hash;
        }        

        public ManualResetEvent allDone = null;
        const int BUFFER_SIZE = 1024;
        const int DefaultTimeout = 2 * 60 * 1000; // 2 minutes timeout

        // Abort the request if the timer fires.
        private static void TimeoutCallback(object state, bool timedOut)
        {
            if (timedOut)
            {
                HttpWebRequest request = state as HttpWebRequest;
                if (request != null)
                {
                    request.Abort();
                }
            }
        }
        
        
        void FinishWebRequest(IAsyncResult result)
        {


            RequestState myRequestState = (RequestState)result.AsyncState;
            HttpWebRequest myHttpWebRequest = myRequestState.request1;
            myRequestState.response = (HttpWebResponse)myHttpWebRequest.EndGetResponse(result);

            //var myHttpWebResponse = (result.AsyncState as HttpWebRequest).EndGetResponse(result);
            using (StreamReader myStreamReader = new StreamReader(myRequestState.response.GetResponseStream(), Encoding.GetEncoding(1251)))
            {
                var resultat = myStreamReader.ReadToEnd();
                myRequestState.responseData = resultat;
                /*
                Regex r = new Regex(@"plantItemId"":""(\d+)"",""value"":""([^""]+)", RegexOptions.Singleline);
                // "id=""dp([^""]+)"">\s*([^""]+)\<\/div\>"
                foreach (Match m in r.Matches(resultat))
                {
                    myRequestState.hash.Add(m.Groups[1].Value, m.Groups[2].Value);
                }
                 */ 
            }
            allRequests--;
        }

        int allRequests = 0;
        string getingAsync(RequestState myRequestState, string uris, string cooky, string ua, string acp, string sLocation)
        {


            string uri = uris;
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            myHttpWebRequest.Referer = sLocation;
            myHttpWebRequest.UserAgent = ua;
            myHttpWebRequest.Accept = acp;
            myHttpWebRequest.Headers.Add("Accept-Language", "ru");
            myHttpWebRequest.ContentType = "text/plain";
            if (!String.IsNullOrEmpty(cooky))
            {
                myHttpWebRequest.Headers.Add(HttpRequestHeader.Cookie, cooky);
            }


            myRequestState.request1 = myHttpWebRequest;

            IAsyncResult result =
                    (IAsyncResult)myHttpWebRequest.BeginGetResponse(new AsyncCallback(FinishWebRequest), myRequestState);

            return "";

        }
         

        string posting(string uris, string cooky, string ua, string acp, string sLogin, string sPassword)
        {
            
            System.Net.ServicePointManager.Expect100Continue = false;

           // var request = (HttpWebRequest)WebRequest.Create("https://10.0.60.3/main.app?SessionId=c952fa02-ba27-41fc-97ef-0aa6d18060d6");
            //  var request = (HttpWebRequest)WebRequest.Create("http://10.0.60.2/main.app?SessionId=8d82670f-41e2-4c9f-8602-fe2d4b3ac3a1&section=auth");
            var request = (HttpWebRequest)WebRequest.Create(uris);

            var postData = "user=" + sLogin;
            postData += "&pwd=" + sPassword;
            postData += "&login=Login";
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            
            int pos = responseString.IndexOf("name=\"SessionId\"");
            string sessionId = responseString.Substring(pos + "name=\"SessionId\" value=\"".Length, 36);
            //response.Close();
            
            return sessionId;
            }
            
        }
        // POST for write values
        string postWriteValue(string sessionId, string id, string new_value, string ip)
        {

           
            System.Net.ServicePointManager.Expect100Continue = false;

            var request = (HttpWebRequest)WebRequest.Create("http://"+ip+"/dialog.app?SessionId=" + sessionId + "&action=new&id=" + id);//   "id=2717  http://10.0.60.2/dialog.app?SessionId=092a2169-6ead-4ed7-95ea-cc676197a314&action=new&id=2717");
            var response1 = (HttpWebResponse)request.GetResponse();
            var responseString1 = new StreamReader(response1.GetResponseStream()).ReadToEnd();

            Hashtable hash = new Hashtable();
            Regex r = new Regex(@"name=""([^""]+)""\s+value=""([^""]+)""", RegexOptions.Singleline);//id="dp(\d+)"\>\s+([^""]+)\<\/div\>
            foreach (Match m in r.Matches(responseString1))
            {                
                hash.Add(m.Groups[1].Value, m.Groups[2].Value);
            }

            

           string result1 = geting("http://10.0.60.2/dialog.app?SessionId=" + sessionId + "&action=new&id="+id, googies, uAgent, Acpt, "http://10.0.60.2/dialog.app?SessionId=9daaef45-1454-499b-8158-c474ef7ef00f&action=new&id=2717");
          
                      
           var request1 = (HttpWebRequest)WebRequest.Create("http://10.0.60.2/dialog.app?SessionId=" + sessionId);
            //new value of variable
           var postData = "action=" + hash["action"];
            postData += "&DpDescription=" + hash["DpDescription"];
            postData += "&id=" + hash["id"];
            postData += "&min=" + hash["min"];
            postData += "&max=" + hash["max"];
            postData += "&res=" + hash["res"];
            postData += "&decimals=" + hash["decimals"];
            postData += "&unit=" + hash["unit"];
            postData += "&infotext=" + hash["infotext"];
            postData += "&HasOSV=" + hash["HasOSV"];
            postData += "&val=" + new_value;

            //press OK
            postData += "&OK=OK";
            var data = Encoding.ASCII.GetBytes(postData);           
            request1.Method = "POST";
            request1.ContentType = "application/x-www-form-urlencoded";
            request1.ContentLength = data.Length;
            //write to post request
            using (var stream = request1.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response_new = (HttpWebResponse)request1.GetResponse())
            {
                var responseString_new = new StreamReader(response_new.GetResponseStream()).ReadToEnd();

                return "ok";
            }

        } 
  /////////////////////
        public string postWriteValueHttps(RequestObject requestObject, string ip, string id, string new_value)
        {

           
            System.Net.ServicePointManager.Expect100Continue = true;


            string preffix = requestObject.URLs.Substring(0, requestObject.URLs.IndexOf("://") + 3);


            Hashtable hash = null;
            if (postWriteValueHash.ContainsKey(id))
                hash = postWriteValueHash[id];
            if (hash == null)
            {
                string fileName = CacheFolder + "postWriteValueHash-" + sessionIP + "-" + id;
                if (File.Exists(fileName))
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        byte[] array = new byte[fileStream.Length];
                        // считываем данные
                        fileStream.Read(array, 0, array.Length);
                        // декодируем байты в строку
                        string json = System.Text.Encoding.Default.GetString(array);
                        hash = JsonConvert.DeserializeObject<Hashtable>(json);
                    }
                }



                if (hash == null || hash.Count == 0)
                {


                    var request = (HttpWebRequest)WebRequest.Create(preffix + ip + "/dialog.app?SessionId=" + sessionId + "&action=new&id=" + id);
                    request.KeepAlive = true;
                    //request.Headers.Add(HttpRequestHeader.Cookie, "SessionId=" + sessionId);

                    var responseString1 = "";
                    using (var response1 = (HttpWebResponse)request.GetResponse())
                    {
                        var stream = response1.GetResponseStream();
                        responseString1 = new StreamReader(stream).ReadToEnd();
                    }


                    hash = new Hashtable();
                    Regex r = new Regex(@"name=""([^""]+)""\s+value=""([^""]+)""", RegexOptions.Singleline);
                    foreach (Match m in r.Matches(responseString1))
                    {
                        hash.Add(m.Groups[1].Value, m.Groups[2].Value);
                    }

                    if (hash != null && hash.Count > 0)
                    {
                        using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            string json = JsonConvert.SerializeObject(hash);
                            var bytes = System.Text.Encoding.Default.GetBytes(json);
                            fileStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                }

                /*
                IEnumerator hashEnumer = (IEnumerator)hash.GetEnumerator();
                while (hashEnumer.MoveNext())
                {
                    DictionaryEntry de = (DictionaryEntry)hashEnumer.Current;
                }
                */
                if (hash != null && hash.Count > 0)
                    postWriteValueHash.Add(id, hash);
                else
                    Console.WriteLine("hash is empty " + sessionIP + "-" + id);

            }

            string result1 = "";
            if (postWriteValueResult.ContainsKey(id))
            {
                result1 = postWriteValueResult[id];
            }
            else
            {

                string fileName = CacheFolder + "postWriteValueResult-" + sessionIP + "-" + id;
                if (File.Exists(fileName))
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        byte[] array = new byte[fileStream.Length];
                        // считываем данные
                        fileStream.Read(array, 0, array.Length);
                        result1 = System.Text.Encoding.Default.GetString(array);
                    }
                }

                if (string.IsNullOrEmpty(result1))
                {
                    result1 = geting(preffix + ip + "/dialog.app?SessionId=" + sessionId + "&action=new&id=" + id, googies, uAgent, Acpt, preffix + ip + "/dialog.app?SessionId=34e32e0a-72a9-424d-86d6-36f3b63ae8f5&action=new&id=" + id);//"http://10.0.60.2/dialog.app?SessionId=9daaef45-1454-499b-8158-c474ef7ef00f&action=new&id=2717");

                    if (!string.IsNullOrEmpty(result1))
                    { 
                    using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        var bytes = System.Text.Encoding.Default.GetBytes(result1);
                        fileStream.Write(bytes, 0, bytes.Length);
                    } 
                        }
                }
                if (result1 != "")
                    postWriteValueResult.Add(id, result1);
                else
                    Console.WriteLine("result1 is empty " + sessionIP + "-" + id);
            }
         
           var request1 = (HttpWebRequest)WebRequest.Create(preffix + ip + "/dialog.app?SessionId=" + sessionId);
           request1.KeepAlive = true;
           var postData="";
           if (id.Equals("589") || id.Equals("1402") || id.Equals("780") || id.Equals("2024") || id.Equals("2646") || id.Equals("1003") || id.Equals("1001"))
           {
               postData = "action=" + hash["action"];
               postData += "&DpDescription=" + hash["DpDescription"];
               postData += "&id=" + hash["id"];
               
               postData += "&value=" + new_value;

               //press OK
               postData += "&OK=OK";
           }
           else if (id.Equals("686") || id.Equals("687") || id.Equals("689") || id.Equals("690"))
           {
               string[] new_value1 = new_value.Split(':');
               postData = "action=" + hash["action"];
               postData += "&DpDescription=" + hash["DpDescription"];
               postData += "&id=" + hash["id"];
               postData += "&decimals=" + hash["decimals"];
               
               postData += "&infotext=" + hash["infotext"];
               postData += "&IsValidDay=" + "false";
               postData += "&IsValidHour=" + "false";

               postData += "&val_0=" + new_value1[0].Trim();
               postData += "&res_0=" + "1";
               postData += "&min_0=" + "0";
               postData += "&max_0=" + "59";
               postData += "&upper_0=" + "59";
               postData += "&IsValiMin=" + "true";

               postData += "&val_1=" + new_value1[1].Trim();
               postData += "&res_1=" + "1";
               postData += "&min_1=" + "0";
               postData += "&max_1=" + "55";
               postData += "&upper_1=" + "55";
               postData += "&IsValiSec=" + "true";
               postData += "&NrInputs=" + "2";
               //press OK
               postData += "&OK=OK";
           }
           else if (!(id.Equals("686") || id.Equals("687") || id.Equals("689") || id.Equals("690")) && !(id.Equals("589"))) 
           {
               postData = "action=" + hash["action"];
               postData += "&DpDescription=" + hash["DpDescription"];
               postData += "&id=" + hash["id"];
               postData += "&min=" + hash["min"];
               postData += "&max=" + hash["max"];
               postData += "&res=" + hash["res"];
               postData += "&decimals=" + hash["decimals"];
               postData += "&unit=" + hash["unit"];
               postData += "&infotext=" + hash["infotext"];
               postData += "&HasOSV=" + hash["HasOSV"];
               postData += "&val=" + new_value;

               //press OK
               postData += "&OK=OK";
           }
            var data = Encoding.ASCII.GetBytes(postData);           
            request1.Method = "POST";
            request1.ContentType = "application/x-www-form-urlencoded";
            request1.ContentLength = data.Length;
            //write to post request
            using (var stream = request1.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response_new = (HttpWebResponse)request1.GetResponse())
            {
                var responseString_new = new StreamReader(response_new.GetResponseStream()).ReadToEnd();
                response_new.Close();

                return "ok";
            }

        }

        public void writeToLog(string message)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            var time = DateTime.Now;
            message += "; Time=" + time.ToString() + ":" + time.Millisecond;
            logger.Info(message);
        }

        
        public void Parse(RequestObject obj)
        {
            try
            {
                //writeToLog("Start parsing");
                IsRefreshing = true;
                var t1 = DateTime.Now;

                this.data = UpdateData(sessionIP, obj);
                SaveToFile(this.data);
                var t2 = DateTime.Now;
                Console.WriteLine("Data updated: sessionIP=" + sessionIP + "; time=" + (t2 - t1));

            }
            finally
            {
                IsRefreshing = false;
                //writeToLog("End parsing");
            }
        }

        SCADAObject[] LoadFromFile()
        {
            SCADAObject[] results = null;
            try
            {
                string fileName = CacheFolder + @"data-" + sessionIP;
                if (File.Exists(fileName))
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        byte[] array = new byte[fileStream.Length];
                        // считываем данные
                        fileStream.Read(array, 0, array.Length);
                        // декодируем байты в строку
                        string json = System.Text.Encoding.Default.GetString(array);
                        results = JsonConvert.DeserializeObject<SCADAObject[]>(json);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("url=" + requestObject.URLs + ":" + ex.ToString());
            }

            return results;
        }

        void SaveToFile(SCADAObject[] data)
        {
            try
            {
                string fileName = CacheFolder + @"data-" + sessionIP;
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    string json = JsonConvert.SerializeObject(data);
                    var bytes = System.Text.Encoding.Default.GetBytes(json);
                    fileStream.Write(bytes, 0, bytes.Length);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("url=" + requestObject.URLs + ":" + ex.ToString());
            }

        }

        public SCADAObject[] ReadData()
        {
            if (this.data == null && !IsRefreshing)
                this.data = LoadFromFile();
            return data;
        }


        public bool IsRefreshing
        {
            get
            {
                lock (update_lock)
                {
                    return isRefreshing;
                }
            }
            set
            {
                lock (update_lock)
                {
                    isRefreshing = value;
                }
            }
        }

        public bool isRefreshing = false;
        public string ip = "";
        public string uris = "";

    }


    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "DataService" в коде и файле конфигурации.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
    [AspNetCompatibilityRequirements(
           RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DataService : IDataService
    {
        static Dictionary<string,OZW772_RMU7201> objs = new Dictionary<string,OZW772_RMU7201>();

        public void Init(string sessionIP,RequestObject requestObject)
        {
            //foreach (var requestObject in requestObjects)

            {
                OZW772_RMU7201 obj = null;
                if (!objs.ContainsKey(sessionIP))
                {
                    obj = new OZW772_RMU7201();
                    objs.Add(sessionIP, obj);
                }
                else
                    obj = objs[sessionIP];
                if (!obj.IsInitialized)
                    obj.Init(sessionIP, requestObject);
            }

        }


        public SCADAObject[] GetData(string sessionIP, RequestObject requestObject)
        {
            try
            {
                OZW772_RMU7201 obj = GetObject(sessionIP, requestObject.URLs);
                if (obj == null)
                    throw new Exception("Need Initialize");
                //obj.writeToLog("Start GetData ----------------------------------------");
                SCADAObject[] result = obj.ReadData();
                //obj.writeToLog("End GetData ----------------------------------------");
                return result;
                
            }
            catch(Exception ex)
            {

                Console.WriteLine("url=" + requestObject.URLs + ":" + ex.ToString());
                throw ex;
            }
            return null;
        }

        public string PostWriteValueHttps(string sessionIP, RequestObject requestObject, string id, string new_value, string ip)
        {
            OZW772_RMU7201 obj = GetObject(sessionIP, requestObject.URLs);
            if (obj == null)
                throw new Exception("Need Initialize");

            Console.WriteLine("Set value: sessionIP=" + sessionIP + "; id=" + id + "; new_value=" + new_value);
            if (string.IsNullOrEmpty(new_value))
                return "ok";
            //obj.writeToLog("Start PostData ----------------------------------------");
            try { 
            if (!obj.IsInitialized)
                return null;
            var result = obj.postWriteValueHttps(requestObject, requestObject.URL, id, new_value);
            //obj.writeToLog("End PostData ----------------------------------------");
            return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("url=" + requestObject.URLs + ":" + ex.ToString());
            }
            return null;
        }

        public void StartUpdating(string sessionIP, RequestObject requestObject)
        {

            OZW772_RMU7201 obj = GetObject(sessionIP, requestObject.URLs);
            if (obj == null)
                throw new Exception("Need Initialize");
            obj.StartUpdating();
            
        }

        public void StopUpdating(string sessionIP, RequestObject requestObject)
        {

                if (string.IsNullOrEmpty(sessionIP))
                {
                    foreach (var obj1 in objs.Values)
                    {
                        obj1.StopUpdating();
                    }
                    return;
                }
            
                OZW772_RMU7201 obj = GetObject(sessionIP, requestObject.URLs);
                if (obj == null)
                    throw new Exception("Need Initialize");
                obj.StopUpdating();
            
        }



        OZW772_RMU7201 GetObject(string sessionIP, string URLs)
        {
            OZW772_RMU7201 obj = null;
            if (objs.ContainsKey(sessionIP))
                obj = objs[sessionIP];
            return obj;
        }

    }
}
