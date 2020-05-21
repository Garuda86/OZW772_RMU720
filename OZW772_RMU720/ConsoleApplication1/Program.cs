using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string pass1 = "Administrator";
            string pass = "Password";


            string sessionId = posting("http://10.0.60.2/main.app?SessionId=8743aaba-14bd-423a-91e4-6a80bddfbcf5&section=auth", "", "", "", pass1, pass);

            var request = (HttpWebRequest)WebRequest.Create("http://10.0.60.2/dialog.app?SessionId=" + sessionId + "&action=new&id=2533");
                                                             //http://10.0.60.2/dialog.app?SessionId=27feb3af-743c-4cae-afc0-861e30979718&action=new&id=2533
                                                             //http://10.0.60.2/dialog.app?SessionId=73338f35-b64a-46fb-bed2-434f81d72412&action=new&id=2533

            //var request = (HttpWebRequest)WebRequest.Create("http://yandex.ru");

            //request.Referer = "http://10.0.60.2/";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            request.Headers.Add(HttpRequestHeader.Cookie, "SessionId=" + sessionId);

            var responseString1 = "";
            using (var response1 = (HttpWebResponse)request.GetResponse())
            {
                var stream = response1.GetResponseStream();
                responseString1 = new StreamReader(stream).ReadToEnd();
            }

        }

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

        static string posting(string uris, string cooky, string ua, string acp, string sLogin, string sPassword)
        {

            System.Net.ServicePointManager.Expect100Continue = false;

            // var request = (HttpWebRequest)WebRequest.Create("https://10.0.60.3/main.app?SessionId=c952fa02-ba27-41fc-97ef-0aa6d18060d6");
            //  var request = (HttpWebRequest)WebRequest.Create("http://10.0.60.2/main.app?SessionId=8d82670f-41e2-4c9f-8602-fe2d4b3ac3a1&section=auth");
            var request = (HttpWebRequest)WebRequest.Create(uris);
            request.KeepAlive = true;

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
                response.Close();

                return sessionId;
            }

        }
    }
}
