using System.Collections.Generic;
using System.Linq;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository
{
    public class LocalPaysRepository : IPaysRepository
    {
        void IPaysRepository.Add(Pays pays)
        {
            throw new System.NotImplementedException();
        }

        int IPaysRepository.Count()
        {
            throw new System.NotImplementedException();
        }

        void IPaysRepository.Delete(Pays pays)
        {
            throw new System.NotImplementedException();
        }

        Pays IPaysRepository.Find(int id)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Pays> IPaysRepository.FindAll()
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Pays> IPaysRepository.Take(int index, int length)
        {
            throw new System.NotImplementedException();
        }

        void IPaysRepository.Update(Pays pays)
        {
            throw new System.NotImplementedException();
        }
    }
}
