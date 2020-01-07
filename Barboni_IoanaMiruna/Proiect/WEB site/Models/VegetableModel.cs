using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegumeDeBelint.Models
{
    public class vegetableModel
    {
        private List<Vegetable> vegetables;

        public vegetableModel()
        {
            this.vegetables = new List<Vegetable>() {
                new Vegetable {
                    Id = "p01",
                    Name = "Ardei Iute",
                    Price = 5,
                    Photo = "ardeiIute.jpg"
                },
                new Vegetable {
                    Id = "p02",
                    Name = "Busuioc",
                    Price = 1,
                    Photo = "busuioc.jpg"
                },
                new Vegetable {
                    Id = "p03",
                    Name = "Dovleac",
                    Price = 3,
                    Photo = "dovleac.jpg",
                },
                 new Vegetable {
                    Id = "p04",
                    Name = "Castravete",
                    Price = 6,
                    Photo = "castravete.jpg"

                },
                 new Vegetable {
                    Id = "p05",
                    Name = "Varză",
                    Price = 3,
                    Photo = "varza.jpg",
                },

                 new Vegetable {
                    Id = "p06",
                    Name = "Roșii",
                    Price = 8,
                    Photo = "rosii.jpg",
                },

                 new Vegetable {
                    Id = "p07",
                    Name = "Conopidă",
                    Price = 5,
                    Photo = "conopida.jpg",
                },

                 new Vegetable {
                    Id = "p08",
                    Name = "Morcovi",
                    Price = 6,
                    Photo = "morcovi.jpg",
                },

                 new Vegetable {
                    Id = "p09",
                    Name = "Vinete",
                    Price = 3,
                    Photo = "vinete.jpg",
                },

                 new Vegetable {
                    Id = "p10",
                    Name = "Cartofi",
                    Price = 3,
                    Photo = "cartofi.jpg",

                },

                 new Vegetable {
                    Id = "p11",
                    Name = "Pătrunjel",
                    Price = 15,
                    Photo = "patrunjel.jpg",
                },

                   new Vegetable {
                    Id = "p12",
                    Name = "Țelină",
                    Price = 3,
                    Photo = "telina.jpg",
                }
            };
        }

        public List<Vegetable> findAll()
        {
            return this.vegetables;
        }

        public Vegetable find(string id)
        {
            return this.vegetables.Single(p => p.Id.Equals(id));
        }

    }
}
