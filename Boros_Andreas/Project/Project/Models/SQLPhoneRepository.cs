using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class SQLPhoneRepository : IPhoneRepository
    {
        private readonly AppPhoneDBContext context;
        public SQLPhoneRepository(AppPhoneDBContext context)
        {
            this.context = context;
        }

        public Phone Add(Phone phone)
        {
            context.Phones.Add(phone);
            context.SaveChanges();
            return phone;
        }

        public Phone Delete(int id)
        {
          Phone phone=  context.Phones.Find(id);
            if(phone!=null)
            {
                context.Phones.Remove(phone);
                context.SaveChanges();
            }
            return phone;
        }

        public IEnumerable<Phone> GetAllPhone()
        {
            return context.Phones;
        }

        public Phone GetPhone(int Id)
        {
          return context.Phones.Find(Id);
        }

        public Phone Update(Phone phoneChanges)
        {
             var phone =  context.Phones.Attach(phoneChanges);
            phone.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return phoneChanges;

        }
    }
}
