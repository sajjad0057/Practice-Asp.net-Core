using FirstDemo.Infrastructure.Entities;

namespace FirstDemo.Infrastructure.Seeds;

public class StudentSeed
{
    internal Student[] StudentsData
    {
        get
        {
            return new Student[]
            {
                    new Student { Id = new Guid("ccc3c3a7-c923-4505-90fe-0a4f4355187d"),Name="Student 1",Address = "Dhaka",Cgpa = 3.0 },
                    new Student { Id = new Guid("d786c18d-9c46-4afb-b117-95076d2bd435"),Name="Student 2",Address = "Rangpur",Cgpa = 3.50 },
                    new Student { Id = new Guid("92fb217d-8026-4ebb-bce9-631e08576c5c"),Name="Student 3",Address = "Dhaka",Cgpa = 3.70 },
                    new Student { Id = new Guid("18587525-9ecc-42e1-bf5a-511cc1fa4a0e"),Name="Student 4",Address = "Natore",Cgpa = 3.50 },
            };
        }
    }
}
