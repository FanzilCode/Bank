using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    class Payment
    {
        // кредит
        Credit credit;
        // дата погашения
        DateTime date;
        // сумма к погашению
        double sum;

        // конструктор c 1 параметром
        public Payment(Credit credit)
        {
            this.credit = credit;
            Console.Write("Введите дату погашения(в формате ДД.ММ.ГГГГ): ");
            date = Convert.ToDateTime(Console.ReadLine());
            sum = credit.Pay(date);
        }
        // конструктор с 3-мя параметрами
        public Payment(Credit credit, DateTime date, double sum)
        {
            this.credit = credit;
            this.date = date;
            this.sum = sum;
        }
        // метод Print - печать на экран информации по погашению
        public void Print(int index)
        {
            Console.Write($"Индекс: {index}\n" +
                $"Кредит:");
            credit.Print(0);
            Console.WriteLine($"Дата погашения: {date.ToShortDateString()}\n" +
                $"Сумма погашения: {sum}\n");
        }
        // перегрузки операторов == и !=
        public static bool operator ==(Payment p1, Payment p2)
        {
            return p1.credit == p2.credit && p1.date == p2.date && p1.sum == p2.sum;
        }
        public static bool operator !=(Payment p1, Payment p2)
        {
            return !(p1 == p2);
        }
        // переопределение метода Equals
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            Payment payment = (Payment)obj;
            return payment == this;
        }
        // переопределение метода GetHashCode()
        public override int GetHashCode()
        {
            return credit.GetHashCode() + date.GetHashCode() + sum.GetHashCode();
        }
        // переопределение метода ToString() для записи в файл
        public override string ToString()
        {
            return $"{credit}\n" +
                $"{date.ToShortDateString()}%{sum}";
        }
        public bool IsAvailable(Client client)
        {
            return this.credit.IsAvailable(client);
        }

    }
}
