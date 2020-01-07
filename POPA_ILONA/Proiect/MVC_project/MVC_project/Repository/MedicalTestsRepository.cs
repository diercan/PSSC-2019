using System;
using System.Collections.Generic;
using System.Linq;
using MVC_project.Models;

namespace MVC_project.Repository
{
    public interface IMedicalTestsRepository
    {
        void CreateMedicalTests(MedicalTests medicalTests);
        List<MedicalTests> GetAllMedicalTests();
        MedicalTests GetMedicalTestsById(Guid id);
        void DeleteMedicalTests(MedicalTests medicalTests);
        void EditMedicalTests(MedicalTests medicalTests);
    }

    public class MedicalTestsRepository : IMedicalTestsRepository
    {
        private readonly List<MedicalTests> List;

        public MedicalTestsRepository()
        {
            List = new List<MedicalTests>(); 
            List.Add(new MedicalTests
            {
                Id = Guid.NewGuid(),
                Name = "Popescu Andreea",
                Phone = 0787389508,
                TestName = TestName.CompleteBloodCount,
                Date = DateTime.Now,
                TestedComponent = "Platelets",
                NormalValues = "150,000 to 450,000/mcL",
                ResultedValues = "350,000/mcL",
                Diagnosis = "Healthy",
            });
            List.Add(new MedicalTests
            {
                Id = Guid.NewGuid(),
                Name = "Marinescu Sorin",
                Phone = 0735541232,
                TestName = TestName.BasicMetabolicPanel,
                Date = DateTime.Now,
                TestedComponent = "Glucose",
                NormalValues = "70-99 mg/dL",
                ResultedValues = "150 mg/dL",
                Diagnosis = "Diabetes",

            });
            List.Add(new MedicalTests
            {
                Id = Guid.NewGuid(),
                Name = "Andreescu Vasile",
                Phone = 0763241327,
                TestName = TestName.ThyroidFunctionTests,
                Date = DateTime.Now,
                TestedComponent = "T3 hormone",
                NormalValues = "100–200 ng/dL",
                ResultedValues = "230 ng/dL",
                Diagnosis = "Grave’s disease",

            });
        }

        public void CreateMedicalTests(MedicalTests medicalTests)
        {
            List.Add(medicalTests);
        }

        public void DeleteMedicalTests(MedicalTests medicalTests)
        {
            List.Remove(medicalTests);
        }

        public void EditMedicalTests(MedicalTests medicalTests)
        {
            var test = List.FirstOrDefault(m=>m.Id==medicalTests.Id); 
            test.Name = medicalTests.Name;
            test.Phone = medicalTests.Phone;
            test.TestName = medicalTests.TestName;
            test.Date = medicalTests.Date;
            test.TestedComponent = medicalTests.TestedComponent;
            test.NormalValues = medicalTests.NormalValues;
            test.ResultedValues = medicalTests.ResultedValues;
            test.Diagnosis = medicalTests.Diagnosis;

        }
  
        public List<MedicalTests> GetAllMedicalTests()
        {
            return List;
        }

        public MedicalTests GetMedicalTestsById(Guid id)
        {
            return List.FirstOrDefault(_ => _.Id == id);
        }
    }
}
