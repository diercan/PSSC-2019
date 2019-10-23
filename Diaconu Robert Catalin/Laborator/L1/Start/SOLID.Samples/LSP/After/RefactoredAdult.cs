namespace SOLID.Samples.Tests.LSP.After
{
    public class Adult : Person
    {
        public Adult(string personType) : base(personType)
        {
            this.PersonType = personType;
        }


    }
}