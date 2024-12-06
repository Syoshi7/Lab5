using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    internal class Category
    {
        private int C_id;
        private string C_category_name;
        private string C_age_limit;


        public Category(int id, string category_name, string age_limit)
        {
            C_id = id;
            C_category_name = category_name;
            C_age_limit = age_limit;
        }

        public int ID
        {
            get => C_id;
            set => C_id = value;
        }

        public string Name
        {
            get => C_category_name;
            set => C_category_name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string AgeLimit
        {
            get => C_age_limit;
            set => C_age_limit = value;
        }

        public override string ToString()
        {
            return $"ID категории: {C_id}, название категории: {C_category_name}, возрастные ограничения: {C_age_limit}";
        }
    }
}
