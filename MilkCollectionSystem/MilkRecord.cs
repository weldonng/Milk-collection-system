namespace MilkCollectionSystem
{
    public class MilkRecord
    {
        public int Id { get; set; }

        public int FarmerId { get; set; }

        public Farmer Farmer { get; set; }

        public double Litres { get; set; }

        public double Amount { get; set; }

        public DateTime CollectionDate { get; set; }
    }
}