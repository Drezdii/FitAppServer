using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitAppServer.DataAccess.Entities;

public class Workout
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateOnly? Date { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    public WorkoutTypeCode Type { get; set; }
    public User User { get; set; } = null!;
    public int UserId { get; set; }
    public WorkoutProgramDetail? WorkoutProgramDetails { get; set; }
}