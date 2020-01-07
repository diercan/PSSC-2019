using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPlanner.Models.DDD
{
    public class CalendarDate
    {
        private DateTime _date;
        public DateTime Date { get { return _date; } }

        public CalendarDate(DateTime date)
        {
            //Contract.Requires<ArgumentNullException>(text != null, "text");
            //Contract.Requires<ArgumentCannotBeEmptyStringException>(!string.IsNullOrEmpty(text), "text");

            _date = date;
        }

        #region override object
        public override string ToString()
        {
            return Date.ToString();
        }

        public override bool Equals(object obj)
        {
            var nume = (CalendarDate)obj;
            return Date.Equals(nume.Date);
        }

        public override int GetHashCode()
        {
            return Date.GetHashCode();
        }
        #endregion
    }
}
