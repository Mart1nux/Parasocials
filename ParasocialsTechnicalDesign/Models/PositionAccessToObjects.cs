namespace ParasocialsPOSAPI.Models
{
    [Flags]
    public enum PositionAccessToObjects
    {
        None = 0,
        Discount = 1,
        Order = 2,
        Product = 4,
        Reservation = 8,
        Tax = 16,
        Customer = 32,
        Employee = 64,
        Position = 128,
        Shift = 256,
        Tip = 512,
        Company = 1024,
        Refunds = 2048,
    }
}
