using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmacie.Models.CommonComponents
{
    public class RealNumber
    {
        private float _float;
        public RealNumber(float floatt)
        {
            _float = floatt;
        }
        public float getRealNumber { get { return _float; } }
        public RealNumber getText { get { return this; } }
    }
}
