using System.Collections.Generic;


namespace PersonEntities
{
    public class ModelFactory
    {
        public static IPerson CreatePerson()
        {
            return new Person();
        }

        public static IEnumerable<IPerson> CreateListofPerson()
        {
            return new List<Person>();
        }

    }
}
