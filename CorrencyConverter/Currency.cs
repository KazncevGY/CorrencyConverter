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
        public string code
        { get; }
        public decimal value
        { get; }
        public bool isSelected
        { get; set;  }


        public Currency(string n, string c, decimal v)
        {
            name = n;
            code = c;
            value = v;
            isSelected = false;
        }

        public decimal convert(decimal amount, Currency target)
        {
            return amount * value / target.value;
        }
    }
}
