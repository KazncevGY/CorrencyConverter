using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrencyConverter
{
    class Currency
    {
        public string name
        { get; }
        public float value
        { get; }

        public Currency(string n, float v)
        {
            name = n;
            value = v;
        }

    }
}
