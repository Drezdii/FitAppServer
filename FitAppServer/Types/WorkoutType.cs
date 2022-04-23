using System;
using System.Collections.Generic;
using FitAppServer.DataAccess.Entities;

namespace FitAppServer.Types;

public class WorkoutType
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<ExerciseType> Exercises { get; set; } = new List<ExerciseType>();
    // Change to GraphQL type
    public WorkoutTypeCode Type { get; set; }
}