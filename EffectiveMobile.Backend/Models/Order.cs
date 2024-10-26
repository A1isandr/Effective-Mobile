namespace EffectiveMobile.Backend.Models
{
    public class Order
    {
        public int Id { get; set; }

        public double Weight { get; set; }

        public int DistrictId { get; set; }

        public DateTime DueTime { get; set; }
    }
}
