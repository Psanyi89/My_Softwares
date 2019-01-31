using PersonEntities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

namespace DataLayerLogic.Managers
{
    internal class ProxyPersonManager : IPersonManager
    {
        public List<Person> GetPersons()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response =
                    client.GetAsync("https://easv-person.herokuapp.com/api/persons/").Result;
                return response.Content.ReadAsAsync<List<Person>>().Result;
            }
        }

        public Person AddPerson(Person p)
        {
            using (var client = new HttpClient())
            {
                var formatter = new JsonMediaTypeFormatter
                {
                    SerializerSettings = new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }
                };
                HttpResponseMessage response =
                    client.PostAsync("https://easv-person.herokuapp.com/api/persons/", p, formatter).Result;
                return response.Content.ReadAsAsync<Person>().Result;
            }
        }

        public bool DeletePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public Person UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
