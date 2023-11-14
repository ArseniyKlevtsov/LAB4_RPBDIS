namespace LAB4_RPBDIS.Models
{
    public class TrainType
    {
        public int TrainTypeId { get; set; }
        public string? TypeName { get; set; }

        public List<Train> Trains { get; set; } = new();
    }
}
