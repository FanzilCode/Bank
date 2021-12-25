using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    class Universal : CreditType
    {
        public Universal()
        {
            name = "Универсальный";
            conditions = "Положительная кредитная история";
            rate = 0.005;
            time = 90;
        }
    }
}
