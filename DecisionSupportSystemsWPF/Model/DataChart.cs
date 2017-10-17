using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionSupportSystemsWPF.Model
{
   public class DataChart
    {
        public KeyValuePair<double, double> rowdata { get; set; }
        public string classcolumn { get; set; }
    }

    public class KNNObj
    {
        public int ObjIndex { get; set; }
        public int ObjName { get; set; }
    }

}
