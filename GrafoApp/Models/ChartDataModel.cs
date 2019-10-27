using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrafoApp.Models
{
    public class ChartDataModel
    {
        public ChartDataModel()
        {
            backgroundColor = "rgba(0, 0, 0, 1)";
            borderColor = "rgba(0, 0, 0, 0.5)";
        }

        public string backgroundColor { get; }
        public string borderColor { get; }
        public List<DataVertice> data { get; set; }
    }

    public class DataVertice
    {
        public string label { get; set; }
        public decimal x { get; set; }
        public decimal y { get; set; }
    }
}
