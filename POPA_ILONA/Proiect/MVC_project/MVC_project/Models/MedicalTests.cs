using System;
namespace MVC_project.Models
{
    public class MedicalTests
    {

        public Guid Id { get; set; }
        //[Required]
        public string Name { get; set; }

        public int Phone { get; set; }

        public TestName TestName { get; set; }

        public string TestedComponent { get; set; }

        public string NormalValues { get; set; }

        public string ResultedValues { get; set; }

        public string Diagnosis { get; set; }

        //[DataType(DataType.Date)]
        public DateTime Date { get; set; }

        //public NormalValues { get; set; }


}

    public enum TestName
    {
        CompleteBloodCount,
        BasicMetabolicPanel,
        ThyroidFunctionTests
    }

}

