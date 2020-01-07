using MedFind.Interfaces;
using MedFind.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Models
{
    public class SharedModels
    {
        public IMedic medic { get; set; }
        public IStudent student { get; set; }
    }
}
