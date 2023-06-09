using System;

namespace Backend.DTOs.Stats;

public record BodyWeightDto(DateOnly Date, float Weight);