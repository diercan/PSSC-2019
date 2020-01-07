using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
///  A group of functional requirements = functionality 
///  tests will belong to a functionality that  can be found on more projects
///  so it is not unique(identifier) but immutable  so it can be considered a value object?   
///  
/// 
/// 
/// 
/// </summary>
namespace WeTestBackend.Models
{
    public enum Functionality
    {
        GATEWAY,
        DTC,
        CAN_COMMUNICATION,
        FLEXRAY_COMMUNICATION,
        LIN_COMMUNICATION,
        UDS,
        FUNCTIONAL_SAFETY
    }
}
