using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.Models
{
    public class IntervalModel
    {
        public double a { get; set; }
        public double b { get; set; }

        public string IntervalString
        {
            get { return a.ToString() + " : " + b.ToString(); }
            private set { }
        }

        public override bool Equals(object obj)
        {
            var o = obj as IntervalModel;

            if (o is IntervalModel)
            {
                return o.a == a && o.b == b;
            }

            return base.Equals(obj);

        }
    }
}
