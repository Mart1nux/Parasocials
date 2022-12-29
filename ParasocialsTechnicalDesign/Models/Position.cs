namespace ParasocialsPOSAPI.Models
{
    public class Position
    {
        public Guid PositionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PositionPermisions Permisions { get; set; }
        public PositionAccessToObjects AccessToObjects { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
