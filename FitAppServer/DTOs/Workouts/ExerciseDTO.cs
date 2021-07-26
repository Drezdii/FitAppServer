using System.Collections.Generic;

namespace FitAppServer.DTOs.Workouts
{
    public class ExerciseDTO
    {
        public int ID { get; set; }
        public int ExerciseInfoID { get; set; }
        public List<SetDTO> Sets { get; set; }
    }
}
