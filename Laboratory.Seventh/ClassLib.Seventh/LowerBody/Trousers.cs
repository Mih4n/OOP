using ClassLib.Seventh.Contracts;
using ClassLib.Seventh.Enums;

namespace ClassLib.Seventh.LowerBody;

public record Trousers(
    int Id,
    Size Size,
    Gender Gender,
    Fabric Fabric,
    string Brand
) : ILowerBody;

