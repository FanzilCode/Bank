using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    class Standard : CreditType
    {
        public Standard()
        {
            name = "Стандарт";
            conditions = "Положительная кредитная история";
            rate = 0.004;
            time = 180;
        }
    }
}
