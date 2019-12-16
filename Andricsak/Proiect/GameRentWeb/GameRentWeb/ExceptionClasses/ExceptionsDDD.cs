using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRentWeb.ExceptionClasses
{
    public class NotEnoughRents : Exception
    {
        public NotEnoughRents()
        {

        }
        public NotEnoughRents(int rentsNumber)
            : base(string.Format("Not enough rents, rents - {0}",rentsNumber))
        {

        }
    }
    public class ItemNotFound : Exception
    {
        public ItemNotFound()
        {

        }

        public ItemNotFound(string name)
            : base(string.Format("Item: {0}, doesn't exist", name))
        {

        }
    }
}
