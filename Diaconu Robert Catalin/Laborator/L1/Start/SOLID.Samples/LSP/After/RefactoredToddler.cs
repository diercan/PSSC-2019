namespace SOLID.Samples.Tests.LSP.After
{
    public class Toddler : Person
    {
        public Toddler(string personType) : base(personType)
        {
            this.PersonType = personType;
        }
    }
}