using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitAppServer.DataAccess.Entites
{
    public class ExerciseInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
