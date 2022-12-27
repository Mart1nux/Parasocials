namespace ParasocialsPOSAPI.Models
{
    [Flags]
    public enum PositionPermisions
    {
        None = 0,
        ReadAccess = 1,
        WriteAccess = 2,
        UpdateAccess = 4,
        DeleteAccess = 8,
        SensitiveInformationAccess = 16,
    }
}
