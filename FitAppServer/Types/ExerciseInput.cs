using System.Collections;
using System.Collections.Generic;

namespace FitAppServer.Types;

public record ExerciseInput(int Id, int ExerciseInfoId, IReadOnlyCollection<SetInput> Sets);