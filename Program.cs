using System;
using System.Collections.Generic;
using System.Linq;

namespace linq
{
    public class Product
    {
        public int ID {set; get;}
        public string Name {set; get;}         // tên
        public double Price {set; get;}        // giá
        public string[] Colors {set; get;}     // các màu sắc
        public int Brand {set; get;}           // ID Nhãn hiệu, hãng
        public Product(int id, string name, double price, string[] colors, int brand)
        {
            ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
        }
        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        override public string ToString()
        => $"{ID,3} {Name, 12} {Price, 5} {Brand, 2} {string.Join(",", Colors)}";

    }
    public class  Brand {
        public string Name {set; get;}
        public int ID {set; get;}
    }
    class Program
    {
        static void Main(string[] args)
        {
            var brands = new List<Brand>() {
                new Brand{ID = 1, Name = "Công ty AAA"},
                new Brand{ID = 2, Name = "Công ty BBB"},
                new Brand{ID = 4, Name = "Công ty CCC"},
            };

            var products = new List<Product>()
            {
                new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
                new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
                new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
                new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
                new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
                new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
                new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
            };
            var rq = from product in products 
                    where product.ID > 3
                    select product;

            var rq1 = from product in products
            select new              {
                namene = product.Name.ToUpper()
            };
           
            // nhieu from
            var q = from product in products
                from color in product.Colors
                where color == "Trắng"
                select product;
            
            // orderby
            var q1 = from product in products
            where product.Price < 500
            orderby product.Price
            select product;
            //  foreach (var product in q1){
            //     Console.WriteLine(product);
            // }
            // group ..........by .........
            /*
            nhom cac doi tuong co chung 1 gia tri nao do vao 1 nhom
            */
            var q2= from product in products
            where product.Price >= 400 && product.Price <= 500
            orderby product.Price descending
            group product by product.Price;
            /*
            Nhom tat ca cac product theo tung gia tri price
            */ 
            // foreach(var group in q2){
            //     foreach(var product in group){
            //         Console.WriteLine(product);
            //     }
            // }
            // let dung de luu 1 ke qua nao do trong truy van
            var q3 = from product in products
            group product by product.Price into gr
            let count = gr.Count()
            select new {
                price = gr.Key,
                numberProduct = count
            };
            /*
            Câu truy vấn LINQ thường bắt đầu bằng mệnh đề from và kết thúc bằng mệnh 
            đề select hoặc group, giữa chúng là những mệnh đề where, orderby, join, let
            */
            // foreach (var item in q3)
            // {
            //     Console.WriteLine($"{item.price} - {item.numberProduct}");
            // } 
            var q4 = from product in products
            join brand in brands
            on product.Brand equals brand.ID
            select new {
                name = product.Name,
                brandName = ( brand != null) ? brand.Name:" No brand"
            } ;
            // foreach(var x in q4){
            //     Console.WriteLine(x.name+ " "+ x.brandName);
            // }
            var q5 = from product in products
                join brand in brands on product.Brand equals brand.ID into t
                from brand in t.DefaultIfEmpty()
                select new {
                    name = product.Name,
                    brand = (brand == null) ? "NO-BRAND" : brand.Name
                };
            foreach(var p in q5){
                Console.WriteLine(p.name + " "+ p.brand);
            }
            // Select
            /*
            where
            selectmany:noi nhieu mang thanh 1 mang duy nhat
            min,max,sum,average
            Join
            Take
            Skip
            order by
            distinct: loai bo cac phan tu trung lap
            single singleOrDefault: kiem tra phan tu thoa man dieu kien logic duy nhat. Neu co nhieu hon 1 phan tu thoa man thi loi
            Any: true/false
            All: true/false
            Count: co su dung dieu kien trong  ham duoc
            */
            var q6 = from product in products
            select product;
            
        }
    }
}
