using PersonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerLogic
{
    public interface IPersonManager
    {
        /// <summary>
        /// Getting all persons
        /// </summary>
        /// <returns></returns>
        List<Person> GetPersons();
        /// <summary>
        /// Add a person using a Person Object
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Person AddPerson(Person person);
        /// <summary>
        /// Deletes person with the id or return false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeletePerson(Person person);
        /// <summary>
        /// This will update the person attributes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Person UpdatePerson(Person person);
    }
}
