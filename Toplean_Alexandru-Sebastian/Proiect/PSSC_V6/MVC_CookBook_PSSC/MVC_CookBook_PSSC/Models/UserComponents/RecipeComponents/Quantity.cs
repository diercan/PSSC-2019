using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_CookBook_PSSC.Models.CommonComponents;

namespace MVC_CookBook_PSSC.Models.UserComponents.RecipeComponents
{
    public class Quantity
    {
        private Number number;
        private MeasuringUnit measuringUnit;
        public Quantity(Number number, MeasuringUnit measuringUnit) {
            this.number = number;
            this.measuringUnit = measuringUnit;
        }
        public Quantity GetQuantity { get { return this; } }
        public Number GetNumber { get { return number; } }
        public MeasuringUnit GetMeasuringUnit { get { return this.measuringUnit; } }
    }
}
