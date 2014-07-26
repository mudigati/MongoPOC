using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;

namespace MongoPOC.Models
{
    public class Employee
    {
        //public string EmployeeId { get; set; }

        public ObjectId _id { get; set; }

        public string EmployeeName { get; set; }
    }
}