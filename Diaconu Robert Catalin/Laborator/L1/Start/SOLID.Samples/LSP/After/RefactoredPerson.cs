namespace SOLID.Samples.Tests.LSP.After
{  
	public abstract class Person
	{
        //not sure about this
        protected string personType;
            public Person(string personType)
            {
                this.PersonType = personType;
            }

        public string PersonType { get => personType; set => personType = value; }
    }
}