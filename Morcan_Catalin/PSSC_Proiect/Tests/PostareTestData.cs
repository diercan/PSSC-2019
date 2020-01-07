using System;
using System.Collections.Generic;
using System.Text;
using PSSC.Models.Postare;
using PSSC.Models.Generic;

namespace PSSCTests
{
    public class PostareTestData
    {
        public static List<PostareViewModel> Postari = new List<PostareViewModel>()
        {
            new PostareViewModel()
            {
                id = new Guid(),
                Autor = "a",
                Titlu = "a",
                Body = "a",
                Topic = "a",
                NivelImportanta = (GradImportantaPostare)1,
                Tip= (TipPostare)1
            },
            new PostareViewModel()
            {
                id = new Guid(),
                Autor = "b",
                Titlu = "b",
                Body = "b",
                Topic = "b",
                NivelImportanta = (GradImportantaPostare)1,
                Tip= (TipPostare)1
            }

        };




    }
    
}
