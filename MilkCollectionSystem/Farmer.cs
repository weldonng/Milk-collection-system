using System.ComponentModel.DataAnnotations;

namespace MilkCollectionSystem
{
    public class Farmer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [RegularExpression(@"^(?:254|\+254|0)?(7\d{8}|1\d{8})$",
            ErrorMessage = "Enter a valid Kenyan phone number (07XXXXXXXX, 01XXXXXXXX or +2547XXXXXXXX)")]
        public string Phone { get; set; }
        public List<MilkRecord> MilkRecords { get; set; }
        = new List<MilkRecord>();
    }
}