using System.Collections.Generic;

namespace FitAppServer.DataAccess.Entites
{
    public class Exercise
    {
        public int ID { get; set; }
        public ExerciseInfo ExerciseInfo { get; set; }
        public int ExerciseInfoID { get; set; }
        public List<Set> Sets { get; set; }
        public Workout Workout { get; set; }
    }
}
