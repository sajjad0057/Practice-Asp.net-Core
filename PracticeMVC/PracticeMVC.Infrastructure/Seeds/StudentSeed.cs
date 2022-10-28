using PracticeMVC.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Infrastructure.Seeds
{
    internal class StudentSeed
    {
        internal Student[] Students
        {
            get
            {
                return new Student[]
                {
                    new Student { Id = new Guid("6A969BC5-3F01-4444-90C9-696B230F48EA"), Name="Student 1",Address = "Dhaka",Cgpa = 3.0 },
                    new Student { Id = new Guid("2A8681CB-92C6-4C5C-97F7-5A08833FE6E9"), Name="Student 2",Address = "Rangpur",Cgpa = 3.50 },
                    new Student { Id = new Guid("3A2DF08F-9801-4AF8-BCE5-BB5EB8937188"), Name="Student 3",Address = "Dhaka",Cgpa = 3.70 },
                    new Student { Id = new Guid("D2831A6E-CFFE-4A82-849D-A0DB9800A741"), Name="Student 4",Address = "Natore",Cgpa = 3.50 },
                };
            }
        } 
    }
}
