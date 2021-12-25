using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    class Another : CreditType
    {
        public Another(string name, string conditions, double rate, int time)
        {
            this.name = name;
            this.conditions = conditions;
            this.rate = rate;
            this.time = time;
        }
        public Another()
        {
            Console.Write("Название: ");
            name = Console.ReadLine();

            Console.Write("Условия для получения кредита: ");
            conditions = Console.ReadLine();

            Console.WriteLine("Ставка по кредиту(в %) в день: ");
            rate = (double)(Convert.ToDouble(Console.ReadLine()) / 100);

            Console.Write("Срок(кол-во дней): ");
            time = Convert.ToInt32(Console.ReadLine());
        }
        public Another(string[] arr)
        {
            name = arr[0];
            conditions = arr[1];
            rate = Convert.ToDouble(arr[2]);
            time = Convert.ToInt32(arr[3]);
        }
    }
}
