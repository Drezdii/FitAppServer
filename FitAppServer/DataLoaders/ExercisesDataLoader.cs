using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FitAppServer.Services;
using FitAppServer.Types;
using GreenDonut;

namespace FitAppServer.DataLoaders;

public class ExerciseDataLoader : GroupedDataLoader<int, ExerciseType>
{
    private readonly IWorkoutsService _service;

    public ExerciseDataLoader(IWorkoutsService service, IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _service = service;
    }

    protected override async Task<ILookup<int, ExerciseType>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var exercises = await _service.GetExercisesByWorkoutIdsAsync(keys.ToList());
        return exercises.Select(q => new ExerciseType
        {
            Id = q.Id,
            ExerciseInfoId = q.ExerciseInfoId,
            WorkoutId = q.WorkoutId
        }).ToLookup(q => q.WorkoutId);
    }
}