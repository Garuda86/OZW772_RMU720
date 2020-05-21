using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using FB;
using InSAT.Library.Interop;
using System.ComponentModel;
using System.Globalization;

namespace OZW_100606
{
    [Serializable,
    ComVisible(true),
    Guid("C4E90516-4488-4A15-AE34-2B28DE9B463E"),
    CatID(CatIDs.CATID_OTHER),
    DisplayName("OZW_100606_FB"),
    ControllerCode(54),
    FBOptions(FBOptions.UseScanByTime)]

    public class OZW_100606 : StaticFBBase
    {
        private static string uAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
        private static string Acpt = "application/json, text/javascript, */*; q=0.01";
        private static string googies = "";
        private static string URLs = null;
        private static string URI = null;
        private static string URLs1 = "http://10.0.60.2/main.app?SessionId=8d82670f-41e2-4c9f-8602-fe2d4b3ac3a1&section=auth";
        private static string log = "Administrator";
        private static string pass1 = "Password";
        private static string pass = "a987123!";
        private static string sessionId = null;
        private static string sessionIP = null;
        private static Boolean b = false;
        private static Hashtable hash = new Hashtable();

        public class TrustAllCertificatePolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
            {
                return true;
            }
        }

        protected override void ToRuntime()
        {

            b = true;
            ServicePointManager.ServerCertificateValidationCallback
                            = (obj, certificate, chain, errors) => true;


        }

        protected override void ToDesign() { }

        protected override void UpdateData()
        {
            const int Pin_IP = 46;
            sessionIP = GetPinValue(Pin_IP).ToString();
            if (b)
            {


                URLs = "https://10.0.60.6/main.app?SessionId=70a0e649-7310-44c4-af6c-be66b39fe255&section";
                /*if (sessionIP.Equals("10.0.60.3")) { URLs = "https://10.0.60.3/main.app?SessionId=c952fa02-ba27-41fc-97ef-0aa6d18060d6"; }
                else if (sessionIP.Equals("10.0.60.6")) { URLs = "https://10.0.60.6/main.app?SessionId=70a0e649-7310-44c4-af6c-be66b39fe255&section"; }
               else if (sessionIP.Equals("10.0.60.10")) { URLs = "https://10.0.60.10/main.app?SessionId=511d4201-94dc-4db4-a0f3-92fdf39793ba"; }*/

                URI = "https://10.0.60.6/main.app?SessionId=f899bef9-e644-4f0a-8008-d8743e490141&section=popcard&id=863&idtype=4"; ;
                /* if (sessionIP.Equals("10.0.60.3")) { URI = "https://10.0.60.3/main.app?SessionId=dd658316-a9c5-4d14-98ec-55c0b4be2252&section=popcard&id=449&idtype=4";  }
             else if (sessionIP.Equals("10.0.60.6")) { URI = "https://10.0.60.6/main.app?SessionId=f899bef9-e644-4f0a-8008-d8743e490141&section=popcard&id=863&idtype=4";  }
              else if (sessionIP.Equals("10.0.60.10")){ URI = "https://10.0.60.10/main.app?SessionId=511d4201-94dc-4db4-a0f3-92fdf39793ba&section=popcard&id=863&idtype=4"; }*/
                //получаем ИД сессии
                ServicePointManager.ServerCertificateValidationCallback
                               = (obj, certificate, chain, errors) => true;
                sessionId = posting(URLs, googies, uAgent, Acpt, log, pass);

                string[] IDs = new string[6];

                IDs = new string[] { "863", "878", "881", "886", "1000", "1091" };
                /*if (sessionIP.Equals("10.0.60.3")) { IDs = new string[] { "449", "464", "467", "473", "588", "684" }; }
                else if (sessionIP.Equals("10.0.60.6")||sessionIP.Equals("10.0.60.10")) { IDs = new string[] { "863", "878", "881", "886", "1000", "1091" }; }*/
                // else if (sessionIP.Equals("10.0.60.10")) { IDs = new string[] { "863", "878", "881", "886", "1000", "1091" }; }
                //inpu; vent;  pump; kontrol; rejim;  PID

                foreach (string n in IDs)
                {
                    //stranica
                    string result = geting("https://" + "10.0.60.6" + "/main.app?SessionId=" + sessionId + "&section=popcard&id=" + n + "&idtype=4", googies, uAgent, Acpt, URI);

                    Regex rst = new Regex(@"id=""dp([^""]+)"">\s*([^""]+)\<\/div\>", RegexOptions.Singleline);

                    foreach (Match m in rst.Matches(result))
                    {
                        hash.Add(m.Groups[1].Value, m.Groups[2].Value);
                    }

                }

            }


            Hashtable r = outputs(sessionIP, URI);
            //read data
                        
                  SetPinValue(16, float.Parse(r["864"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));//"863", "878", "881", "884", "886", "1000", "1091" 
              SetPinValue(17, float.Parse(r["865"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(18, float.Parse(r["867"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(19, float.Parse(r["868"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(20, float.Parse(r["869"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(21, float.Parse(r["870"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(22, float.Parse(r["871"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(23, float.Parse(r["872"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));

              if (r["879"].Equals("Off")) {
                  SetPinValue(24, 0);
              }
              else { SetPinValue(24, 1); }

              if (r["880"].Equals("Off"))
              {
                  SetPinValue(25, 0);
              }
              else { SetPinValue(25, 1); }

              SetPinValue(48, float.Parse(r["882"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(49, float.Parse(r["883"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat)); //"449", "464", "467", "470", "473", "588", "684"
              SetPinValue(26, 0);
              double d = 0;

              if (double.TryParse(r["886"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
              {
                SetPinValue(27,(float)(d));

              }
              else { SetPinValue(27, 0); }


              SetPinValue(28, float.Parse(r["887"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(29, float.Parse(r["888"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(30, float.Parse(r["889"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(31, float.Parse(r["890"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(32, float.Parse(r["891"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(33, float.Parse(r["892"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(34, float.Parse(r["893"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
          
              if (double.TryParse(r["896"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
              {
                  SetPinValue(35, (float)(d));

              }
              else { SetPinValue(35, 0); }


              SetPinValue(36, float.Parse(r["899"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(37, float.Parse(r["900"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(38, 0);
              SetPinValue(39, r["1001"]);
              SetPinValue(40, float.Parse(r["1092"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(41, r["1093"]);
              SetPinValue(42, r["1094"]);
              SetPinValue(43, float.Parse(r["1095"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
              SetPinValue(44, r["1096"]);
              SetPinValue(45, r["1097"]);
              


            //write data
            if (b)
            {
                SetPinValue(1, 20.0);
                SetPinValue(2, 20.0);
                SetPinValue(3, 20.0);
                SetPinValue(4, 20.0);
                SetPinValue(5, 20.0);
                SetPinValue(6, 20.0);
                SetPinValue(7, 35.0);
                SetPinValue(8, 16.0);
                SetPinValue(9, "4");
                SetPinValue(10, 20.0);
                SetPinValue(11, "02:30");
                SetPinValue(12, "00:00");
                SetPinValue(13, 20.0);
                SetPinValue(14, "02:30");
                SetPinValue(15, "00:00");
            }/*
            else
            {
                double ecsp = GetPinDouble(28);

                if ((GetPinDouble(1) != ecsp && !b))
                {
                    postWriteValueHttps(sessionId, "476", GetPinValue(1).ToString(), "10.0.60.3");
                }


                double pcsp = GetPinDouble(29);

                if ((GetPinDouble(2) != pcsp && !b))
                {
                    postWriteValueHttps(sessionId, "477", GetPinValue(2).ToString(), "10.0.60.3");
                }

                double ccsp = GetPinDouble(30);

                if ((GetPinDouble(3) != ccsp && !b))
                {
                    postWriteValueHttps(sessionId, "478", GetPinValue(3).ToString(), "10.0.60.3");
                }
                double chsp = GetPinDouble(31);

                if ((GetPinDouble(4) != chsp && !b))
                {
                    postWriteValueHttps(sessionId, "479", GetPinValue(4).ToString(), "10.0.60.3");
                }

                double phsp = GetPinDouble(32);

                if ((GetPinDouble(5) != phsp && !b))
                {
                    postWriteValueHttps(sessionId, "480", GetPinValue(5).ToString(), "10.0.60.3");
                }

                double ehsp = GetPinDouble(33);

                if ((GetPinDouble(6) != ehsp && !b))
                {
                    postWriteValueHttps(sessionId, "481", GetPinValue(6).ToString(), "10.0.60.3");
                }

                double salv_max = GetPinDouble(36);

                if ((GetPinDouble(7) != salv_max && !b))
                {
                    postWriteValueHttps(sessionId, "486", GetPinValue(7).ToString(), "10.0.60.3");
                }

                double salv_min = GetPinDouble(37);
                if ((GetPinDouble(8) != salv_min && !b))
                {
                    postWriteValueHttps(sessionId, "487", GetPinValue(8).ToString(), "10.0.60.3");
                }

                double xp1 = GetPinDouble(40);
                if ((GetPinDouble(10) != xp1 && !b))
                {
                    postWriteValueHttps(sessionId, "685", GetPinValue(10).ToString(), "10.0.60.3");
                }

                /*string tn1 = GetPinString(41);
                if ((!GetPinString(11).Equals(tn1) && !b))
                {
                    postWriteValueHttps(sessionId, "686", GetPinValue(11).ToString(), "10.0.60.3");
                }

                string tv1 = GetPinString(42);
                if ((!GetPinString(12).Equals(tv1) && !b))
                {
                    postWriteValueHttps(sessionId, "687", GetPinValue(12).ToString(), "10.0.60.3");
                }

                double xp2 = GetPinDouble(43);
                if ((GetPinDouble(13) != xp2 && !b))
                {
                    postWriteValueHttps(sessionId, "688", GetPinValue(13).ToString(), "10.0.60.3");
                }

                string tn2 = GetPinString(44);
                if ((!GetPinString(14).Equals(tn2) && !b))
                {
                    postWriteValueHttps(sessionId, "689", GetPinValue(14).ToString(), "10.0.60.3");
                }

                string tv2 = GetPinString(45);
                if ((!GetPinString(15).Equals(tv2) && !b))
                {
                    postWriteValueHttps(sessionId, "690", GetPinValue(15).ToString(), "10.0.60.3");
                }

                string prom = GetPinString(39);
                if ((!GetPinString(9).Equals(prom) && !b))
                {
                    postWriteValueHttps(sessionId, "589", GetPinValue(9).ToString(), "10.0.60.3");
                }
            }*/
            b = false;

        }



        ///////////////////////
        static Hashtable outputs(string ip, string uris)
        {
            float f = 0;
            ServicePointManager.ServerCertificateValidationCallback
                            = (obj, certificate, chain, errors) => true;

            Hashtable hash_r = new Hashtable();


            foreach (DictionaryEntry de in hash)
            {
                string resultat = geting("https://" + ip + "/ajax.app?SessionId=" + sessionId + "&service=getDp&plantItemId=" + de.Key, googies, uAgent, Acpt, uris);
                Regex r = new Regex(@"plantItemId"":""(\d+)"",""value"":""([^""]+)", RegexOptions.Singleline);
                // "id=""dp([^""]+)"">\s*([^""]+)\<\/div\>"
                foreach (Match m in r.Matches(resultat))
                {
                    hash_r.Add(m.Groups[1].Value, m.Groups[2].Value);
                }
            }

            return hash_r;
        }

        ///////////////////////
        static void outputs1()
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
        static string geting(string uris, string cooky, string ua, string acp, string sLocation)
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
            // выполняем запрос
            using (HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
            {
                using (StreamReader myStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), Encoding.GetEncoding(1251)))
                {
                    var result = myStreamReader.ReadToEnd();
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
        // POST for write values
        static string postWriteValue(string sessionId, string id, string new_value, string ip)
        {


            System.Net.ServicePointManager.Expect100Continue = false;

            var request = (HttpWebRequest)WebRequest.Create("http://" + ip + "/dialog.app?SessionId=" + sessionId + "&action=new&id=" + id);//   "id=2717  http://10.0.60.2/dialog.app?SessionId=092a2169-6ead-4ed7-95ea-cc676197a314&action=new&id=2717");
            var response1 = (HttpWebResponse)request.GetResponse();
            var responseString1 = new StreamReader(response1.GetResponseStream()).ReadToEnd();

            Hashtable hash = new Hashtable();
            Regex r = new Regex(@"name=""([^""]+)""\s+value=""([^""]+)""", RegexOptions.Singleline);//id="dp(\d+)"\>\s+([^""]+)\<\/div\>
            foreach (Match m in r.Matches(responseString1))
            {
                hash.Add(m.Groups[1].Value, m.Groups[2].Value);
            }



            string result1 = geting("http://10.0.60.2/dialog.app?SessionId=" + sessionId + "&action=new&id=" + id, googies, uAgent, Acpt, "http://10.0.60.2/dialog.app?SessionId=9daaef45-1454-499b-8158-c474ef7ef00f&action=new&id=2717");


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
        static string postWriteValueHttps(string sessionId, string id, string new_value, string ip)
        {


            System.Net.ServicePointManager.Expect100Continue = false;

            var request = (HttpWebRequest)WebRequest.Create("https://" + ip + "/dialog.app?SessionId=" + sessionId + "&action=new&id=" + id);
            var response1 = (HttpWebResponse)request.GetResponse();
            var responseString1 = new StreamReader(response1.GetResponseStream()).ReadToEnd();

            Hashtable hash = new Hashtable();
            Regex r = new Regex(@"name=""([^""]+)""\s+value=""([^""]+)""", RegexOptions.Singleline);
            foreach (Match m in r.Matches(responseString1))
            {
                hash.Add(m.Groups[1].Value, m.Groups[2].Value);
            }

            IEnumerator hashEnumer = (IEnumerator)hash.GetEnumerator();
            while (hashEnumer.MoveNext())
            {
                DictionaryEntry de = (DictionaryEntry)hashEnumer.Current;

            }

            string result1 = geting("https://" + ip + "/dialog.app?SessionId=" + sessionId + "&action=new&id=" + id, googies, uAgent, Acpt, "https://" + ip + "/dialog.app?SessionId=34e32e0a-72a9-424d-86d6-36f3b63ae8f5&action=new&id=" + id);//"http://10.0.60.2/dialog.app?SessionId=9daaef45-1454-499b-8158-c474ef7ef00f&action=new&id=2717");

            var request1 = (HttpWebRequest)WebRequest.Create("https://" + ip + "/dialog.app?SessionId=" + sessionId);
            var postData = "";
            if (id.Equals("589"))
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


    }
}
