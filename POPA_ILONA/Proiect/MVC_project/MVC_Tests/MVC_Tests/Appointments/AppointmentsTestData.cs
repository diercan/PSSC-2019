using System;
using System.Collections.Generic;
using MVC_project.Models;

namespace MVC_Tests
{
    public class AppointmentsTestData
    {
        public static List<Appointments> Appointments = new List<Appointments>()
        {
            new Appointments
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "Matei Alexandru",
                Phone = 0727032456,
                TestType = TestType.BasicMetabolicPanel
            },
            new Appointments
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Name = "Albulescu Adrian",
                Phone = 0773354587,
                TestType = TestType.CompleteBloodCount
            }
        };
    }
}
