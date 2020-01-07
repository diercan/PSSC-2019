using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace Web.Models.DDD{
    public class Appointment : TableEntity,IEntity, IAggregateRoot
    {
        public string Id { get;  set; }
        public string MedName{ get;  set; } //properties, which represent the state of the entity can be modified only through operations(methods)
        
        public DateTime DOB { get;  set; }  
        
      //  public string PatientName { get; private set; }
        //public string BreedPet { get;  set; }
        public string Symptoms { get;  set; }
        public string Status { get;  set;}
        /// other properties

        public Appointment(AppointmentDto app) { 
            this.PartitionKey = app.PatientName;
            this.RowKey = app.BreedPet;
            PopulateProperties(app);
        }
        
        
         public Appointment(){}

        private void PopulateProperties(AppointmentDto appointment)
        {
            DOB=appointment.DOB;
            MedName=appointment.MedName;
            Symptoms=appointment.Symptoms;
            Status=Convert.ToString(State.Pending);
            //populate other properties
        }

        //operations
        public void ChangeState(string state){
            //change state logic
            if(state=="Accepted")
                Status=Convert.ToString(State.Accepted);
            else if(state=="InProgress")
                Status=Convert.ToString(State.InProgress);
            else if(state=="Finished")
                Status=Convert.ToString(State.Finished);
        }

        public void ChangeMed(string med) //(int id) or (Guid id) better use an id instead of name
        {
            //change med logic
            MedName=med;
        }

        //other operations

    }
}