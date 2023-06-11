using System;

namespace Backend.DTOs.Stats;

public record BodyWeightEntryDto(int Id, DateOnly Date, float Weight);