using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFFiltragemSinal
{
    internal class SeismicData
    {
        public string name { get; set; }
        public float interval { get; set; }
        public float[] data { get; set; }
        public float[] modifiedData { get; set; }
        public float highPassFrequencyCut { get; set; }
        public float lowPassFrequencyCut { get; set; }
    }
}
