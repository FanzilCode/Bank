using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bank
{
    class CreditBank
    {
        static List<Client> clients = new List<Client>();
        static List<Credit> credits = new List<Credit>();
        static List<Payment> payments = new List<Payment>();
        static List<ICreditType> creditTypes = new List<ICreditType>();

        // метод ReadOnFile - чтение из файла
        static void ReadOnFile(string path1, string path2)
        {
            using (StreamReader sr = new StreamReader(path1))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ICreditType creditType;
                    string[] arr = line.Trim().Split("%");

                    switch (arr[0])
                    {
                        case "Стандарт":
                            {
                                creditType = new Standard();
                                break;
                            }
                        case "Универсальный":
                            {
                                creditType = new Universal();
                                break;
                            }
                        default:
                            {
                                creditType = new Another(arr);
                                break;
                            }
                    }
                    if (!creditTypes.Contains(creditType))
                        creditTypes.Add(creditType);

                    Client client = new Client(sr.ReadLine().Trim().Split("%"));
                    if (!clients.Contains(client))
                        clients.Add(client);

                    arr = sr.ReadLine().Trim().Split("%");

                    double sum = Convert.ToDouble(arr[0]);
                    DateTime date = Convert.ToDateTime(arr[1]);

                    Credit credit = new Credit(creditType, client, sum, date);
                    if (!credits.Contains(credit))
                        credits.Add(credit);
                }
            }

            using (StreamReader sr = new StreamReader(path2))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ICreditType creditType;
                    string[] arr = line.Trim().Split("%");

                    switch (arr[0])
                    {
                        case "Стандарт":
                            {
                                creditType = new Standard();
                                break;
                            }
                        case "Универсальный":
                            {
                                creditType = new Universal();
                                break;
                            }
                        default:
                            {
                                creditType = new Another(arr);
                                break;
                            }
                    }
                    if (!creditTypes.Contains(creditType))
                        creditTypes.Add(creditType);

                    Client client = new Client(sr.ReadLine().Trim().Split("%"));
                    if (!clients.Contains(client))
                        clients.Add(client);

                    arr = sr.ReadLine().Trim().Split("%");

                    double sum = Convert.ToDouble(arr[0]);
                    DateTime date = Convert.ToDateTime(arr[1]);

                    Credit credit = new Credit(creditType, client, sum, date);
                    if (!credits.Contains(credit))
                        credits.Add(credit);

                    arr = sr.ReadLine().Trim().Split("%");
                    date = Convert.ToDateTime(arr[0]);
                    sum = Convert.ToDouble(arr[1]);

                    Payment payment = new Payment(credit, date, sum);
                    if (!payments.Contains(payment))
                        payments.Add(payment);
                }
            }
        }
        // метод SaveToFile - запись в файл
        static void SaveToFile(string path1, string path2)
        {
            string strings = "";
            foreach (var credit in credits)
            {
                strings += credit + "\n";
            }
            using (StreamWriter sw = new StreamWriter(path1))
            {
                sw.Write(strings.Trim());
            }

            strings = "";
            foreach (var payment in payments)
            {
                strings += payment + "\n";
            }
            using (StreamWriter sw = new StreamWriter(path2))
            {
                sw.Write(strings.Trim());
            }
        }

        // метод PrintClients - вывод на экран списка клиентов
        static void PrintClients()
        {
            int index = 0;
            foreach (var client in clients)
            {
                client.Print(index);
                index++;
            }
        }

        // метод PrintCreditTypes - вывод на экран списка видов кредитов
        static void PrintCreditTypes()
        {
            int index = 0;
            foreach (var creditType in creditTypes)
            {
                creditType.Print(index);
                index++;
            }
        }

        // метод PrintCredits() - вывод на экран списка отчётов по кредитам
        static void PrintCredits()
        {
            int index = 0;
            foreach (var credit in credits)
            {
                credit.Print(index);
                index++;
            }
        }

        // метод PrintCredits(Client client) - вывод кредитной истории клиента на экран
        static void PrintCredits(Client client)
        {
            int index = 0;
            foreach (var credit in credits)
            {
                if (credit.IsAvailable(client))
                {
                    credit.Print(index);
                    index++;
                }
            }
        }

        // метод PrintCredits(ICreditType creditType) - вывод списка кредитов по виду
        static void PrintCredits(ICreditType creditType)
        {
            int index = 0;
            foreach (var credit in credits)
            {
                if (credit.IsAvailable(creditType))
                {
                    credit.Print(index);
                    index++;
                }
            }
        }

        // метод PrintCredits(bool status) - вывод на экран списка погашенных/не погашенных кредитов
        static bool PrintCredits(bool status)
        {
            int index = 0;
            foreach (var credit in credits)
            {
                if (credit.IsRepaid(status))
                {
                    credit.Print(index);
                    index++;
                }
            }
            return index != 0;
        }

        // метод AddCredit() - добавить в список отчёт о кредите
        static void AddCredit()
        {
            Console.WriteLine("Выберите:\n1) Выбрать вид кредита из списка\n2) Добавить новый вид кредита");
            int choise = Convert.ToInt32(Console.ReadLine());
            ICreditType creditType;
            if (choise == 1)
            {
                PrintCreditTypes();
                Console.Write("Введите индекс вида кредита: ");
                creditType = creditTypes[Convert.ToInt32(Console.ReadLine())];
            }
            else
            {
                creditType = new Another();
                if (!creditTypes.Contains(creditType))
                    creditTypes.Add(creditType);
            }

            Console.WriteLine("Выберите:\n1) Выбрать клиента из списка\n2) Добавить нового клиента");
            choise = Convert.ToInt32(Console.ReadLine());
            Client client;
            if (choise == 1)
            {
                PrintClients();
                Console.Write("Введите индекс клиента: ");
                client = clients[Convert.ToInt32(Console.ReadLine())];
            }
            else
            {
                client = new Client();
                if (!clients.Contains(client))
                    clients.Add(client);
            }

            Console.Write("Сумма кредита: ");
            double sum = Convert.ToDouble(Console.ReadLine());

            Console.Write("Дата выдачи: ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            Credit credit = new Credit(creditType, client, sum, date);
            if (!credits.Contains(credit))
            {
                credits.Add(credit);
                Console.WriteLine("Отчёт о кредите добавлен в список.");
            }
            else
                Console.WriteLine("Отчёт о кредите уже в списке.");
        }

        // метод PrintPayments - вывод на экран списка погашений
        static void PrintPayments()
        {
            int index = 0;
            foreach (var payment in payments)
            {
                payment.Print(index);
                index++;
            }
        }

        // метод PrintPayments - вывод списка погашений клиента на экран
        static void PrintPayments(Client client)
        {
            int index = 0;
            foreach (var payment in payments)
            {
                if (payment.IsAvailable(client))
                    payment.Print(index);
            }
        }

        // метод AddPayment() - добавить отчёт о погашении
        static void AddPayment()
        {
            Console.WriteLine("Выберите кредит: ");
            PrintCredits(false);
            Console.Write("Введите индекс кредита: ");
            Credit credit = credits[Convert.ToInt32(Console.ReadLine())];
            Payment payment = new Payment(credit);
            payments.Add(payment);
            Console.WriteLine("Отчёт о погашении добавлен в список.");
        }

        static void Main(string[] args)
        {
            string path1 = @"Credits.txt";
            FileInfo fileInf = new FileInfo(path1);
            if (!fileInf.Exists)
                fileInf.Create();

            string path2 = @"Payments.txt";
            fileInf = new FileInfo(path2);
            if (!fileInf.Exists)
                fileInf.Create();
            ReadOnFile(path1, path2);

            int choise = 1;
            while(choise >= 1 && choise <= 4)
            {
                Console.Write("\t\t\t\tГлавная\n" +
                    "\t\tВыберите:\n" +
                    "1) Клиенты\n" +
                    "2) Виды кредитов\n" +
                    "3) Кредиты\n" +
                    "4) Погашения\n" +
                    "5) Выход\n" +
                    "Ваш выбор: ");
                choise = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch(choise)
                {
                    case 1:
                        {
                            Console.Write("\t\t\t\tКлиенты\n" +
                                "\t\tВыберите:\n" +
                                "1) Получить список клиентов\n" +
                                "2) Добавить клиента\n" +
                                "3) На главную\n" +
                                "Ваш выбор: ");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            if (choise2 == 1)
                                PrintClients();
                            else
                            {
                                if (choise == 2)
                                {
                                    Client client = new Client();
                                    if(!clients.Contains(client))
                                    {
                                        clients.Add(client);
                                        Console.WriteLine("Клиент добавлен в список.");
                                    }
                                    else
                                        Console.WriteLine("Клиент уже в списке.");
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.Write("\t\t\t\tВиды кредитов\n" +
                                "\t\tВыберите:\n" +
                                "1) Получить список видов кредитов\n" +
                                "2) Добавить новый вид кредита\n" +
                                "3) На главную\n" +
                                "Ваш выбор: ");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            if (choise2 == 1)
                                PrintCreditTypes();
                            else
                            {
                                if (choise == 2)
                                {
                                    CreditType creditType = new Another();
                                    if (!creditTypes.Contains(creditType))
                                    {
                                        creditTypes.Add(creditType);
                                        Console.WriteLine("Вид кредита добавлен в список.");
                                    }
                                    else
                                        Console.WriteLine("Вид кредита уже в списке.");
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.Write("\t\t\t\tКредиты\n" +
                                "\t\tВыберите:\n" +
                                "1) Получить список кредитов\n" +
                                "2) Получить список кредитов по виду\n" +
                                "3) Получить список погашенных кредитов\n" +
                                "4) Получить список не погашенных кредитов\n" +
                                "5) Получить кредитную историю клиента\n" +
                                "6) Добавить отчёт по кредиту\n" +
                                "7) На главную.\n" +
                                "Ваш выбор: ");
                            int choise2 = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();

                            switch(choise2)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("\t\tПолучить список кредитов");
                                        if (credits.Count > 0)
                                        {
                                            PrintCredits();
                                        }
                                        else
                                            Console.WriteLine("Список кредитов пуст.");
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("\t\tСписок кредитов по виду");
                                        Console.WriteLine("Выберите вид кредита(введите индекс): ");
                                        PrintCreditTypes();
                                        Console.Write("Введите индекс: ");
                                        PrintCredits(creditTypes[Convert.ToInt32(Console.ReadLine())]);
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("\t\tСписок погашенных кредитов");
                                        bool b = PrintCredits(true);
                                        if(!b)
                                            Console.WriteLine("Список пуст.");
                                        break;
                                    }
                                case 4:
                                    {
                                        Console.WriteLine("\t\tСписок не погашенных кредитов");
                                        bool b = PrintCredits(false);
                                        if (!b)
                                            Console.WriteLine("Список пуст.");
                                        break;
                                    }
                                case 5:
                                    {
                                        Console.WriteLine("\t\tКредитная история клиента");
                                        Console.WriteLine("Выберите клиента(введите индекс): ");
                                        PrintClients();
                                        Console.Write("Введите индекс: ");
                                        PrintCredits(clients[Convert.ToInt32(Console.ReadLine())]);
                                        break;
                                    }
                                case 6:
                                    {
                                        Console.WriteLine("\t\tДобавить отчёт по кредиту");
                                        AddCredit();
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.Write("\t\t\t\tПогашения\n" +
                                "\t\tВыберите:\n" +
                                "1) Получить список погашений\n" +
                                "2) Получить список погашений клиента\n" +
                                "3) Добавить отчёт о погашении\n" +
                                "4) На главную\n" +
                                "Ваш выбор: ");
                            int choise2 = Convert.ToInt32(Console.ReadLine());

                            switch(choise2)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("\t\tПолучить список погашений");
                                        if(payments.Count > 0)
                                        {
                                            PrintPayments();
                                        }
                                        else
                                            Console.WriteLine("Cписок погашений пуст.");
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("\t\tПолучить список погашений клиента\n" +
                                            "Выберите клиента(введите индекс): ");
                                        PrintClients();
                                        Console.Write("Введите индекс: ");
                                        PrintPayments(clients[Convert.ToInt32(Console.ReadLine())]);
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("\t\tДобавить отчёт о погашении");
                                        AddPayment();
                                        break;
                                    }
                                default:
                                    break;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }

            SaveToFile(path1, path2);
            Console.ReadKey();
        }
    }
}
