using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class ShopPlace
    {
        private string SP_shop_id;
        private string SP_shop_district;
        private string SP_shop_adress;

        public ShopPlace(string shop_id, string shop_district, string shop_adress)
        {
            SP_shop_id = shop_id;
            SP_shop_district = shop_district;
            SP_shop_adress = shop_adress;
        }

        public string ID
        {
            get => SP_shop_id;
            set => SP_shop_id = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string ShopDistrict
        {
            get => SP_shop_district;
            set => SP_shop_district = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string ShopAdress
        {
            get => SP_shop_adress;
            set => SP_shop_adress = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString()
        {
            return $"ID магазина: {SP_shop_id}, район: {SP_shop_district}, адрес: {SP_shop_adress}";
        }
    }
}
