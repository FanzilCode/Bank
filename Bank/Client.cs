using System;

namespace Bank
{
    class Client
    {
        // название
        string title;
        // вид собственности
        string property;
        // адрес
        string address;
        // телефон
        string phone;
        // контактное лицо
        string person;

        // св-во для поля title
        public string Title
        {
            get
            {
                return title;
            }
        }
        // конструктор с 5-ю параметрами
        public Client(string title, string property, string address, string phone, string person)
        {
            this.title = title;
            this.property = property;
            this.address = address;
            this.phone = phone;
            this.person = person;
        }
        // конструктор без параметров
        public Client()
        {
            Console.Write("Название: ");
            title = Console.ReadLine();

            Console.Write("\nВид собственнности: ");
            property = Console.ReadLine();

            Console.Write("\nАдрес: ");
            address = Console.ReadLine();

            Console.Write("\nТелефон: ");
            phone = Console.ReadLine();

            Console.Write("\nКонтактное лицо: ");
            person = Console.ReadLine();
        }
        // конструктор для чтения из файла
        public Client(string[] arr)
        {
            title = arr[0];
            property = arr[1];
            address = arr[2];
            phone = arr[3];
            person = arr[4];
        }

        // переопредениие метода ToString() для записи в файл
        public override string ToString()
        {
            return $"{title}%{property}%{address}%{phone}%{person}";
        }
        // метод Print() для выведения на экран информации о клиенте
        public void Print(int index)
        {
            Console.WriteLine($"Индекс: {index}\n" +
                $"Название: {title}\n" +
                $"Вид собственности: {property}\n" +
                $"Адрес: {address}\n" +
                $"Телефон: {phone}\n" +
                $"Контактное лицо: {person}");
        }
        // операторы равенства
        public static bool operator ==(Client c1, Client c2)
        {
            return (c1.title == c2.title && c1.property == c2.property && c1.address == c2.address && c1.phone == c2.phone && c1.person == c2.person);
        }
        public static bool operator !=(Client c1, Client c2)
        {
            return !(c1 == c2);
        }
        // переопределение метода Equals()
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            Client client = (Client)obj;
            return this == client;
        }
        // переопределение метода GetHashCode()
        public override int GetHashCode()
        {
            return title.GetHashCode() + property.GetHashCode() + address.GetHashCode() + phone.GetHashCode() + phone.GetHashCode() + person.GetHashCode();
        }
    }
}
