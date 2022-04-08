using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FitAppServer.Services;
using FitAppServer.Types;
using GreenDonut;

namespace FitAppServer.DataLoaders;

public class SetsDataLoader : GroupedDataLoader<int, SetType>
{
    private readonly IWorkoutsService _service;

    public SetsDataLoader(IWorkoutsService service, IBatchScheduler batchScheduler,
        DataLoaderOptions? options = null) : base(batchScheduler, options)
    {
        _service = service;
    }

    protected override async Task<ILookup<int, SetType>> LoadGroupedBatchAsync(IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        var sets = await _service.GetSetsByExerciseIdsAsync(keys.ToList());
        return sets.Select(q => new SetType
        {
            Id = q.Id,
            Reps = q.Reps,
            Weight = q.Weight,
            Completed = q.Completed,
            ExerciseId = q.ExerciseId
        }).ToLookup(q => q.ExerciseId);
    }
}