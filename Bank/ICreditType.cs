using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    interface ICreditType
    {
        public string Name { get; }
        public void Print(int index);
        public double GetRate(int count);
    }
}
