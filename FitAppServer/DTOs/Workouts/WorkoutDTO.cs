using System;
using System.Collections.Generic;

namespace FitAppServer.DTOs.Workouts
{
    public class WorkoutDTO
    {
        public WorkoutDescription Description { get; set; }
        public List<ExerciseDTO> Exercises { get; set; }
    }
}
