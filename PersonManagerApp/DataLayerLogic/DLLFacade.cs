using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayerLogic.Managers;
using PersonEntities;
namespace DataLayerLogic
{
    public class DLLFacade
    {
        public IPersonManager GetPersonManagerMemory()
        {
            return new PersonManagerFakeDB();
            //return new ProxyPersonManager();
        }
        public IPersonManager GetPersonManagerTxt()
        {
            return new TxtPersonManager();
        }
    }
}
