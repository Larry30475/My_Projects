using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MyWebService
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/persons", Method = "POST", RequestFormat = WebMessageFormat.Xml)]
        string addXml(Person item);

        [OperationContract]
        [WebGet(UriTemplate = "/persons/{id}", ResponseFormat = WebMessageFormat.Xml)]
        Person getByIdXml(string Id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/persons/{id}", Method = "PUT", RequestFormat = WebMessageFormat.Xml)]
        string updateXml(string Id, Person item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/persons/{id}", Method = "DELETE")]
        string deleteXml(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "/persons/size")]
        int getSizeXml();

        [OperationContract]
        [WebGet(UriTemplate = "/persons")]
        List<Person> getAllXml();

        [OperationContract]
        [WebGet(UriTemplate = "/persons/filter/{name}")]
        List<Person> getAllFilterXml(string Name);

        [OperationContract]
        [WebInvoke(UriTemplate = "/json/persons", Method = "POST", RequestFormat = WebMessageFormat.Json)]
        string addJson(Person item);

        [OperationContract]
        [WebGet(UriTemplate = "/json/persons/{id}", ResponseFormat = WebMessageFormat.Json)]
        Person getByIdJson(string Id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/json/persons/{id}", Method = "PUT", RequestFormat = WebMessageFormat.Json)]
        string updateJson(string Id, Person item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/json/persons/{id}", Method = "DELETE")]
        string deleteJson(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "/json/persons/size")]
        int getSizeJson();

        [OperationContract]
        [WebGet(UriTemplate = "/json/persons", ResponseFormat = WebMessageFormat.Json)]
        List<Person> getAllJson();

        [OperationContract]
        [WebGet(UriTemplate = "json/persons/filter/{name}", ResponseFormat = WebMessageFormat.Json)]
        List<Person> getAllFilterJson(string Name);
    }


    [DataContract]
    public class Person
    {
        [DataMember(Order = 0)]
        public int Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public int Age { get; set; }

        [DataMember(Order = 3)]
        public string Email { get; set; }
    }
}
