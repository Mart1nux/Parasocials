namespace ParasocialsPOSAPI.Models
{
    public class AddTip
    {
        public String Giver { get; set; }
        public TipType Type { get; set; }
        public Guid Receiver { get; set; }
    }
}
