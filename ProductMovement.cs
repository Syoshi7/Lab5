using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class ProductMovement
    {
        private int PM_id_operation; 
        private DateTime PM_date_operation;
        private string PM_id_shop;  // P + Int
        private int PM_article;
        private string PM_operation_type;
        private int PM_item_count;
        private string PM_client_card_usage;

        public ProductMovement(int id_operation, DateTime date_operation, string id_shop, int article, string operation_type, int item_count, string client_card_usage)
        {
            PM_id_operation = id_operation;
            PM_date_operation = date_operation;
            PM_id_shop = id_shop;
            PM_article = article;
            PM_operation_type = operation_type;
            PM_item_count = item_count;
            PM_client_card_usage = client_card_usage;
        }

        public int IdOperation
        {
            get => PM_id_operation;
            set => PM_id_operation = value;
        }

        public DateTime DateOperation
        {
            get => PM_date_operation;
            set => PM_date_operation = value;
        }

        public string Shop
        {
            get => PM_id_shop;
            set => PM_id_shop = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int Article
        {
            get => PM_article; 
            set => PM_article = value;
        }

        public string OperationType
        {
            get => PM_operation_type;
            set => PM_operation_type = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int ItemCount
        {
            get => PM_item_count; 
            set => PM_item_count = value;
        }

        public string ClientCardUsage
        {
            get => PM_client_card_usage;
            set => PM_client_card_usage = value;// ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString()
        {
            return $"ID операции: {PM_id_operation}, дата: {PM_date_operation:dd-MM-yyyy}, магазин: {PM_id_shop}, артикул: {PM_article}, тип операции: {PM_operation_type}" +
                $" \n количество упаковок товара: {PM_item_count}, была ли при оплате использована клиентская карта: {PM_client_card_usage}";
        }
    }
}
