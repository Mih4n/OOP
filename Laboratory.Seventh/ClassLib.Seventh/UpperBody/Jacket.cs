using ClassLib.Seventh.Contracts;
using ClassLib.Seventh.Enums;

namespace ClassLib.Seventh.UpperBody;

public record Jacket(
    Size Size,
    Gender Gender,
    Fabric Fabric,
    int PocketsCount
) : IUpperBody;

