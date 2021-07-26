using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitAppServer.DataAccess.Entites
{
    public class Workout
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
        public WorkoutType Type { get; set; }
        public User User { get; set; }
    }
}
