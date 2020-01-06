using System.Collections.Generic;
using SOLID.Samples.OCP.After.Specs;
using SOLID.Samples.Tests.OCP.After;
/*
 * In varianta initiala adaugarea unui nou tip de filtru
 * necesita adaugarea unei noi metode de filtrare in clasa ProductFilter
 * modificand astfel clasa si incalcand OCP.
 * In varianta refactorizata clasa ProductFilter nu trebuie modificata
 * pentru ca un client sa poata adauga un nou tip de filtru.
 * Acest lucru este posibil prin construirea in mod generic a filtrelor astfel:
 * exista o interfata ISpecification prin care fiecare clasa concreta ce o implementeaza
 * trebuie sa aibe o metoda care sa returneze daca conditia de filtrare e indeplinita
 * astfel un filtru de produse e construit folosind o specificatie predefinita sau definita de client.
 */
namespace SOLID.Samples.OCP.After
{
	public class Consumer
	{
		public Consumer()
		{
			Filter = new ProductFilter();
		}

		protected ProductFilter Filter { get; set; }

		public void FilterProductsByColor()
		{
            var greenFilteredProducts = Filter.Filter(new List<Product>(), new ColorSpecification(Color.Green));
		}
        public void FilterByColorAndPrice()
        {
            var redPricyFilteredProducts = Filter.Filter(new List<Product>(),
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Red), 
                    new PriceSpecification(10)
                ));
        }
	}
}
