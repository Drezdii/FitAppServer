using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitAppServer.DTOs.Workouts
{
    public class SetDTO
    {
        public int ID { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
        public bool Completed { get; set; }
    }
}
