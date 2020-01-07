using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/// <summary>
/// value object  describes test state
/// In work  - name sais it all 
/// Completed - test is made
/// Done - test is integrated (approved by integrator ) in the genericTestEnv and documented in all places needed 
/// </summary>
namespace WeTestBackend.Models
{
    public enum TestState
    {
        InWork,
        Completed,
        Done
    }
}
