using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public interface IPhoneRepository
    {
        Phone GetPhone(int Id);
        IEnumerable<Phone> GetAllPhone();
        Phone Add(Phone phone);
        Phone Update(Phone phoneChanges);
        Phone Delete(int id);
    
    }
}
