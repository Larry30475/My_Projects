using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Diagnostics;

namespace MyWebService
{
    public class MyRestService : IRestService
    {
        private static List<Person> persons = new List<Person>(){
            new Person{Id = 1, Name = "Petro", Age = 15, Email = "PetroPetro@gmail.com"},
            new Person{Id = 2, Name = "Wiktor", Age = 43, Email = "Wiktor1980@gmail.com"},
            new Person{Id = 3, Name = "Kamil", Age = 24, Email = "Kamil12345@gmail.com"}
        };

        private static int curIndex = 4;

        public void addXml(Person item)
        {
            Debug.WriteLine("addXml Called");
            if (item is null)
            {
                throw new WebFaultException<string>("400:BadRequest", HttpStatusCode.BadRequest);
            }

            foreach (var person in persons)
            {
                if (person.Name == item.Name && person.Age == item.Age && person.Email == item.Email)
                {
                    throw new WebFaultException<string>("409: Conflict", HttpStatusCode.Conflict);
                }
            }

            item.Id = curIndex++;
            persons.Add(item);
            WebOperationContext.Current.OutgoingResponse.StatusDescription = "Success creating new user";
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Created;
        }

        public Person getByIdXml(string Id)
        {
            Debug.WriteLine("getByIdXml Called");
            int id = int.Parse(Id);
            int idx = persons.FindIndex(p => p.Id == id);
            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            }

            return persons.ElementAt(idx);
        }

        public void updateXml(string Id, Person item)
        {
            Debug.WriteLine("updateXml Called");
            if (item is null)
            {
                throw new WebFaultException<string>("400:BadRequest", HttpStatusCode.BadRequest);
            }

            int id = int.Parse(Id);
            int idx = persons.FindIndex(p => p.Id == id);
            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            }

            persons[idx] = item;
            WebOperationContext.Current.OutgoingResponse.StatusDescription = "Success updating the user";
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
        }

        public void deleteXml(string Id)
        {
            Debug.WriteLine("deleteXml Called");
            int id = int.Parse(Id);
            int idx = persons.FindIndex(p => p.Id == id);
            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            }

            persons.RemoveAt(idx);
            WebOperationContext.Current.OutgoingResponse.StatusDescription = "Success deleting the user";
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
        }

        public List<Person> getAllXml()
        {
            Debug.WriteLine("getAllXml Called");
            return persons;
        }

        public int getSizeXml()
        {
            Debug.WriteLine("getSizeXml Called");
            return persons.Count;
        }

        public List<Person> getAllFilterXml(string Name)
        {
            Debug.WriteLine("getAllFilterXml Called");
            return persons.FindAll(p => p.Name == Name);
        }

        public string getCreatorsNamesXml()
        {
            Debug.WriteLine("getCreatorsNamesXml Called");
            string name1 = "Wiktor Sadowy 260373";
            string name2 = "Ivan Luzhanskyi 247372";
            return name1 + "\n" + name2;
        }

        public void addJson(Person item)
        {
            Debug.WriteLine("addJson Called");
            addXml(item);
        }

        public Person getByIdJson(string Id)
        {
            Debug.WriteLine("getByIdJson Called");
            return getByIdXml(Id);
        }

        public void updateJson(string Id, Person item)
        {
            Debug.WriteLine("updateJson Called");
            updateXml(Id, item);
        }

        public void deleteJson(string Id)
        {
            Debug.WriteLine("deleteJSON Called");
            deleteXml(Id);
        }

        public int getSizeJson()
        {
            Debug.WriteLine("getSizeJson Called");
            return getSizeXml();
        }

        public List<Person> getAllJson()
        {
            Debug.WriteLine("getAllJson Called");
            return getAllXml();
        }

        public List<Person> getAllFilterJson(string Name)
        {
            Debug.WriteLine("getAllFilterJson Called");
            return getAllFilterXml(Name);
        }

        public string getCreatorsNamesJson()
        {
            Debug.WriteLine("getCreatorsNamesJson Called");
            return getCreatorsNamesXml();
        }
    }
}
