using System.Collections.Generic;

namespace MovieApp.Api.Model
{
    public class Character
    {
        public int id { get; set; }

        public string name { get; set; }

        public string status { get; set; }

        public string species { get; set; }

        public string type { get; set; }

        public string gender { get; set; }

        public string image { get; set; }

        public Origin origin { get; set; }

        public Location location { get; set; }

        public List<string> episode { get; set; }

        public string url { get; set; }

        public string created { get; set; }
    }
}
