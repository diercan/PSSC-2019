using MedFind.Interfaces;
using MedFind.Models;
using RabbitMQ.Client.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedFind.Repository
{
    public class RStudent : IStudent
    {
        private static List<Student> List_Student;
        private List<Medic> Lista_Cabinete;
        
        
        public RStudent()
        {
            List_Student = new List<Student>();

            List_Student.Add(new Student { StudentAccount = "Alexandru.Abrudan", Name = "Abrudan Alexandru", StudentID = "ACIS54321", ListCabinets = new List<Medic>() });
            List_Student.Add(new Student { StudentAccount = "Darius.Bagiu", Name = "Bagiu Darius", StudentID = "ACIS12345",ListCabinets = new List<Medic>() });
            List_Student.Add(new Student { StudentAccount = "Madalina.Cristina", Name = "Drehuta Madalina", StudentID = "ACCTI67891", ListCabinets = new List<Medic>() });
        }

        public Student ReturnStudentAfterLoginStudent(LoginStudentViewModel student)
        {
            foreach(Student item in List_Student)
            {
                if (item.StudentAccount.Equals(student.StudentAccount))
                {
                    
                    return item;                    
                }
                
            }
            return null;
        }
 
        public void CreateStudent(Student student)
        {
            List_Student.Add(student);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return List_Student;
        }

        public static Student AddCabinet(Medic medic, string StudentAccount)
        {
            
            foreach (Student item in List_Student)
            {
                if (item.StudentAccount.Equals(StudentAccount))
                {
                   item.ListCabinets.Add(medic);
                   RMedic.AddStudent(item, medic.MedicAccount.ToString());        
                   return item;
                }
            }
            throw new NotImplementedException();

        }

        public Medic SendStudent()
        {
            throw new NotImplementedException();
        }
    }
}
