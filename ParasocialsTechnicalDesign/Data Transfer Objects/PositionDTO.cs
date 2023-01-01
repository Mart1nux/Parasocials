using ParasocialsPOSAPI.Models;
namespace ParasocialsPOSAPI.Data_Transfer_Objects;

public class PositionDTO 
{
        public Guid PositionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PositionPermisions Permisions { get; set; }
        public PositionAccessToObjects AccessToObjects { get; set; }
}