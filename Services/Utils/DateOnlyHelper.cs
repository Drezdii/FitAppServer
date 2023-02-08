using System;

namespace FitAppServer.Services.Utils;

public static class DateOnlyHelper
{
    public static DateOnly DateNow() => DateOnly.FromDateTime(DateTime.Now);
}