using System;
using System.Collections.Generic;
using MVC_project.Models;

namespace MVC_Tests
{
    public class MedicalTestData
    {
        public static List<MedicalTests> MedicalTests = new List<MedicalTests>()
        {
            new MedicalTests
            {
                Id = Guid.NewGuid(),
                Name = "Andreescu Valentina",
                Phone = 0737349508,
                TestName = TestName.CompleteBloodCount,
                Date = DateTime.Now,
                TestedComponent = "Platelets",
                NormalValues = "150,000 to 450,000/mcL",
                ResultedValues = "350,000/mcL",
                Diagnosis = "Healthy",
            },
            new MedicalTests
            {
                Id = Guid.NewGuid(),
                Name = "Morariu Vasile",
                Phone = 0725376538,
                TestName = TestName.ThyroidFunctionTests,
                Date = DateTime.Now,
                TestedComponent = "T3 hormone",
                NormalValues = "100–200 ng/dL",
                ResultedValues = "230 ng/dL",
                Diagnosis = "Grave’s disease",
            }
        };
    }
}
