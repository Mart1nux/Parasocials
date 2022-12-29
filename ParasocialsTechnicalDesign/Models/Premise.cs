namespace ParasocialsPOSAPI.Models
{
    public class Premise
    {
        public Guid PremiseId { get; set; }
        public PremisesType Type { get; set; }
        public string Location { get; set; }
    }
}
