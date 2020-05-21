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
using NLog;
using OZW772_RMU720.localhost;

namespace OZW772_RMU720
{
     [Serializable,
    ComVisible(true),
    Guid("44AF71C9-E7C0-4E99-A56D-BE465F9F0D6B"),
    CatID(CatIDs.CATID_OTHER),
    DisplayName("OZW772_RMU720_FB"),
    ControllerCode(54),
    FBOptions(FBOptions.UseScanByTime)]



    public class OZW772_RMU720 : StaticFBBase
    {
        private static string uAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
        private static string Acpt = "application/json, text/javascript, */*; q=0.01";
        private static string googies="";
        //private static string URLs = null;
        private static string URI = null; 
        private static string URLs1 = "http://10.0.60.2/main.app?SessionId=8d82670f-41e2-4c9f-8602-fe2d4b3ac3a1&section=auth";
        private static string log = "Administrator";
        private static string pass1 = "Password";
        private static string pass = "a987123!";
        //private static string sessionId = null;
        private static string sessionIP = null;
        private Boolean b =false;
        private static Hashtable hash = new Hashtable();
        bool isBusy = false;
        bool first = true;
        static localhost.DataService client = null;

        static Dictionary<string, localhost.RequestObject> requestObjects = new Dictionary<string, localhost.RequestObject>();

        static Dictionary<string, int> textValues = new Dictionary<string, int>();

        static bool updateStoped = false;

        protected override void ToRuntime() {




            first = true;

            b = true;

            if (textValues.Count == 0)
            {
                textValues.Add("Comfort", 1);
                textValues.Add("Precomfort", 2);
                textValues.Add("Economy", 3);
                textValues.Add("Protection", 4);
            }

        }

        static void  writeToLog(string message)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            var time = DateTime.Now;
            message += "; Time=" + time.ToString() + ":" + time.Millisecond;
            logger.Info(message);
        }

        protected override void ToDesign() 
        { 
        
        }


         /*
         public UpdateDataForService()
         {
             UpdateData();
         }
         */

        


         /*
         public Hashtable GetDataFromService(string ip, string uris)
         {

         }
         */


        localhost.RequestObject InitObject()
        {
            localhost.RequestObject obj1 = new localhost.RequestObject();

            //                obj1.IDsForRequests = new string[]{"450", "451", "452", "453", "454", "455", "456", "457", "465", "468", "475", "476", "477", "478", "479", "480", "481", "482", "483", "486", "487", "589", "471", "472"};

            obj1.URLs = "https://10.0.60.3/main.app?SessionId=c952fa02-ba27-41fc-97ef-0aa6d18060d6";
            obj1.IDsForRequests = new string[] { "450", "451", "452", "453", "454", "455", "456", "457", "465", "468", "475", "476", "477", "478", "479", "480", "481", "482", "483", "486", "487", "589", "471", "472" };
            if (sessionIP.Equals("10.0.60.3"))
            {
            }
            else if (sessionIP.Equals("10.0.60.6"))
            {
                obj1.URLs = "https://10.0.60.6/main.app?SessionId=70a0e649-7310-44c4-af6c-be66b39fe255&section";
                obj1.IDsForRequests = new string[] { "866", "874", "875", "894", "895", "897", "901", "902", "903", "904", "864", "865", "867", "868", "869", "870", "871", "872", "879", "880", "886", "887", "888", "889", "890", "891", "892", "893", "896", "899", "900", "1001", "1003", "882", "883" };
            }
            else if (sessionIP.Equals("10.0.60.10"))
            {
                obj1.URLs = "https://10.0.60.10/main.app?SessionId=511d4201-94dc-4db4-a0f3-92fdf39793ba";
                obj1.IDsForRequests = new string[] { "866", "874", "875", "894", "895", "897", "901", "902", "903", "904", "864", "865", "867", "868", "869", "870", "871", "872", "879", "880", "886", "887", "888", "889", "890", "891", "892", "893", "896", "899", "900", "1001", "1003", "882", "883" };
            }

            else if (sessionIP.Equals("10.0.60.2"))
            {
                obj1.URLs = "http://10.0.60.2/main.app?SessionId=8743aaba-14bd-423a-91e4-6a80bddfbcf5&section=auth";
                if (GetPinInt(38)==1)
                {
                obj1.IDsForRequests = new string[] { "2507", "2508", "2509", "2510", "2511", "2512", "2513", "2514", "2522", "2525", "2528", "2529", "2531", "2532", "2533", "2534", "2535", "2536", "2537", "2538", "2539", "2541", "2542", "2543", "2544", "2646" };
                }
                else if (GetPinInt(38) == 2)
                {
                    obj1.IDsForRequests = new string[] { "1885", "1886", "1887", "1888", "1889", "1890", "1891", "1892", "1900", "1903", "1906", "1907", "1909", "1910", "1911", "1912", "1913", "1914", "1915", "1916", "1917", "1918", "1919", "1920", "1921", "1922", "2024" };
                }
                else if (GetPinInt(38) == 3)
                {
                    obj1.IDsForRequests = new string[] { "1263", "1264", "1265", "1266", "1267", "1268", "1269", "1270", "1278", "1281", "1284", "1285", "1287", "1288", "1289", "1290", "1291", "1292", "1293", "1294", "1295", "1296", "1297", "1298", "1299", "1300", "1402" };
                }
                else if (GetPinInt(38) == 4)
                {
                    obj1.IDsForRequests = new string[] { "641", "642", "643", "644", "645", "646", "647", "648", "656", "659", "662", "663", "665", "666", "667", "668", "669", "670", "671", "672", "673", "674", "675", "676", "677", "678", "780" };
                }
            }

            obj1.URI = "https://10.0.60.3/main.app?SessionId=dd658316-a9c5-4d14-98ec-55c0b4be2252&section=popcard&id=449&idtype=4";
            if (sessionIP.Equals("10.0.60.3")) { obj1.URI = "https://10.0.60.3/main.app?SessionId=dd658316-a9c5-4d14-98ec-55c0b4be2252&section=popcard&id=449&idtype=4"; }
            else if (sessionIP.Equals("10.0.60.6")) { obj1.URI = "https://10.0.60.6/main.app?SessionId=f899bef9-e644-4f0a-8008-d8743e490141&section=popcard&id=863&idtype=4"; }
            else if (sessionIP.Equals("10.0.60.10")) { obj1.URI = "https://10.0.60.10/main.app?SessionId=511d4201-94dc-4db4-a0f3-92fdf39793ba&section=popcard&id=863&idtype=4"; }
            else if (sessionIP.Equals("10.0.60.2"))
            {
               
                if (GetPinInt(38) == 1)
                {
                    obj1.URI = "http://10.0.60.2/main.app?SessionId=1770edc8-7c31-464d-982f-44300285d583&section=popcard&id=2487&idtype=2";
                }
                else if (GetPinInt(38) == 2)
                {
                    obj1.URI = "http://10.0.60.2/main.app?SessionId=1770edc8-7c31-464d-982f-44300285d583&section=popcard&id=1865&idtype=2";
                }
                else if (GetPinInt(38) == 3)
                {
                    obj1.URI = "http://10.0.60.2/main.app?SessionId=1770edc8-7c31-464d-982f-44300285d583&section=popcard&id=1243&idtype=2";
                }
                else if (GetPinInt(38) == 4)
                {
                    obj1.URI = "http://10.0.60.2/main.app?SessionId=1770edc8-7c31-464d-982f-44300285d583&section=popcard&id=621&idtype=2";
                }
            }
            //получаем ИД сессии


            //obj1.IDs = new string[] { "449", "464", "467", "470", "473", "588", "684" };
            if (sessionIP.Equals("10.0.60.3")) { obj1.IDs = new string[] { "449", "464", "467", "470", "473", "588", "684" }; }
            else if (sessionIP.Equals("10.0.60.6") || sessionIP.Equals("10.0.60.10")) { obj1.IDs = new string[] { "863", "878", "887", "881", "886", "1000", "1091", "1002" }; }
            else if (sessionIP.Equals("10.0.60.10")) { obj1.IDs = new string[] { "863", "878", "881", "886", "1000", "1091", "1002" }; }
            else if (sessionIP.Equals("10.0.60.2"))
            {

                if (GetPinInt(38) == 1)
                {
                    obj1.IDs = new string[] { "2506", "2521", "2524", "2527", "2530", "2645", "2749" };
                }
                else if (GetPinInt(38) == 2)
                {
                    obj1.IDs = new string[] { "1884", "1899", "1902", "1905", "1908", "2023", "2119" };
                }
                else if (GetPinInt(38) == 3)
                {
                    obj1.IDs = new string[] { "1262", "1277", "1280", "1283", "1286", "1401", "1497" };
                }
                else if (GetPinInt(38) == 4)
                {
                    obj1.IDs = new string[] { "640", "655", "658", "661", "664", "779", "875" };
                }
            }

            return obj1;
        }
         
        protected override void UpdateData() {

            const int Pin_IP = 34;
            sessionIP = GetPinValue(Pin_IP).ToString();

            string sessionIPKey = sessionIP;

            writeToLog("Start updateData; isBusy=" + isBusy);

            if (isBusy)
                return;

            isBusy = true;

            if (GetPinInt(38) != null)
                sessionIPKey = sessionIP + "-" + GetPinInt(38);

            bool allowWrite = GetPinBool(39);
            //allowWrite = true;


            if (first)
            {

               
                //inpu; vent;  pump; kontrol; rejim;  PID
                localhost.RequestObject obj1 = InitObject();
                obj1.URL = sessionIP;
                if (!requestObjects.ContainsKey(sessionIPKey))
                    requestObjects.Add(sessionIPKey, obj1);


                if (client == null)
                    client = new localhost.DataService();

                try
                {
                    client.Init(sessionIPKey, requestObjects[sessionIPKey]);
                    // останавливаем обновление параметров на сервисе
                    if (!updateStoped)
                    {
                        client.StopUpdating("", null);
                        updateStoped = true;
                    }
                }
                catch (Exception ex)
                {

                }
            }




            var obj = requestObjects[sessionIPKey];


            /*
            if (b)
            {
                
                Init();
               
            }
*/
            try
            {
                //Hashtable r = null;//outputs(sessionIP, URI);
                
                Hashtable r = new Hashtable();

                writeToLog("Start getting data from service");


                SCADAObject[] results = null;

                try
                {

                    results = client.GetData(sessionIPKey, obj);
                }
                catch(Exception ex)
                {
                    //if (ex.ToString().IndexOf("Need")>=0)
                    {
                        localhost.RequestObject obj1 = InitObject();
                        obj1.URL = sessionIP;
                        if (!requestObjects.ContainsKey(sessionIPKey))
                            requestObjects.Add(sessionIPKey, obj1);
                        client.Init(sessionIPKey, requestObjects[sessionIPKey]);
                        results = client.GetData(sessionIPKey, obj);
                    }
                }

                if (results != null && results.Length > 0)
                {
                    for (int i=0;i<results.Length;i++)
                    {
                        r.Add(results[i].key, results[i].value);
                    }
                }

                if (!b)
                {
                    // запускаем обновление параметров на сервисе
                    client.StartUpdating(sessionIPKey, obj);
                }

                if (r.Count == 0)
                {
                    isBusy = false;
                    first = false;
                    client.StartUpdating(sessionIPKey, obj);
                    return;
                }


                writeToLog("End getting data from service");
                 
                //read data
                // строка ид параметров для чтения:  {"450", "451", "452", "453", "454", "455", "456", "457", "465", '468", "475", "476", "477", "478", "479", "480", "481", "482", "483", "486", "487", "589", "471", "472"}
                //присваиваем значения если IP 10.0.60.3
                if (sessionIP.Equals("10.0.60.3"))
                {
                    try
                    {
                        SetPinValue(10, float.Parse(r["450"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(10, 0); }
                    try
                    {
                    SetPinValue(11, float.Parse(r["451"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(11, 0); }
                     try
                    {
                    SetPinValue(12, float.Parse(r["452"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                         }
                    catch (Exception e) { SetPinValue(12, 0); }
                    try
                    {
                    SetPinValue(13, float.Parse(r["453"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                    catch (Exception e) { SetPinValue(13, 0); }
                    try
                    {
                    SetPinValue(14, float.Parse(r["454"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                    catch (Exception e) { SetPinValue(14, 0); }
                    try
                    {
                    SetPinValue(15, float.Parse(r["455"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                    catch (Exception e) { SetPinValue(15, 0); }
                    try
                    {
                    SetPinValue(16, float.Parse(r["456"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                    catch (Exception e) { SetPinValue(16, 0); }
                    try
                    {
                    SetPinValue(17, float.Parse(r["457"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                    catch (Exception e) { SetPinValue(17, 0); }
                    try
                    {
                    if (r["465"].Equals("Off"))
                    {
                        SetPinValue(18, 0);
                    }
                    else { SetPinValue(18, 1); }
                        }
                    catch (Exception e) { SetPinValue(18, 0); }
                    try
                    {

                    if (r["468"].Equals("Off"))
                    {
                        SetPinValue(19, 0);
                    }
                    else { SetPinValue(19, 1); }
                        }
                    catch (Exception e) { SetPinValue(19, 0); }
                    
                    
                    SetPinValue(20, 0);
                    double d = 0;
                    try
                    {
                    if (double.TryParse(r["475"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))//System.Globalization.CultureInfo.InvariantCulture, 
                    {
                        SetPinValue(21, (float)(d));

                    }
                    else { SetPinValue(21, 0); }
                    }
                    catch (Exception e) { SetPinValue(21, 0); }
                    try
                    {

                    SetPinValue(22, float.Parse(r["476"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(22, 0); }
                    try
                    {
                    SetPinValue(23, float.Parse(r["477"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }  
                    catch (Exception e) { SetPinValue(23, 0); }
                    try
                    {
                    SetPinValue(24, float.Parse(r["478"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                         }  
                    catch (Exception e) { SetPinValue(24, 0); }
                    try
                    {
                    SetPinValue(25, float.Parse(r["479"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                         }  
                    catch (Exception e) { SetPinValue(25, 0); }
                    try
                    {
                    SetPinValue(26, float.Parse(r["480"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                         }  
                    catch (Exception e) { SetPinValue(26, 0); }
                    try
                    {
                    SetPinValue(27, float.Parse(r["481"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                         }  
                    catch (Exception e) { SetPinValue(27, 0); }
                    try
                    {
                    SetPinValue(28, float.Parse(r["482"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                         }  
                    catch (Exception e) { SetPinValue(23, 0); }
                    try
                    {

                    if (double.TryParse(r["483"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
                    {
                        SetPinValue(29, (float)(d));

                    }
                    else { SetPinValue(29, 0); }
                         }  
                    catch (Exception e) { SetPinValue(29, 0); }
                    try
                    {
                    SetPinValue(30, float.Parse(r["486"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                         }  
                    catch (Exception e) { SetPinValue(30, 0); }
                    try
                    {
                    SetPinValue(31, float.Parse(r["487"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                         }  
                    catch (Exception e) { SetPinValue(31, 0); }
                   
                    SetPinValue(32, 0);
                    
                    try
                    {
                    SetPinValue(33, r["589"]);
                         }  
                    catch (Exception e) { SetPinValue(33, 0); }
                    try
                    {
                    SetPinValue(36, float.Parse(r["471"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                         }  
                    catch (Exception e) { SetPinValue(36, 0); }
                    try
                    {
                    SetPinValue(37, float.Parse(r["472"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat)); //"449", "464", "467", "470", "473", "588", "684"
                     }  
                    catch (Exception e) { SetPinValue(37, 0); }
                    

                    //write data
                    if (b)
                    {
                        if (allowWrite)
                        {
                            SetPinValue(1, 20.0);
                            SetPinValue(2, 20.0);
                            SetPinValue(3, 20.0);
                            SetPinValue(4, 20.0);
                            SetPinValue(5, 20.0);
                            SetPinValue(6, 20.0);
                        
                            SetPinValue(7, 35.0);
                            SetPinValue(8, 16.0);
                            SetPinValue(9, 4);
                            
                        }
                        b = false;
                        
                    }
                    
                    {
                        if (allowWrite)
                        {
                            double ecsp = GetPinDouble(22);

                            if ((GetPinDouble(1) != ecsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "476", GetPinValue(1).ToString(), "10.0.60.3");
                            }


                            double pcsp = GetPinDouble(23);

                            if ((GetPinDouble(2) != pcsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "477", GetPinValue(2).ToString(), "10.0.60.3");
                            }

                            double ccsp = GetPinDouble(24);

                            if ((GetPinDouble(3) != ccsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "478", GetPinValue(3).ToString(), "10.0.60.3");
                            }
                            double chsp = GetPinDouble(25);

                            if ((GetPinDouble(4) != chsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "479", GetPinValue(4).ToString(), "10.0.60.3");
                            }

                            double phsp = GetPinDouble(26);

                            if ((GetPinDouble(5) != phsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "480", GetPinValue(5).ToString(), "10.0.60.3");
                            }

                            double ehsp = GetPinDouble(27);

                            if ((GetPinDouble(6) != ehsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "481", GetPinValue(6).ToString(), "10.0.60.3");
                            }

                            double salv_max = GetPinDouble(30);

                            if ((GetPinDouble(7) != salv_max && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "486", GetPinValue(7).ToString(), "10.0.60.3");
                            }

                            double salv_min = GetPinDouble(31);
                            if ((GetPinDouble(8) != salv_min && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "487", GetPinValue(8).ToString(), "10.0.60.3");
                            }
                        }


                        string prom = GetPinString(33);
                        if (prom != null && textValues.ContainsKey(prom))
                            prom = textValues[prom].ToString();
                        if ((GetPinString(9) != null && !GetPinString(9).Equals(prom) && !b))
                        {
                            postWriteValueHttps(obj, sessionIPKey, "589", GetPinValue(9).ToString(), "10.0.60.3");
                        }
                    }
                }
                //========================================================================================
                //присваиваем значения если IP 10.0.60.6
                // строка параметров для чтения: {"864", "865", "867", "868", "869", "870", "871", "872", "879", "880", "886", "887", "888", "889", "890", "891", "892", "893", "896", "899", "900", "1001", 882", "883"}
                if (sessionIP.Equals("10.0.60.6"))
                {
                      
                    try
                    {
                    SetPinValue(10, float.Parse(r["864"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));//"863", "878", "881", "884", "886", "1000", "1091" 
                          }  
                    catch (Exception e) { SetPinValue(10, 0); }
                    try
                    {
                    SetPinValue(11, float.Parse(r["865"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(11, 0); }
                    try
                    {
                    SetPinValue(12, float.Parse(r["866"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(12, 0); }
                    try
                    {
                    SetPinValue(13, float.Parse(r["867"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(13, 0); }
                    try
                    {
                    SetPinValue(14, float.Parse(r["868"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(14, 0); }
                    try
                    {
                    SetPinValue(15, float.Parse(r["869"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(15, 0); }
                    try
                    {
                    SetPinValue(16, float.Parse(r["870"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(16, 0); }
                    try
                    {
                    SetPinValue(17, float.Parse(r["871"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(17, 0); }
                    try
                    {

                    if (r["879"].Equals("Off"))
                    {
                        SetPinValue(18, 0);
                    }
                    else { SetPinValue(18, 1); }
                          }  
                    catch (Exception e) { SetPinValue(18, 0); }
                    try
                    {

                    if (r["882"].Equals("Off"))
                    {
                        SetPinValue(19, 0);
                    }
                    else { SetPinValue(19, 1); }
                          }  
                    catch (Exception e) { SetPinValue(19, 0); }
                   

                    //"449", "464", "467", "470", "473", "588", "684"
                    SetPinValue(20, 0);
                    double d = 0;
                     
                    try
                    {
                    if (double.TryParse(r["889"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
                    {
                        SetPinValue(21, (float)(d));

                    }
                    else { SetPinValue(21, 0); }
                          }  
                    catch (Exception e) { SetPinValue(21, 0); }
                    try
                    {
                    
                    SetPinValue(22, float.Parse(r["890"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(22, 0); }
                    try
                    {
                    SetPinValue(23, float.Parse(r["891"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(23, 0); }
                    try
                    {
                    SetPinValue(24, float.Parse(r["892"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(24, 0); }
                    try
                    {
                    SetPinValue(25, float.Parse(r["893"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(25, 0); }
                    try
                    {
                    SetPinValue(26, float.Parse(r["894"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(26, 0); }
                    try
                    {
                    SetPinValue(27, float.Parse(r["895"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(27, 0); }
                    try
                    {
                    SetPinValue(28, float.Parse(r["896"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(28, 0); }
                    try
                    {

                    if (double.TryParse(r["897"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
                    {
                        SetPinValue(29, (float)(d));

                    }
                    else { SetPinValue(29, 0); }
                          }  
                    catch (Exception e) { SetPinValue(29, 0); }
                    try
                    {

                    SetPinValue(30, float.Parse(r["900"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(30, 0); }
                    try
                    {
                    SetPinValue(31, float.Parse(r["901"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(31, 0); }
                    try
                    {
                    SetPinValue(32, 0);
                          }  
                    catch (Exception e) { SetPinValue(32, 0); }
                    try
                    {
                    SetPinValue(33, r["1003"]);
                          }  
                    catch (Exception e) { SetPinValue(33, 0); }
                    try
                    {
                    SetPinValue(36, float.Parse(r["885"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(36, 0); }
                    try
                    {
                    SetPinValue(37, float.Parse(r["886"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(37, 0); }
                    


                    //write data
                    if (b)
                    {
                        if (allowWrite)
                        {
                            SetPinValue(1, 20.0);
                            SetPinValue(2, 20.0);
                            SetPinValue(3, 20.0);
                            SetPinValue(4, 20.0);
                            SetPinValue(5, 20.0);
                            SetPinValue(6, 20.0);                        
                            SetPinValue(7, 35.0);
                            SetPinValue(8, 16.0);
                            SetPinValue(9, 4);
                        }
                            b = false;
                        

                    }
                    
                    {
                        if (allowWrite)
                        {
                            double ecsp = GetPinDouble(22);

                            if ((GetPinDouble(1) != ecsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "890", GetPinValue(1).ToString(), "10.0.60.6");
                            }


                            double pcsp = GetPinDouble(23);

                            if ((GetPinDouble(2) != pcsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "891", GetPinValue(2).ToString(), "10.0.60.6");
                            }

                            double ccsp = GetPinDouble(24);

                            if ((GetPinDouble(3) != ccsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "892", GetPinValue(3).ToString(), "10.0.60.6");
                            }
                            double chsp = GetPinDouble(25);

                            if ((GetPinDouble(4) != chsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "893", GetPinValue(4).ToString(), "10.0.60.6");
                            }

                            double phsp = GetPinDouble(26);

                            if ((GetPinDouble(5) != phsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "894", GetPinValue(5).ToString(), "10.0.60.6");
                            }

                            double ehsp = GetPinDouble(27);

                            if ((GetPinDouble(6) != ehsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "895", GetPinValue(6).ToString(), "10.0.60.6");
                            }

                            double salv_max = GetPinDouble(30);

                            if ((GetPinDouble(7) != salv_max && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "900", GetPinValue(7).ToString(), "10.0.60.6");
                            }

                            double salv_min = GetPinDouble(31);
                            if ((GetPinDouble(8) != salv_min && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "901", GetPinValue(8).ToString(), "10.0.60.6");
                            }
                        }


                        string prom = GetPinString(33);
                        if (prom != null && textValues.ContainsKey(prom))
                            prom = textValues[prom].ToString();
                        if ((!GetPinString(9).Equals(prom) && !b))
                        {
                            postWriteValueHttps(obj, sessionIPKey, "1003", GetPinValue(9).ToString(), "10.0.60.6");
                        }
                    }
                }



//===========================================================================================

                //присваиваем значения если IP 10.0.60.10
                // строка параметров для чтения: {"864", "865", "867", "868", "869", "870", "871", "872", "879", "880", "886", "887", "888", "889", "890", "891", "892", "893", "896", "899", "900", "1001", 882", "883"}
                if (sessionIP.Equals("10.0.60.10"))
                {
                     
                    try
                    {
                    SetPinValue(10, float.Parse(r["864"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));//"863", "878", "881", "884", "886", "1000", "1091" 
                          }  
                    catch (Exception e) { SetPinValue(10, 0); }
                    try
                    {
                    SetPinValue(11, float.Parse(r["865"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(11, 0); }
                    try
                    {
                    SetPinValue(12, float.Parse(r["866"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(12, 0); }
                    try
                    {
                    SetPinValue(13, float.Parse(r["867"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(13, 0); }
                    try
                    {
                    SetPinValue(14, float.Parse(r["868"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(14, 0); }
                    try
                    {
                    SetPinValue(15, float.Parse(r["869"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(15, 0); }
                    try
                    {
                    SetPinValue(16, float.Parse(r["870"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(16, 0); }
                    try
                    {
                    SetPinValue(17, float.Parse(r["871"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(17, 0); }
                    try
                    {

                    if (r["879"].Equals("Off"))
                    {
                        SetPinValue(18, 0);
                    }
                    else { SetPinValue(18, 1); }

                          }  
                    catch (Exception e) { SetPinValue(18, 0); }
                    try
                    {

                    if (r["882"].Equals("Off"))
                    {
                        SetPinValue(19, 0);
                    }
                    else { SetPinValue(19, 1); }

                          }  
                    catch (Exception e) { SetPinValue(19, 0); }
                    try
                    {

                    //"449", "464", "467", "470", "473", "588", "684"
                    SetPinValue(20, 0);
                          }  
                    catch (Exception e) { SetPinValue(20, 0); }
                   
                    double d = 0;

                     
                    try
                    {
                    if (double.TryParse(r["888"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
                    {
                        SetPinValue(21, (float)(d));

                    }
                    else { SetPinValue(21, 0); }
                          }  
                    catch (Exception e) { SetPinValue(21, 0); }
                    try
                    {
                    SetPinValue(22, float.Parse(r["889"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(22, 0); }
                    try
                    {
                    SetPinValue(23, float.Parse(r["890"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(23, 0); }
                    try
                    {
                    SetPinValue(24, float.Parse(r["891"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(24, 0); }
                    try
                    {
                    SetPinValue(25, float.Parse(r["892"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(25, 0); }
                    try
                    {
                    SetPinValue(26, float.Parse(r["893"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue (26, 0); }
                    try
                    {
                    SetPinValue(27, float.Parse(r["894"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(27, 0); }
                    try
                    {
                    SetPinValue(28, float.Parse(r["895"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(28, 0); }
                    try
                    {

                    if (double.TryParse(r["896"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
                    {
                        SetPinValue(29, (float)(d));

                    }
                    else { SetPinValue(29, 0); }
                          }  
                    catch (Exception e) { SetPinValue(29, 0); }
                    try
                    {


                    SetPinValue(30, float.Parse(r["899"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(30, 0); }
                    try
                    {
                    SetPinValue(31, float.Parse(r["900"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(31, 0); }
                    try
                    {
                    SetPinValue(32, 0);
                          }  
                    catch (Exception e) { SetPinValue(32, 0); }
                    try
                    {
                    SetPinValue(33, r["1001"]);
                          }  
                    catch (Exception e) { SetPinValue(33, 0); }
                    try
                    {
                    SetPinValue(36, float.Parse(r["885"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(36, 0); }
                    try
                    {
                    SetPinValue(37, float.Parse(r["886"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                          }  
                    catch (Exception e) { SetPinValue(37, 0); }
                    


                    //write data
                    if (b)
                    {
                        if (allowWrite)
                        {
                            SetPinValue(1, 20.0);
                            SetPinValue(2, 20.0);
                            SetPinValue(3, 20.0);
                            SetPinValue(4, 20.0);
                            SetPinValue(5, 20.0);
                            SetPinValue(6, 20.0);                        
                            SetPinValue(7, 35.0);
                            SetPinValue(8, 16.0);
                            SetPinValue(9, 4);
                        }
                        b = false;

                    }
                    
                    {
                        if (allowWrite)
                        {
                            double ecsp = GetPinDouble(22);

                            if ((GetPinDouble(1) != ecsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "889", GetPinValue(1).ToString(), "10.0.60.10");
                            }


                            double pcsp = GetPinDouble(23);

                            if ((GetPinDouble(2) != pcsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "890", GetPinValue(2).ToString(), "10.0.60.10");
                            }

                            double ccsp = GetPinDouble(24);

                            if ((GetPinDouble(3) != ccsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "891", GetPinValue(3).ToString(), "10.0.60.10");
                            }
                            double chsp = GetPinDouble(25);

                            if ((GetPinDouble(4) != chsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "892", GetPinValue(4).ToString(), "10.0.60.10");
                            }

                            double phsp = GetPinDouble(26);

                            if ((GetPinDouble(5) != phsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "893", GetPinValue(5).ToString(), "10.0.60.10");
                            }

                            double ehsp = GetPinDouble(27);

                            if ((GetPinDouble(6) != ehsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "894", GetPinValue(6).ToString(), "10.0.60.10");
                            }

                            double salv_max = GetPinDouble(30);

                            if ((GetPinDouble(7) != salv_max && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "899", GetPinValue(7).ToString(), "10.0.60.10");
                            }

                            double salv_min = GetPinDouble(31);
                            if ((GetPinDouble(8) != salv_min && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "900", GetPinValue(8).ToString(), "10.0.60.10");
                            }
                        }




                        string prom = GetPinString(33);
                        if (prom != null && textValues.ContainsKey(prom))
                            prom = textValues[prom].ToString();
                        if ((!GetPinString(9).Equals(prom) && !b))
                        {
                            postWriteValueHttps(obj, sessionIPKey, "1001", GetPinValue(9).ToString(), "10.0.60.10");
                        }
                    }
                }

                //присваиваем значения если IP 10.0.60.2
                if (sessionIP.Equals("10.0.60.2"))
                {
                    if (GetPinInt(38) == 1)
                    {
                       
                   
                    try
                    {
                        SetPinValue(10, float.Parse(r["2507"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(10, 0); }
                    try
                    {
                        SetPinValue(11, float.Parse(r["2508"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(11, 0); }
                    try
                    {
                        SetPinValue(12, float.Parse(r["2509"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(12, 0); }
                    try
                    {
                        SetPinValue(13, float.Parse(r["2510"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(13, 0); }
                    try
                    {
                        SetPinValue(14, float.Parse(r["2511"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(14, 0); }
                    try
                    {
                        SetPinValue(15, float.Parse(r["2512"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(15, 0); }
                    try
                    {
                        SetPinValue(16, float.Parse(r["2513"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(16, 0); }
                    try
                    {
                        SetPinValue(17, float.Parse(r["2514"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(17, 0); }
                    try
                    {
                        if (r["2522"].Equals("Off"))
                        {
                            SetPinValue(18, 0);
                        }
                        else { SetPinValue(18, 1); }
                    }
                    catch (Exception e) { SetPinValue(18, 0); }
                    try
                    {

                        if (r["2525"].Equals("Off"))
                        {
                            SetPinValue(19, 0);
                        }
                        else { SetPinValue(19, 1); }
                    }
                    catch (Exception e) { SetPinValue(19, 0); }


                    SetPinValue(20, 0);
                    double d = 0;
                    try
                    {
                        if (double.TryParse(r["2528"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))//System.Globalization.CultureInfo.InvariantCulture, 
                        {
                            SetPinValue(21, (float)(d));

                        }
                        else { SetPinValue(21, 0); }
                    }
                    catch (Exception e) { SetPinValue(21, 0); }
                    try
                    {

                        SetPinValue(22, float.Parse(r["2533"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(22, 0); }
                    try
                    {
                        SetPinValue(23, float.Parse(r["2534"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(23, 0); }
                    try
                    {
                        SetPinValue(24, float.Parse(r["2535"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(24, 0); }
                    try
                    {
                        SetPinValue(25, float.Parse(r["2536"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(25, 0); }
                    try
                    {
                        SetPinValue(26, float.Parse(r["2537"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(26, 0); }
                    try
                    {
                        SetPinValue(27, float.Parse(r["2538"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(27, 0); }
                    try
                    {
                        SetPinValue(28, float.Parse(r["2539"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(28, 0); }
                    try
                    {

                        if (double.TryParse(r["2540"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
                        {
                            SetPinValue(29, (float)(d));

                        }
                        else { SetPinValue(29, 0); }
                    }
                    catch (Exception e) { SetPinValue(29, 0); }
                    try
                    {
                        SetPinValue(30, float.Parse(r["2543"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(30, 0); }
                    try
                    {
                        SetPinValue(31, float.Parse(r["2544"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(31, 0); }

                    SetPinValue(32, 0);
                    //{ "2507", "2508", "2509", "2510", "2511", "2512", "2513", "2514", "2522", "2525", "2528", "2529", "2531", 
                        //"2532", "2533", "2534", "2535", "2536", "2537", "2538", "2541", "2542", "2543", "2544", "2646" };
                    try
                    {
                        SetPinValue(33, r["2646"]);
                    }
                    catch (Exception e) { SetPinValue(33, 0); }
                    try
                    {
                        SetPinValue(36, float.Parse(r["2528"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                    }
                    catch (Exception e) { SetPinValue(36, 0); }
                    try
                    {
                        SetPinValue(37, float.Parse(r["2529"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat)); //"449", "464", "467", "470", "473", "588", "684"
                    }
                    catch (Exception e) { SetPinValue(37, 0); }


                    //write data
                    if (b)
                    {
                        if (allowWrite)
                        {
                            SetPinValue(1, 20.0);
                            SetPinValue(2, 20.0);
                            SetPinValue(3, 20.0);
                            SetPinValue(4, 20.0);
                            SetPinValue(5, 20.0);
                            SetPinValue(6, 20.0);                        
                            SetPinValue(7, 35.0);
                            SetPinValue(8, 16.0);
                            SetPinValue(9, 4);
                        }
                        b = false;

                    }
                    
                    {
                        if (allowWrite)
                        {
                            double ecsp = GetPinDouble(22);

                            if ((GetPinDouble(1) != ecsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "2533", GetPinValue(1).ToString(), "10.0.60.2");
                            }


                            double pcsp = GetPinDouble(23);

                            if ((GetPinDouble(2) != pcsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "2534", GetPinValue(2).ToString(), "10.0.60.2");
                            }

                            double ccsp = GetPinDouble(24);

                            if ((GetPinDouble(3) != ccsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "2535", GetPinValue(3).ToString(), "10.0.60.2");
                            }
                            double chsp = GetPinDouble(25);

                            if ((GetPinDouble(4) != chsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "2536", GetPinValue(4).ToString(), "10.0.60.2");
                            }

                            double phsp = GetPinDouble(26);

                            if ((GetPinDouble(5) != phsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "2537", GetPinValue(5).ToString(), "10.0.60.2");
                            }

                            double ehsp = GetPinDouble(27);

                            if ((GetPinDouble(6) != ehsp && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "2538", GetPinValue(6).ToString(), "10.0.60.2");
                            }

                            double salv_max = GetPinDouble(30);

                            if ((GetPinDouble(7) != salv_max && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "2543", GetPinValue(7).ToString(), "10.0.60.2");
                            }

                            double salv_min = GetPinDouble(31);
                            if ((GetPinDouble(8) != salv_min && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "2544", GetPinValue(8).ToString(), "10.0.60.2");
                            }
                        }



                        string prom = GetPinString(33);
                        if (prom != null && textValues.ContainsKey(prom))
                            prom = textValues[prom].ToString();

                        if ((GetPinString(9) != null && !GetPinString(9).Equals(prom) && !b))
                        {
                            postWriteValueHttps(obj, sessionIPKey, "2646", GetPinValue(9).ToString(), "10.0.60.2");
                        }
                    }
                    }
                    // **********************************
                    // N = 2
                    if (GetPinInt(38) == 2)
                    {

                        //{ "1885", "1886", "1887", "1888", "1889", "1890", "1891", "1892", "1900", "1903", "1906", "1907", "1909", "1910", "1911", "1912", "1913", "1914", "1915", "1916", "1919", "1920", "1921", "1922", "2124" };
                
                        try
                        {
                            SetPinValue(10, float.Parse(r["1885"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(10, 0); }
                        try
                        {
                            SetPinValue(11, float.Parse(r["1886"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(11, 0); }
                        try
                        {
                            SetPinValue(12, float.Parse(r["1887"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(12, 0); }
                        try
                        {
                            SetPinValue(13, float.Parse(r["1888"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(13, 0); }
                        try
                        {
                            SetPinValue(14, float.Parse(r["1889"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(14, 0); }
                        try
                        {
                            SetPinValue(15, float.Parse(r["1890"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(15, 0); }
                        try
                        {
                            SetPinValue(16, float.Parse(r["1891"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(16, 0); }
                        try
                        {
                            SetPinValue(17, float.Parse(r["1892"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(17, 0); }
                        try
                        {
                            if (r["1900"].Equals("Off"))
                            {
                                SetPinValue(18, 0);
                            }
                            else { SetPinValue(18, 1); }
                        }
                        catch (Exception e) { SetPinValue(18, 0); }
                        try
                        {

                            if (r["1903"].Equals("Off"))
                            {
                                SetPinValue(19, 0);
                            }
                            else { SetPinValue(19, 1); }
                        }
                        catch (Exception e) { SetPinValue(19, 0); }


                        SetPinValue(20, 0);
                        double d = 0;
                        try
                        {
                            if (double.TryParse(r["1910"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))//System.Globalization.CultureInfo.InvariantCulture, 
                            {
                                SetPinValue(21, (float)(d));

                            }
                            else { SetPinValue(21, 0); }
                        }
                        catch (Exception e) { SetPinValue(21, 0); }
                        try
                        {
                            //"1910", "1911", "1912", "1913", "1914", "1915", "1916", "1919", "1920", "1921", "1922", "2124" };
                
                            SetPinValue(22, float.Parse(r["1911"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(22, 0); }
                        try
                        {
                            SetPinValue(23, float.Parse(r["1912"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(23, 0); }
                        try
                        {
                            SetPinValue(24, float.Parse(r["1913"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(24, 0); }
                        try
                        {
                            SetPinValue(25, float.Parse(r["1914"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(25, 0); }
                        try
                        {
                            SetPinValue(26, float.Parse(r["1915"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(26, 0); }
                        try
                        {
                            SetPinValue(27, float.Parse(r["1916"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(27, 0); }
                        try
                        {
                            SetPinValue(28, float.Parse(r["1917"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(23, 0); }
                        try
                        {

                            if (double.TryParse(r["1918"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
                            {
                                SetPinValue(29, (float)(d));

                            }
                            else { SetPinValue(29, 0); }
                        }
                        catch (Exception e) { SetPinValue(29, 0); }
                        try
                        {
                            SetPinValue(30, float.Parse(r["1921"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(30, 0); }
                        try
                        {
                            SetPinValue(31, float.Parse(r["1922"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(31, 0); }

                        SetPinValue(32, 0);
                       
                        try
                        {
                            var value = r["2024"];
                            /*
                            if (textValues.ContainsKey(r["2024"].ToString()))
                                value = textValues[r["2024"].ToString()];
                            else
                                value = 4;
                             */ 
                             SetPinValue(33, value);
                        }
                        catch (Exception e) { SetPinValue(33, 0); }
                        try
                        {
                            SetPinValue(36, float.Parse(r["1906"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(36, 0); }
                        try
                        {
                            SetPinValue(37, float.Parse(r["1907"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat)); //"449", "464", "467", "470", "473", "588", "684"
                        }
                        catch (Exception e) { SetPinValue(37, 0); }


                        //write data
                        if (b)
                        {
                            if (allowWrite)
                            {
                                SetPinValue(1, 20.0);
                                SetPinValue(2, 20.0);
                                SetPinValue(3, 20.0);
                                SetPinValue(4, 20.0);
                                SetPinValue(5, 20.0);
                                SetPinValue(6, 20.0);                            
                                SetPinValue(7, 35.0);
                                SetPinValue(8, 16.0);
                                SetPinValue(9, 4);
                            }
                            b = false;

                        }
                        
                        {
                            if (allowWrite)
                            {
                                double ecsp = GetPinDouble(22);

                                if ((GetPinDouble(1) != ecsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1911", GetPinValue(1).ToString(), "10.0.60.2");
                                }


                                double pcsp = GetPinDouble(23);

                                if ((GetPinDouble(2) != pcsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1912", GetPinValue(2).ToString(), "10.0.60.2");
                                }

                                double ccsp = GetPinDouble(24);

                                if ((GetPinDouble(3) != ccsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1913", GetPinValue(3).ToString(), "10.0.60.2");
                                }
                                double chsp = GetPinDouble(25);

                                if ((GetPinDouble(4) != chsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1914", GetPinValue(4).ToString(), "10.0.60.2");
                                }

                                double phsp = GetPinDouble(26);

                                if ((GetPinDouble(5) != phsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1915", GetPinValue(5).ToString(), "10.0.60.2");
                                }

                                double ehsp = GetPinDouble(27);

                                if ((GetPinDouble(6) != ehsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1916", GetPinValue(6).ToString(), "10.0.60.2");
                                }

                                double salv_max = GetPinDouble(30);

                                if ((GetPinDouble(7) != salv_max && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1921", GetPinValue(7).ToString(), "10.0.60.2");
                                }

                                double salv_min = GetPinDouble(31);
                                if ((GetPinDouble(8) != salv_min && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1922", GetPinValue(8).ToString(), "10.0.60.2");
                                }
                            }



                            string prom = GetPinString(33);
                            if (prom != null && textValues.ContainsKey(prom))
                                prom = textValues[prom].ToString();
                            if ((GetPinString(9) != null && !GetPinString(9).Equals(prom) && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "2024", GetPinValue(9).ToString(), "10.0.60.2");
                            }
                        }
                    }


                    // **********************************
                    // N = 3
                    if (GetPinInt(38) == 3)
                    {

                        //{ "1263", "1264", "1265", "1266", "1267", "1268", "1269", "1270", "1278", "1281", 
                        try
                        {
                            SetPinValue(10, float.Parse(r["1263"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(10, 0); }
                        try
                        {
                            SetPinValue(11, float.Parse(r["1264"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(11, 0); }
                        try
                        {
                            SetPinValue(12, float.Parse(r["1265"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(12, 0); }
                        try
                        {
                            SetPinValue(13, float.Parse(r["1266"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(13, 0); }
                        try
                        {
                            SetPinValue(14, float.Parse(r["1267"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(14, 0); }
                        try
                        {
                            SetPinValue(15, float.Parse(r["1268"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(15, 0); }
                        try
                        {
                            SetPinValue(16, float.Parse(r["1269"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(16, 0); }
                        try
                        {
                            SetPinValue(17, float.Parse(r["1270"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(17, 0); }
                        try
                        {
                            if (r["1278"].Equals("Off"))
                            {
                                SetPinValue(18, 0);
                            }
                            else { SetPinValue(18, 1); }
                        }
                        catch (Exception e) { SetPinValue(18, 0); }
                        try
                        {

                            if (r["1281"].Equals("Off"))
                            {
                                SetPinValue(19, 0);
                            }
                            else { SetPinValue(19, 1); }
                        }
                        catch (Exception e) { SetPinValue(19, 0); }


                        SetPinValue(20, 0);
                        double d = 0;
                        try
                        {
                            if (double.TryParse(r["1288"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))//System.Globalization.CultureInfo.InvariantCulture, 
                            {
                                SetPinValue(21, (float)(d));

                            }
                            else { SetPinValue(21, 0); }
                        }
                        catch (Exception e) { SetPinValue(21, 0); }
                        try
                        {
                            //"1284", "1285", "1287", "1288", "1289", "1290", "1291", "1292", "1293", "1294", "1297", "1298", "1299", "1300", "1402" };
              

                            SetPinValue(22, float.Parse(r["1289"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(22, 0); }
                        try
                        {
                            SetPinValue(23, float.Parse(r["1290"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(23, 0); }
                        try
                        {
                            SetPinValue(24, float.Parse(r["1291"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(24, 0); }
                        try
                        {
                            SetPinValue(25, float.Parse(r["1292"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(25, 0); }
                        try
                        {
                            SetPinValue(26, float.Parse(r["1293"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(26, 0); }
                        try
                        {
                            SetPinValue(27, float.Parse(r["1294"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(27, 0); }
                        try
                        {
                            SetPinValue(28, float.Parse(r["1295"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(23, 0); }
                        try
                        {

                            if (double.TryParse(r["1296"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
                            {
                                SetPinValue(29, (float)(d));

                            }
                            else { SetPinValue(29, 0); }
                        }
                        catch (Exception e) { SetPinValue(29, 0); }
                        try
                        {
                            SetPinValue(30, float.Parse(r["1299"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(30, 0); }
                        try
                        {
                            SetPinValue(31, float.Parse(r["1300"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(31, 0); }

                        SetPinValue(32, 0);

                        try
                        {
                            var value = r["1402"];
                            /*
                            if (textValues.ContainsKey(r["1402"].ToString()))
                                value = textValues[r["1402"].ToString()];
                            else
                                value = 4;
                             */ 
                             SetPinValue(33, value);
                            //SetStringValue(33, r["1402"].ToString());
                        }
                        catch (Exception e) { SetPinValue(33, 0); }
                        try
                        {
                            SetPinValue(36, float.Parse(r["1284"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(36, 0); }
                        try
                        {
                            SetPinValue(37, float.Parse(r["1285"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat)); //"449", "464", "467", "470", "473", "588", "684"
                        }
                        catch (Exception e) { SetPinValue(37, 0); }


                        //write data
                        if (b)
                        {
                            if (allowWrite)
                            {
                                SetPinValue(1, 20.0);
                                SetPinValue(2, 20.0);
                                SetPinValue(3, 20.0);
                                SetPinValue(4, 20.0);
                                SetPinValue(5, 20.0);
                                SetPinValue(6, 20.0);                            
                                SetPinValue(7, 35.0);
                                SetPinValue(8, 16.0);
                                SetPinValue(9, 4);
                            }
                            b = false;

                        }
                        
                        
                        {
                            if (allowWrite)
                            {
                                double ecsp = GetPinDouble(22);

                                if ((GetPinDouble(1) != ecsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1289", GetPinValue(1).ToString(), "10.0.60.2");
                                }


                                double pcsp = GetPinDouble(23);

                                if ((GetPinDouble(2) != pcsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1290", GetPinValue(2).ToString(), "10.0.60.2");
                                }

                                double ccsp = GetPinDouble(24);

                                if ((GetPinDouble(3) != ccsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1291", GetPinValue(3).ToString(), "10.0.60.2");
                                }
                                double chsp = GetPinDouble(25);

                                if ((GetPinDouble(4) != chsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1292", GetPinValue(4).ToString(), "10.0.60.2");
                                }

                                double phsp = GetPinDouble(26);

                                if ((GetPinDouble(5) != phsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1293", GetPinValue(5).ToString(), "10.0.60.2");
                                }

                                double ehsp = GetPinDouble(27);

                                if ((GetPinDouble(6) != ehsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1294", GetPinValue(6).ToString(), "10.0.60.2");
                                }

                                double salv_max = GetPinDouble(30);

                                if ((GetPinDouble(7) != salv_max && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1299", GetPinValue(7).ToString(), "10.0.60.2");
                                }

                                double salv_min = GetPinDouble(31);
                                if ((GetPinDouble(8) != salv_min && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "1300", GetPinValue(8).ToString(), "10.0.60.2");
                                }
                            }



                            string prom = GetPinString(33);
                            if (prom != null && textValues.ContainsKey(prom))
                                prom = textValues[prom].ToString();
                            if ((GetPinString(9) != null && !GetPinString(9).Equals(prom) && !b))
                            {
                                var value = GetPinValue(9).ToString();
                                postWriteValueHttps(obj, sessionIPKey, "1402", value, "10.0.60.2");
                            }
                        }
                    }

                    
                    // **********************************
                    // N = 4
                    if (GetPinInt(38) == 4)
                    {
                        //{ "641", "642", "643", "644", "645", "646", "647", "648", "656", "659", 
                        try
                        {
                            SetPinValue(10, float.Parse(r["641"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(10, 0); }
                        try
                        {
                            SetPinValue(11, float.Parse(r["642"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(11, 0); }
                        try
                        {
                            SetPinValue(12, float.Parse(r["643"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(12, 0); }
                        try
                        {
                            SetPinValue(13, float.Parse(r["644"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(13, 0); }
                        try
                        {
                            SetPinValue(14, float.Parse(r["645"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(14, 0); }
                        try
                        {
                            SetPinValue(15, float.Parse(r["646"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(15, 0); }
                        try
                        {
                            SetPinValue(16, float.Parse(r["647"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(16, 0); }
                        try
                        {
                            SetPinValue(17, float.Parse(r["648"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(17, 0); }
                        try
                        {
                            if (r["656"].Equals("Off"))
                            {
                                SetPinValue(18, 0);
                            }
                            else { SetPinValue(18, 1); }
                        }
                        catch (Exception e) { SetPinValue(18, 0); }
                        try
                        {

                            if (r["659"].Equals("Off"))
                            {
                                SetPinValue(19, 0);
                            }
                            else { SetPinValue(19, 1); }
                        }
                        catch (Exception e) { SetPinValue(19, 0); }
                        
                   

                        SetPinValue(20, 0);
                        double d = 0;
                        try
                        {
                            if (double.TryParse(r["665"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))//System.Globalization.CultureInfo.InvariantCulture, 
                            {
                                SetPinValue(21, (float)(d));

                            }
                            else { SetPinValue(21, 0); }
                        }
                        catch (Exception e) { SetPinValue(21, 0); }
                        try
                        {
                         
                            SetPinValue(22, float.Parse(r["667"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(22, 0); }
                        try
                        {
                            SetPinValue(23, float.Parse(r["668"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(23, 0); }
                        try
                        {
                            SetPinValue(24, float.Parse(r["669"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(24, 0); }
                        try
                        {
                            SetPinValue(25, float.Parse(r["670"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(25, 0); }
                        try
                        {
                            SetPinValue(26, float.Parse(r["671"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(26, 0); }
                        // "665", "666", "667", "668", "669", "670", "671", "672", "675", "676", "677", "678", "780" };         
                        try
                        {
                            SetPinValue(27, float.Parse(r["672"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(27, 0); }
                        try
                        {
                            SetPinValue(28, float.Parse(r["673"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(23, 0); }
                        try
                        {

                            if (double.TryParse(r["674"].ToString().Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out d))
                            {
                                SetPinValue(29, (float)(d));

                            }
                            else { SetPinValue(29, 0); }
                        }
                        catch (Exception e) { SetPinValue(29, 0); }
                        try
                        {
                            SetPinValue(30, float.Parse(r["677"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(30, 0); }
                        try
                        {
                            SetPinValue(31, float.Parse(r["678"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(31, 0); }

                        SetPinValue(32, 0);

                        try
                        {
                            SetPinValue(33, r["780"]);
                        }
                        catch (Exception e) { SetPinValue(33, 0); }
                        try
                        {
                            SetPinValue(36, float.Parse(r["662"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                        }
                        catch (Exception e) { SetPinValue(36, 0); }
                        try
                        {
                            SetPinValue(37, float.Parse(r["663"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat)); //"449", "464", "467", "470", "473", "588", "684"
                        }
                        catch (Exception e) { SetPinValue(37, 0); }


                        //write data
                        if (b)
                        {
                            if (allowWrite)
                            {
                                SetPinValue(1, 20.0);
                                SetPinValue(2, 20.0);
                                SetPinValue(3, 20.0);
                                SetPinValue(4, 20.0);
                                SetPinValue(5, 20.0);
                                SetPinValue(6, 20.0);                            
                                SetPinValue(7, 35.0);
                                SetPinValue(8, 16.0);
                                SetPinValue(9, 4);
                            }
                            b = false;
                             
                        }
                        
                        {
                            if (allowWrite)
                            {
                                double ecsp = GetPinDouble(22);

                                if ((GetPinDouble(1) != ecsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "667", GetPinValue(1).ToString(), "10.0.60.2");
                                }


                                double pcsp = GetPinDouble(23);

                                if ((GetPinDouble(2) != pcsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "668", GetPinValue(2).ToString(), "10.0.60.2");
                                }

                                double ccsp = GetPinDouble(24);

                                if ((GetPinDouble(3) != ccsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "669", GetPinValue(3).ToString(), "10.0.60.2");
                                }
                                double chsp = GetPinDouble(25);

                                if ((GetPinDouble(4) != chsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "670", GetPinValue(4).ToString(), "10.0.60.2");
                                }

                                double phsp = GetPinDouble(26);

                                if ((GetPinDouble(5) != phsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "671", GetPinValue(5).ToString(), "10.0.60.2");
                                }

                                double ehsp = GetPinDouble(27);

                                if ((GetPinDouble(6) != ehsp && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "672", GetPinValue(6).ToString(), "10.0.60.2");
                                }

                                double salv_max = GetPinDouble(30);

                                if ((GetPinDouble(7) != salv_max && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "677", GetPinValue(7).ToString(), "10.0.60.2");
                                }

                                double salv_min = GetPinDouble(31);
                                if ((GetPinDouble(8) != salv_min && !b))
                                {
                                    postWriteValueHttps(obj, sessionIPKey, "678", GetPinValue(8).ToString(), "10.0.60.2");
                                }

                               
                               
                            }
                            string prom = GetPinString(33);
                            if (prom != null && textValues.ContainsKey(prom))
                                prom = textValues[prom].ToString();
                            if ((GetPinString(9) != null && !GetPinString(9).Equals(prom) && !b))
                            {
                                postWriteValueHttps(obj, sessionIPKey, "780", GetPinValue(9).ToString(), "10.0.60.2");
                            }
                        }
                    }
                }
                //========================================================================================
//========================================================================================

                b = false;
                SetPinValue(35, "OK");

            } catch (Exception e)
            {
                b = true;
                SetPinValue(35, e);
            }

            isBusy = false;
            first = false;
            writeToLog("End updateData; isBusy=" + isBusy);
        }
         /*
        // метод с циклом для чтения данных
        void setPout(Hashtable r, string[] id, int[] p) {

            int[] p = { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 36, 37 };

            for (int i = 0; i <= id.Length; i++)
            {
                try
                {
                    SetPinValue(p[i], float.Parse(r[id[i]].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat));
                }
                catch (Exception e)
                {
                    try
                    {
                        if (r[id[i]].Equals("Off"))
                        {
                            SetPinValue(p[i], 0);
                        }
                        else if (r[id[i]].Equals("On"))
                        {
                            SetPinValue(p[i], 1);
                        }
                        else if (!r[id[i]].Equals("Off") && !r[id[i]].Equals("On"))
                        {
                            SetPinValue(p[i], r[id[i]]);
                        }
                    }
                    catch (Exception e)
                    {
                        SetPinValue(p[i], 0);
                    }
                          
                }
            }
        }
*/
/*             
        ////////////////////////////////////////////////////////////////
        static Hashtable outputs(string ip, string uris) 
        {
            float f = 0;
            ServicePointManager.ServerCertificateValidationCallback
                            = (obj, certificate, chain, errors) => true;

            Hashtable hash_r = new Hashtable();
           

                foreach (DictionaryEntry de in hash)
                {
                    string resultat = geting("https://" + ip + "/ajax.app?sessionIP=" + sessionIP + "&service=getDp&plantItemId=" + de.Key, googies, uAgent, Acpt, uris);
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
          * */
         /*
        // POST for write values
        static string postWriteValue(string sessionId, string id, string new_value, string ip)
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
          */ 
  /////////////////////
        static string postWriteValueHttps(RequestObject obj, string sessionId, string id, string new_value, string ip)
        {

            writeToLog("Start postWriteValueHttps; id = " + id + "; new_value = " + new_value);
            var result = client.PostWriteValueHttps(sessionId, obj, id, new_value, ip);
            writeToLog("End postWriteValueHttps");
                return result;
/*           
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
           var postData="";
           if (id.Equals("589")) {
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
*/
        }      
    

    }
}



