using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;

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

        public string addXml(Person item)
        {
            if (item is null)
            {
                throw new WebFaultException<string>("400:BadRequest", System.Net.HttpStatusCode.BadRequest);
            }

            foreach (var person in persons)
            {
                if (person.Name == item.Name && person.Age == item.Age && person.Email == item.Email)
                {
                    throw new WebFaultException<string>("409: Conflict", System.Net.HttpStatusCode.Conflict);
                }
            }

            item.Id = curIndex++;
            persons.Add(item);
            return "Dodano nowego klienta o ID = " + item.Id;
        }

        public Person getByIdXml(string Id)
        {
            int id = int.Parse(Id);
            int idx = persons.FindIndex(p => p.Id == id);
            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", System.Net.HttpStatusCode.NotFound);
            }

            return persons.ElementAt(idx);
        }

        public string updateXml(string Id, Person item)
        {
            if (item is null)
            {
                throw new WebFaultException<string>("400:BadRequest", System.Net.HttpStatusCode.BadRequest);
            }

            int id = int.Parse(Id);
            int idx = persons.FindIndex(p => p.Id == id);
            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", System.Net.HttpStatusCode.NotFound);
            }

            persons[idx] = item;
            return "Klient zostal poprawnie zaktualizowany.";
        }

        public string deleteXml(string Id)
        {
            int id = int.Parse(Id);
            int idx = persons.FindIndex(p => p.Id == id);
            if (idx == -1)
            {
                throw new WebFaultException<string>("404: Not Found", System.Net.HttpStatusCode.NotFound);

            }

            persons.RemoveAt(idx);
            return "Usunieto klienta o ID = " + Id;
        }

        public List<Person> getAllXml()
        {
            return persons;
        }

        public int getSizeXml()
        {
            return persons.Count;
        }

        public List<Person> getAllFilterXml(string Name)
        {
            return persons.FindAll(p => p.Name == Name);
        }

        public string addJson(Person item)
        {
            return addXml(item);
        }

        public Person getByIdJson(string Id)
        {
            return getByIdXml(Id);
        }

        public string updateJson(string Id, Person item)
        {
            return updateXml(Id, item);
        }

        public string deleteJson(string Id)
        {
            return deleteXml(Id);
        }

        public int getSizeJson()
        {
            return getSizeXml();
        }

        public List<Person> getAllJson()
        {
            return getAllXml();
        }

        public List<Person> getAllFilterJson(string Name)
        {
            return getAllFilterXml(Name);
        }
    }
}
