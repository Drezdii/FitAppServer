using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FitAppServer.DataAccess.Entities;
using FitAppServer.DataLoaders;
using GreenDonut;
using HotChocolate;

namespace FitAppServer.Types;

public class WorkoutType
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public WorkoutTypeCode Type { get; set; }
}