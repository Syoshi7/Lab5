using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class Product
    {
        private int P_article;
        private int P_category_id;
        private string P_product_name;
        private decimal P_buy_price;
        private decimal P_sell_price;
        private double P_card_discount;

        public Product(int article, int category_id, string product_name, decimal buy_price, decimal sell_price, double card_discount)
        {
            P_article = article;
            P_category_id = category_id;
            P_product_name = product_name;
            P_buy_price = buy_price;
            P_sell_price = sell_price;
            P_card_discount = card_discount;
        }

        public int Article
        {
            get => P_article;
            set => P_article = value;
        }

        public int CategoryId
        {
            get => P_category_id;
            set => P_category_id = value; 
        }

        public string ProductName
        {
            get => P_product_name;
            set => P_product_name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public decimal BuyPrice
        {
            get => P_buy_price;
            set => P_buy_price = value;
        }

        public decimal Sellprice
        {
            get => P_buy_price;
            set => P_sell_price = value;
        }

        public double CardDiscount
        {
            get => P_card_discount;
            set => P_card_discount = value;
        }

        public override string ToString()
        {
            return $"Артикул: {P_article}, ID Категории: {P_category_id}, название продукта: {P_product_name}, цена закупки: {P_buy_price} р., цена продажи: {P_sell_price} р., скидка по карте: {P_card_discount} %.";
        }
    }
}
