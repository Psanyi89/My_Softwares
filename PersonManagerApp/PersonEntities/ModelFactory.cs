using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
