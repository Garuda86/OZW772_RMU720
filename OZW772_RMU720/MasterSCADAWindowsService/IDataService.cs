using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MasterSCADAWindowsService
{
    [DataContract]
    public class RequestObject
    {
        [DataMember]
        public string URL;
        [DataMember]
        public string URLs;
        [DataMember]
        public string URI;
        [DataMember]
        public string[] IDs;
        [DataMember]
        public string[] IDsForRequests;
        /// <summary>
        /// 
        /// </summary>
        public System.Collections.Hashtable postHash;
    }

    [DataContract]
    public class SCADAObject
    {
        [DataMember]
        public string key;
        [DataMember]
        public string value;
    }

    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IDataService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        /*
        [WebGet(UriTemplate = "GetData?ip={ip}&uris={uris}",
            BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Xml)]
         */
        SCADAObject[] GetData(string sessionIP, RequestObject requestObject);

        [OperationContract]
        string PostWriteValueHttps(string sessionIP, RequestObject requestObject, string id, string new_value, string ip);

        [OperationContract]
        void Init(string sessionIP, RequestObject requestObjects);

        [OperationContract]
        void StartUpdating(string sessionIP, RequestObject requestObjects);

        [OperationContract]
        void StopUpdating(string sessionIP, RequestObject requestObjects);


    }
}
