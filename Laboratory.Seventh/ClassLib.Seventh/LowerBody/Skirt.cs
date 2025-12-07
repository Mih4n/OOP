using ClassLib.Seventh.Contracts;
using ClassLib.Seventh.Enums;

namespace ClassLib.Seventh.LowerBody;

public record Skirt(
    int Id,
    Size Size,
    Gender Gender,
    Fabric Fabric,
    float ConeRatio
) : ILowerBody;

