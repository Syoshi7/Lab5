using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab5
{
    public class DataBaseInstrument
    {

        private List<ProductMovement> productMovements;
        private List<Product> DB_products;
        private List<Category> DB_categories;
        private List<ShopPlace> DB_shops;
        private Logger DB_logger;


        public DataBaseInstrument(Logger logger)
        {
            productMovements = new List<ProductMovement>();
            DB_products = new List<Product>();
            DB_categories = new List<Category>();
            DB_shops = new List<ShopPlace>();
            DB_logger = logger;
        }

        public void LoadExcelData(string path)
        {
            try
            {
                DB_logger.Info("Загрузка данных Exsel: " + path);

                Worksheet productMovementSheet = new Workbook(path).Worksheets[0];

                productMovements = productMovementSheet.Cells.Rows.Cast<Row>()
                .Skip(1)
                .Select(row => new ProductMovement(
                        int.TryParse(row.GetCellOrNull(0)?.Value?.ToString(), out var id) ? id : 0,
                        DateTime.TryParse(row.GetCellOrNull(1)?.Value?.ToString(), out var date) ? date : DateTime.MinValue,
                        row.GetCellOrNull(2)?.Value?.ToString() ?? "0",
                        int.TryParse(row.GetCellOrNull(3)?.Value?.ToString(), out var quantity) ? quantity : 0,
                        row.GetCellOrNull(4)?.Value?.ToString() ?? "0",
                        int.TryParse(row.GetCellOrNull(5)?.Value?.ToString(), out var anotherQuantity) ? anotherQuantity : 0,
                        row.GetCellOrNull(6)?.Value?.ToString() ?? "0"))
                .ToList();
                DB_logger.Info("Загружена информация о " + productMovements.Count + "движениях товаров.");


                Worksheet ProductSheet = new Workbook(path).Worksheets[1];
                DB_products = ProductSheet.Cells.Rows.Cast<Row>()
                    .Skip(1)
                    .Select(row => new Product(
                        int.Parse(row.GetCellOrNull(0).Value.ToString()),
                        int.Parse(row.GetCellOrNull(1).Value.ToString()),
                        row.GetCellOrNull(2).Value.ToString(),
                        decimal.Parse(row.GetCellOrNull(3).Value.ToString()),
                        decimal.Parse(row.GetCellOrNull(4).Value.ToString()),
                        double.Parse(row.GetCellOrNull(5).Value.ToString())))
                        .ToList();
                DB_logger.Info("Загружена информация о " + DB_products.Count + "продуктах.");


                Worksheet CategorySheet = new Workbook(path).Worksheets[2];
                DB_categories = CategorySheet.Cells.Rows.Cast<Row>()
                    .Skip(1)
                    .Select(row => new Category(
                        int.Parse(row.GetCellOrNull(0).Value.ToString()),
                        row.GetCellOrNull(1).Value.ToString(),
                        row.GetCellOrNull(2).Value.ToString()))
                    .ToList();


                Worksheet ShopPlaceSheet = new Workbook(path).Worksheets[3];
                DB_shops = ShopPlaceSheet.Cells.Rows.Cast<Row>()
                    .Skip(1)
                    .Select(row => new ShopPlace(
                        row.GetCellOrNull(0).Value.ToString(),
                        row.GetCellOrNull(1).Value.ToString(),
                        row.GetCellOrNull(2).Value.ToString()))
                    .ToList();
                DB_logger.Info("Загружена информация о " + DB_shops.Count + "магазине/магазинах.");




                DB_logger.Info("Загружена информация о " + DB_categories.Count + "категориях/категории.");



        
                DB_logger.Info("Данные EXCEL файла загружены.");
            }
            catch (Exception ex)
            {
                DB_logger.Error("Ошибка при загрузке данных Excel файла: \n" + ex.Message);
                Console.WriteLine("Ошибка при загрузке данных Excel файла: " + ex.Message);
            }
        }

        public void PrintData()
        {
            DB_logger.Info("Вывод данных в консоль.");

            Console.WriteLine("////////////////////////////");
            Console.WriteLine("Движение продуктов:");
            DB_logger.Info("Вывод движения продуктов в консоль.");
            productMovements.ForEach(productMovement => Console.WriteLine(productMovement.ToString()));

            Console.WriteLine("////////////////////////////");
            Console.WriteLine("Продукты:");
            DB_logger.Info("Вывод продуктов в консоль.");
            DB_products.ForEach(product => Console.WriteLine(product.ToString()));

            Console.WriteLine("////////////////////////////");
            Console.WriteLine("Категории:");
            DB_logger.Info("Вывод категорий в консоль.");
            DB_categories.ForEach(category => Console.WriteLine(category.ToString()));


            Console.WriteLine("////////////////////////////");
            Console.WriteLine("Магазины:");
            DB_logger.Info("Вывод магазинов в консоль");
            DB_shops.ForEach(shop => Console.WriteLine(shop.ToString()));
        }

        public void DeleteRowByID(string path, int table_number, int dataId)
        {
            try
            {
                DB_logger.Info("Удаление строки с ID " + dataId + " из таблицы ");
                Console.WriteLine("Удаление строки с ID " + dataId + " из таблицы ");

                switch (table_number)
                {
                    case 0:
                        var productmovementToDelete = productMovements.FirstOrDefault(pm => pm.IdOperation == dataId);
                        if (productmovementToDelete != null)
                        {
                            productMovements.Remove(productmovementToDelete);
                            DB_logger.Info("Движение товаров с ID " + dataId + " удалено.");
                            Console.WriteLine("Движение товаров с ID " + dataId + " удалено.");
                        }
                        else
                        {
                            DB_logger.Info("Движение товаров с ID " + dataId + " не найдено.");
                            Console.WriteLine("Движение товаров с ID " + dataId + " не найдено.");
                        }
                        break;

                    case 1:
                        var productToDelete = DB_products.FirstOrDefault(p => p.Article == dataId);
                        if (productToDelete != null)
                        {
                            DB_products.Remove(productToDelete);
                            DB_logger.Info("Товар с ID " + dataId + " удален.");
                            Console.WriteLine("Товар с ID " + dataId + " удален.");
                        }
                        else
                        {
                            DB_logger.Info("Товар с ID " + dataId + " не найден.");
                            Console.WriteLine("Товар с ID " + dataId + " не найден.");
                        }
                        break;

                    case 2:
                        {
                            var categoryToDelete = DB_categories.FirstOrDefault(c => c.ID == dataId);
                            if (categoryToDelete != null)
                            {
                                DB_categories.Remove(categoryToDelete);
                                DB_logger.Info("Категория с ID " + dataId + " удалена.");
                                Console.WriteLine("Категория с ID " + dataId + " удалена.");
                            }
                            else
                            {
                                DB_logger.Info("Категория с ID " + dataId + " не найдена.");
                                Console.WriteLine("Категория с ID " + dataId + " не найдена.");
                            }
                            break;
                        }
                    case 3:
                        {
                            string PID = "Р" + dataId.ToString();
                            var shopToDelete = DB_shops.FirstOrDefault(s => s.ID == PID);
                            if (shopToDelete != null)
                            {
                                DB_shops.Remove(shopToDelete);
                                DB_logger.Info("Магазин с ID " + PID + " удалён.");
                                Console.WriteLine("Магазин с ID " + PID + " удалён.");
                            }
                            else
                            {
                                DB_logger.Info("Магазин с ID " + PID + " не найден.");
                                Console.WriteLine("Магазин с ID " + PID + " не найден.");
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                DB_logger.Error("Ошибка при удалении строки с ID " + dataId + " из таблицы: " + ex.Message);
                Console.WriteLine("Ошибка при удалении строки с ID " + dataId + " из таблицы: " + ex.Message);
            }
        }


        public static string[] GetSheetNames(string path)
        {
            Workbook workbook = new Workbook(path);
            int sheetCount = workbook.Worksheets.Count;
            string[] sheetNames = new string[sheetCount];

            for (int i = 0; i < 4; i++)
            {
                sheetNames[i] = workbook.Worksheets[i].Name;
            }

            return sheetNames;
        }


        public static int GetColumnCount(string path, int table_number)
        {
            Workbook workbook = new Workbook(path);
            Worksheet worksheet = workbook.Worksheets[workbook.Worksheets[table_number].Name];
            return worksheet.Cells.MaxDataColumn + 1;
        }

        
        public void UpdateRowByID(string path, int table_number, int dataId, List<string> data)
        {
            try
            {
                DB_logger.Info("Обновление строки c ID" + dataId + "в таблице ");
                Console.WriteLine("Обновление строки c ID" + dataId + "в таблице ");

                using (var workbook = new Workbook(path))
                {
                    var worksheet = workbook.Worksheets[workbook.Worksheets[table_number].Name];
                    var rowToUpdate = worksheet.Cells.Rows.Cast<Row>().Skip(1).FirstOrDefault(row => row.GetCellOrNull(0)?.IntValue == dataId);

                    if (rowToUpdate != null)
                    {
                        for (int i = 0; i < data.Count; i ++)
                            rowToUpdate.GetCellOrNull(i).PutValue(data[i]);
                    }
                    else
                    {
                        DB_logger.Warning("Строка с ID " + dataId + " не найдена в таблице ");
                        Console.WriteLine("Строка с ID " + dataId + " не найдена в таблице ");
                    }

                    DB_logger.Info("Строка с ID " + dataId + " изменена в таблице " );
                    Console.WriteLine("Строка с ID " + dataId + " изменена в таблице ");
                    workbook.Save(path);
                    LoadExcelData(path);
                }
            }
            catch (Exception ex)
            {
                DB_logger.Error("Ошибка при изменении строки с ID " + dataId + " в таблицe: " + ex.Message);
                Console.WriteLine("Ошибка при изменении строки с ID " + dataId + " в таблице: " + ex.Message);
            }
        }

        public void AddRow(string path, int table_number, List<string> data)
        {
            try
            {
                DB_logger.Info("Добавление новой строки в таблицу ");
                Console.WriteLine("Добавление новой строки в таблицу ");

                using (var workbook = new Workbook(path))
                {
                    var worksheet = workbook.Worksheets[workbook.Worksheets[table_number].Name];
                    var rows = worksheet.Cells.Rows.Cast<Row>().Skip(1).ToList();

                    int newRowID = worksheet.Cells.MaxDataRow + 1;
                    data.Insert(0, newRowID.ToString());

                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[newRowID, i].PutValue(data[i]);
                    }

                    DB_logger.Info("Новая строка с ID " + data[0] + " успешно добавлена в таблицу " );
                    Console.WriteLine("Новая строка с ID " + data[0] + " успешно добавлена в таблицу ");
                    workbook.Save(path);
                    LoadExcelData(path);
                }
            }
            catch (Exception ex)
            {
                DB_logger.Error("Ошибка при добавлении новой строки в таблицу: " + ex.Message);
                Console.WriteLine("Ошибка при добавлении новой строки в таблицу: " + ex.Message);
            }
        }

        public int Query1()
        {
            DB_logger.Info("Сколько продуктов со скидкой более 20%?");

            var cheapProductsCount = DB_products.Count(Product => Product.CardDiscount > 0.2);
            DB_logger.Info("Ответ: " + cheapProductsCount);

            return cheapProductsCount;
        }

        public int Query2()
        {
            // как много было возвратов товаров, проданных по цене больше 10000р?

            DB_logger.Info("Сколько было возвратов товаров, продаваемых по цене больше 5000р?");

            var returnHighPriceCount = productMovements
                .Count(productMovements => DB_products
                .Any(product => product.Article == productMovements.Article && product.Sellprice > 5000)
                && productMovements.OperationType == "Возврат");

            DB_logger.Info("Ответ: " +  returnHighPriceCount);

            return returnHighPriceCount;
        }

        public void Query3()
        {
            // вывести названия игрушек категорий 12+ и которые были куплены 1 августа 2024 года.

            DB_logger.Info("Вывести названия игрушек категорий 12+ и которые были куплены в количестве 10 или меньше штук.");

            var result = from product in DB_products
                         join category in DB_categories on product.CategoryId equals category.ID
                         join productmovement in productMovements on product.CategoryId equals productmovement.IdOperation
                         where (category.AgeLimit == "12+" && productmovement.ItemCount <= 10)
                         select new
                         {
                             ProductArticle = product.Article,
                             ProductName = product.ProductName,
                             CategoryAgeLimit = category.AgeLimit,
                             Count = productmovement.ItemCount,
                         };
            foreach (var product in result)
            {
                Console.WriteLine(
                    $"ProductName: {product.ProductName}, ProductArticle: {product.ProductArticle}, CategoryAgeLimit: {product.CategoryAgeLimit}, Count: {product.Count}");
                DB_logger.Info(
                    $"ProductName: {product.ProductName}, ProductArticle: {product.ProductArticle}, CategoryAgeLimit: {product.CategoryAgeLimit}, Count: {product.Count}");
            }
        }

        public void Query4()
        {
            // Вывести перечень товаров проданных по карте клиента со скидкой 15/20% категории 0+.

            DB_logger.Info("Вывести перечень товаров проданных по карте клиента со скидкой 5% категории 0+.");

            var result = from product in DB_products
                         join category in DB_categories on product.CategoryId equals category.ID
                         join productmovement in productMovements on product.CategoryId equals productmovement.IdOperation
                         where (productmovement.OperationType == "Продажа" && productmovement.ClientCardUsage == "Да" && (product.CardDiscount == 0.05 || product.CardDiscount == 0.3) && category.AgeLimit == "0+")
                         select new
                         {
                             Name = product.ProductName,
                             DiscountUse = productmovement.ClientCardUsage,
                             DiscountSize = product.CardDiscount,
                             ProductAgeLimit = category.AgeLimit
                         };
            foreach (var product in result)
            {
                Console.WriteLine(
                    $"Name: {product.Name}, По скидке?: {product.DiscountUse}, Размер скидки: {product.DiscountSize}, Ограничение по возрасту: {product.ProductAgeLimit}");
                DB_logger.Info(
                    $"Name: {product.Name}, По скидке?: {product.DiscountUse}, Размер скидки: {product.DiscountSize}, Ограничение по возрасту: {product.ProductAgeLimit}");
            }
        }

        public static int IntEnter()                         // Метод для проверки числа на правильность ввода.
        {
            int table_number;
            while (true)
            {
                Console.WriteLine("Введите целое число: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out table_number))
                    return table_number;
                else
                    Console.WriteLine("Ошибка ввода.");
            }
        }
    }
}
