using System.Collections.Generic;

using System.Collections.Generic;

namespace MilkCollectionSystem
{
    public static class DataStore
    {
        public static List<MilkRecord> Records { get; set; }
            = new List<MilkRecord>();

        public static List<Farmer> Farmers { get; set; }
            = new List<Farmer>();
    }
}