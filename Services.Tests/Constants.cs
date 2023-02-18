using System;
using FitAppServer.DataAccess.Entities;

namespace Services.Tests;

public static class Constants
{
    public const int USER_ID = 1;
    public const string USER_EMAIL = "test_email";
    public const string USER_USERNAME = "test_user";
    public const string USER_UUID = "test_uuid";

    public const int WORKOUT_ID = 1000;
    public static DateOnly WORKOUT_DATE = new(2023, 1, 1);
    public static DateTime WORKOUT_START_DATE = new(2023, 1, 1, 9, 0, 0, DateTimeKind.Utc);
    public static DateTime WORKOUT_END_DATE = new(2023, 1, 1, 10, 0, 0, DateTimeKind.Utc);
}