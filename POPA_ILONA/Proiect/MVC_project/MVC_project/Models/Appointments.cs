using System;
namespace MVC_project.Models
{
    public class Appointments
    {
        public Guid Id { get; set; }

        //[DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public TestType TestType { get; set; }

        //[Required]
        public string Name { get; set; }

        public int Phone { get; set; }
    }

    public enum TestType
    {
        CompleteBloodCount,
        BasicMetabolicPanel,
        ThyroidFunctionTests
    }
}
