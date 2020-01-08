using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProiectPSSC.Data;

namespace ProiectPSSC.Models
{
    public static class SeedData
    {
        static List<string> CamereBaieti = new List<string>();
        static List<string> EtajeBaieti = new List<string>();

        static List<string> CamereFete = new List<string>();
        static List<string> EtajeFete = new List<string>();

        static int CurrentIndexB = 0;
        static int CurrentIndexF = 0;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new StudentsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<StudentsContext>>()))
            {
                // Look for any movies.
                if (context.Student.Any())
                {
                    return;   // DB has been seeded
                }

                // This will get the current WORKING directory (i.e. \bin\Debug)
                string workingDirectory = Environment.CurrentDirectory;
                // or: Directory.GetCurrentDirectory() gives the same result

                // This will get the current PROJECT directory
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

                string projectName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

                string FileNameCamin = "CaminConfig.txt";

                string pathCamin = projectDirectory + "\\" + FileNameCamin;

                string FileNameStudenti = "Students_List.txt";

                string pathStudenti = projectDirectory + "\\" + FileNameStudenti;

                int NrEtaje=0;

                int NrCamere = 0;
                int NrLocuri = 0;
                int flag = 0;
                string[] Etaje = new string[6];               

                string linie;
                System.IO.StreamReader fisier =
                    new System.IO.StreamReader(pathCamin);

                if((linie = fisier.ReadLine())!=null)
                {
                    flag++;
                    string[] segmente = linie.Split('/');
                    NrEtaje = int.Parse(segmente[0]);
                    NrCamere = int.Parse(segmente[1]);
                    NrLocuri = int.Parse(segmente[2]);
                    for(int i=0;i<6;i++)
                    {
                        Etaje[i] = segmente[i + 3];
                    }
                    for(int j=0;j< NrEtaje; j++)
                    {
                        if(Etaje[j] != "0")
                        {
                            for(int i=0;i<(NrCamere*NrLocuri);i++)
                            {
                                if(Etaje[j] == "F")
                                {
                                    EtajeFete.Add(j.ToString());
                                }
                                else
                                {
                                    EtajeBaieti.Add(j.ToString());
                                }
                            }

                            List<string> NumarCamera = new List<string>();

                            for(int i=1;i<=NrCamere;i++)
                            {
                                string numar = j.ToString();

                                if(i<10)
                                {
                                    numar = numar + "0" + i.ToString();
                                }
                                else
                                {
                                    numar = numar + i.ToString();
                                }

                                NumarCamera.Add(numar);
                            }

                            foreach(string nr in NumarCamera)
                            {
                                if (Etaje[j] == "F")
                                {
                                    for(int i=0;i<NrLocuri;i++)
                                    {
                                        CamereFete.Add(nr);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < NrLocuri; i++)
                                    {
                                        CamereBaieti.Add(nr);
                                    }
                                }
                            }
                        }
                    }
                }

                string line;
                System.IO.StreamReader file =
                    new System.IO.StreamReader(pathStudenti);
                if (flag == 0)
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        //System.Console.WriteLine(line);
                        string[] fields = line.Split('/');
                        context.Student.Add(
                        new Student
                        {
                            Nume = fields[0],
                            Prenume = fields[1],
                            An = int.Parse(fields[2]),
                            Facultate = fields[3],
                            Etaj = 0,
                            Sex = fields[4],
                            TaxaAchitata = "Nu"
                        }
                        );
                    }
                }
                else
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] fields = line.Split('/');

                        if(fields[4] == "F")
                        {
                            context.Student.Add(
                            new Student
                            {
                                Nume = fields[0],
                                Prenume = fields[1],
                                An = int.Parse(fields[2]),
                                Facultate = fields[3],
                                Camera = CamereFete[CurrentIndexF],
                                Etaj = int.Parse(EtajeFete[CurrentIndexF]),
                                Sex = fields[4],
                                TaxaAchitata = "Nu"
                            }
                            );
                            CurrentIndexF++;
                        }
                        else
                        {
                            context.Student.Add(
                            new Student
                            {
                                Nume = fields[0],
                                Prenume = fields[1],
                                An = int.Parse(fields[2]),
                                Facultate = fields[3],
                                Camera = CamereBaieti[CurrentIndexB],
                                Etaj = int.Parse(EtajeBaieti[CurrentIndexB]),
                                Sex = fields[4],
                                TaxaAchitata = "Nu"
                            }
                            );
                            CurrentIndexB++;
                        }
                    }

                }

                context.SaveChanges();
            }
        }
    }
}
