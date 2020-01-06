using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class MockPhoneRepository : IPhoneRepository
    {
        private List<Phone> _phoneList;

        public MockPhoneRepository()
        {
            _phoneList = new List<Phone>()
            {
                new Phone() { Id=1 , Name = "Iphone" , Type="XS MAX" , Color ="black", Dimension = "70.9 x 143.6 x 7.7"},
                new Phone() {Id=2,Name="Samsung", Type="S10",Color="Silver" ,Dimension = "70.9 x 143.6 x 7.7"},
                new Phone() {Id=3, Name="Iphone",Type="XS",Color="Blue" ,Dimension = "70.9 x 143.6 x 7.7"}
            };
        }

        public Phone Add(Phone phone)
        {
           phone.Id= _phoneList.Max(e => e.Id) + 1;
            _phoneList.Add(phone);
            return phone;
        }

        public Phone Delete(int id)
        {
           Phone phone= _phoneList.FirstOrDefault(e => e.Id == id);
            if(phone != null)
            {
                _phoneList.Remove(phone);
            }
            return phone;
        }

        public IEnumerable<Phone> GetAllPhone()
        {
            return _phoneList;
        }

        public Phone GetPhone(int Id)
        {
            return _phoneList.FirstOrDefault(e =>e.Id == Id);
        }

        public Phone Update(Phone phoneChanges)
        {
            Phone phone = _phoneList.FirstOrDefault(e => e.Id == phoneChanges.Id);
            if (phone != null)
            {
                phone.Name = phoneChanges.Name;
                phone.Color = phoneChanges.Color;
                phone.Type = phoneChanges.Type;
                phone.Dimension = phoneChanges.Dimension;
                phone.Price = phoneChanges.Price;
            }
            return phone;
        }
    }
}