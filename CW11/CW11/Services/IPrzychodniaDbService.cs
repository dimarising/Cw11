using CW11.Models;
using System.Collections.Generic;


namespace CW11.Services
{
    public interface IPrzychodniaDbService
    {
        public IEnumerable<Doctor> GetDoctors();
        public string AddDoctor(Doctor doctor);

        public string ModifyDoctor(Doctor doctor);

        public string DropDoctor(int IdDoctor);
    }
}
