using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_CookBook_PSSC.Models.CommonComponents;

namespace MVC_CookBook_PSSC.Models.UserComponents.RecipeComponents
{
    public class MeasuringUnit
    {
        private ShortText measuringUnit;
        List<String> acceptedMeasuringUnits = new List<string>() {"tbs","cup","ml","g","ounce"};
        public MeasuringUnit(ShortText meas) 
        {
            System.Diagnostics.Contracts.Contract.Requires(acceptedMeasuringUnits.Contains(meas.GetText));
            measuringUnit = meas;

        }
        public ShortText GetText { get { return measuringUnit; } }
    }
}
