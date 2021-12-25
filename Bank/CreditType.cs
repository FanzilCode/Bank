using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    abstract class CreditType : ICreditType
    {
        // название
        protected string name;
        // условия получения
        protected string conditions;
        // ставка по кредиту
        protected double rate;
        // срок
        protected int time;

        // св-во для поля name
        public string Name
        {
            get
            {
                return name;
            }
        }
        public double GetRate(int count)
        {
            return Math.Pow((1 + rate), count);
        }
        // метод Print() - печать на экран информации о виде кредита
        public void Print(int index)
        {
            Console.WriteLine($"Индекс: {index}\n" +
                $"Название: {name}\n" +
                $"Условия получения: {conditions}\n" +
                $"Ставка: {rate*100}% в день\n" +
                $"Cрок(кол-во дней): {time}\n");
        }
        // перегрузки методов == и !=
        public static bool operator ==(CreditType c1, CreditType c2)
        {
            return c1.name == c2.name;
        }
        public static bool operator != (CreditType c1, CreditType c2)
        {
            return !(c1 == c2);
        }
        // переопределение метода Equals
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            CreditType type = (CreditType)obj;
            return type == this;
        }
        // переопределение метода GetHashCode
        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
        // переопределение метода ToString() для записи в файл
        public override string ToString()
        {
            return $"{name}%{conditions}%{rate}%{time}";
        }
    }
}
