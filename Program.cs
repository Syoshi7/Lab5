using System.Text;

namespace Lab5

{
    class Program
    {
        static void Main()
        {

            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            const string path = "C:\\Users\\Syoshi\\source\\repos\\Lab5\\bin\\Debug\\net8.0\\LR5-var1.xls";
            string[] sheetNames = DataBaseInstrument.GetSheetNames(path);
            int userInput;
            string userChoice;
            int table_number;

            

            do
            {
                Console.WriteLine("Создать новый файл для записывания истории действий или записывать в существующий?");
                Console.WriteLine("0 - Создать новый, 1 - Записывать в существующий");
                userInput = DataBaseInstrument.IntEnter();
            } while ((userInput != 0) && (userInput != 1));

            bool append = userInput == 1;
            var logger = new Logger("log.txt", append);
            logger.Info("Программа была запущена в " + DateTime.Now);

            var databaseinstrument = new DataBaseInstrument(logger);
            databaseinstrument.LoadExcelData(path);

            do
            {
                Console.WriteLine("\nЛабораторная работа по работе с Excel файлами." +
                    "\nРабота с файлом " + path + "." +
                    "\nТаблицы в файле: " +
                    string.Join(", ", sheetNames) +
                    "\n\nКоманды:" +
                    "\n1. Вывод содержимого базы данных." +
                    "\n2. Удаление строки из базы данных." +
                    "\n3. Изменение строки из базы данных." +
                    "\n4. Добавление строки в базу данных." +
                    "\n5. Запрос 1." +
                    "\n6. Запрос 2." +
                    "\n7. Запрос 3." +
                    "\n8. Запрос 4." +
                    "\n0. Выход из программы.\n");

                do
                {
                    Console.WriteLine("Выберите номер исполняемой команды: ");
                    userChoice = Console.ReadLine();
                } while (int.TryParse(userChoice, out int number2) && number2 < 0 && number2 > 8);

                switch (userChoice)
                {
                    case "1":
                        databaseinstrument.PrintData();
                        break;

                    case "2":
                        Console.WriteLine("Введите номер таблицы в которой будем удалять строку (0 - Движение товаров, 1 - Товар, 2 - Категория, 3 - Магазин): ");
                        table_number = DataBaseInstrument.IntEnter();

                        while (table_number < 0 || table_number > 3)
                        {
                            Console.WriteLine("Неверный номер таблицы. Повторите ввод(0 - Движение товаров, 1 - Товар, 2 - Категория, 3 - Магазин).");
                            table_number = DataBaseInstrument.IntEnter();
                        }

                        Console.WriteLine("Введите ID удаляемой строки: ");
                        string idInput = Console.ReadLine();
                        int id;

                        while (!int.TryParse(idInput, out id) || id <= 0)
                        {
                            Console.WriteLine("Некорректный ввод ID. Повторите ввод: ");
                            idInput = Console.ReadLine();
                        }

                        databaseinstrument.DeleteRowByID(path, table_number, id);
                        break;

                    case "3":
                        try
                        {
                            Console.WriteLine("Введите номер таблицы в которой будем менять значение(0 - Движение товаров, 1 - Товар, 2 - Категория, 3 - Магазин): ");
                            table_number = DataBaseInstrument.IntEnter();

                            while (table_number < 0 || table_number > 3)
                            {
                                Console.WriteLine("Неверный номер таблицы. Пожалуйста, введите корректный номер таблицы(0 - Движение товаров, 1 - Товар, 2 - Категория, 3 - Магазин): ");
                                table_number = DataBaseInstrument.IntEnter();
                            }

                            Console.WriteLine("Введите ID строки, которую хотите обновить: ");
                            idInput = Console.ReadLine();

                            while (!int.TryParse(idInput, out id) || id <= 0)
                            {
                                Console.WriteLine("Неверный ID. Пожалуйста, введите положительное целое число: ");
                                idInput = Console.ReadLine();
                            }

                            var columns = DataBaseInstrument.GetColumnCount(path, table_number);
                            List<string> data = new List<string>();
                            data.Add(idInput);
                            Console.WriteLine("Введите новые данные для строки: ");
                            for (int i = 1; i < columns; i++)
                            {
                                Console.WriteLine($"Введите значение для столбца {i + 1}: ");
                                string input = Console.ReadLine();
                                data.Add(input);
                            }
                            databaseinstrument.UpdateRowByID(path, table_number, id, data);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Ошибка при обновлении строки: {e.Message}");
                            logger.Error($"Ошибка при обновлении строки: {e.Message}");
                        }
                        break;

                    case "4":
                        try
                        {
                            Console.WriteLine("Введите номер таблицы в которую будем добавлять строку(0 - Движение товаров, 1 - Товар, 2 - Категория, 3 - Магазин): ");
                            table_number = DataBaseInstrument.IntEnter();

                            while (table_number < 0 || table_number > 3)
                            {
                                Console.WriteLine("Неверный номер таблицы. Повторите ввод(0 - Движение товаров, 1 - Товар, 2 - Категория, 3 - Магазин).");
                                table_number = DataBaseInstrument.IntEnter();
                            }

                            var columns = DataBaseInstrument.GetColumnCount(path, table_number);
                            List<string> data = new List<string>();

                            Console.WriteLine("Введите новые данные для строки: ");
                            for (int i = 1; i < columns; i++)
                            {
                                Console.WriteLine($"Введите значение для столбца {i + 1}: ");
                                string input = Console.ReadLine();
                                data.Add(input);
                            }
                            databaseinstrument.AddRow(path, table_number, data);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Ошибка при добавлении строки: {e.Message}");
                            logger.Error($"Ошибка при добавлении строки: {e.Message}");
                        }
                        break;

                    case "5":
                        logger.Info("Запуск запроса 1.");
                        Console.WriteLine("Сколько товаров с скидкой больше 20%?");
                        var products = databaseinstrument.Query1();
                        Console.WriteLine("Ответ: " + products + "\n");
                        break;

                    case "6":
                        logger.Info("Запуск запроса 2.");
                        Console.WriteLine("Сколько было возвратов товаров, продаваемых по цене больше 5000р.?");
                        Console.WriteLine("Введите сумму: ");
                        var returns = databaseinstrument.Query2();
                        Console.WriteLine("Ответ: " +  returns + "\n");
                        break;

                    case "7":
                        logger.Info("Запуск запроса 3.");
                        Console.WriteLine("Вывести названия игрушек категорий 12+ и которые были куплены в количестве <10 штук.");
                        databaseinstrument.Query3();
                        break;

                    case "8":
                        logger.Info("Запуск запроса 4.");
                        Console.WriteLine("Вывести перечень товаров проданных по карте клиента со скидкой 5% категории 0+.");
                        databaseinstrument.Query4();
                        break;

                    case "0":
                        Console.WriteLine("Завершение программы.");
                        break;
                }
            } while (userChoice != "0");
        }
    }
}
