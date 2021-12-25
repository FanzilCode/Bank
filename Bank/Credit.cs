using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    class Credit
    {
        // вид кредита
        ICreditType creditType;
        // клиент
        Client client;
        // сумма кредита
        double sum;
        // дата выдачи
        DateTime date;
        // дата погашения
        DateTime datePay;
        // статус (погашен или нет)
        bool status;

        // конструктор с 4-мя параметрами
        public Credit(ICreditType creditType, Client client, double sum, DateTime date)
        {
            this.creditType = creditType;
            this.client = client;
            this.sum = sum;
            this.date = date;
            status = false;

            datePay = date;
        }
        // метод Print() - печать на экран информации по кредиту
        public void Print(int index)
        {
            Console.WriteLine($"Индекс: {index}\n" +
                $"Вид кредита: {creditType.Name}\n" +
                $"Клиент: {client.Title}\n" +
                $"Cумма кредита: {sum} руб.\n" +
                $"Дата выдачи: {date.ToShortDateString()}\n" +
                $"Погашен? : {status}\n");
        }
        // переопределение метода ToString() для записи в файл
        public override string ToString()
        {
            return $"{creditType}\n" +
                $"{client}\n" +
                $"{sum}%{date.ToShortDateString()}";
        }
        public double Pay(DateTime date)
        {
            int count = (date - datePay).Days;
            sum = sum * creditType.GetRate(count);
            Console.Write($"Текущий долг по кредиту: {sum} руб.\n\n" +
                $"Введите сумму, которую хотите внести: ");
            double pay = Convert.ToDouble(Console.ReadLine());
            sum -= pay;
            if(sum <= 0)
            {
                sum = 0;
                Console.WriteLine("Кредит погашен.");
                status = true;
            }
            else
            {
                Console.WriteLine($"Остаток по кредиту: {sum} руб.");
            }
            return pay;
        }
        // перегрузки операторов == и !=
        public static bool operator ==(Credit c1, Credit c2)
        {
            return c1.creditType.Name == c2.creditType.Name && c1.client == c2.client && c1.sum == c2.sum && c1.date == c2.date;
        }
        public static bool operator !=(Credit c1, Credit c2)
        {
            return !(c1 == c2);
        }
        // переопределение метода Equals()
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            Credit credit = (Credit)obj;
            return this == credit;
        }
        // переопределение метода GetHashCode()
        public override int GetHashCode()
        {
            return creditType.Name.GetHashCode() + client.GetHashCode() + sum.GetHashCode() + date.GetHashCode();
        }
        public bool IsAvailable(Client client)
        {
            return client == this.client;
        }
        public bool IsAvailable(ICreditType creditType)
        {
            return this.creditType == creditType;
        }
        public bool IsRepaid(bool status)
        {
            return this.status == status;
        }
    }
}
