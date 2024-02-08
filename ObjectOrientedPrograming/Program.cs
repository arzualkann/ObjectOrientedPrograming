using static ObjectOrientedPrograming.Program;

namespace ObjectOrientedPrograming
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductService productService = new ProductManager(new FakeBankService());
            productService.Sell(new Product { Id = 1,Name="Laptop",Price=1000 },
                new Customer { Id=1,Name="Engin",IsStudent=true,IsOfficer=false});
        }
              /**
          * ///Senaryo
          * /Gereksinim
          * Kullanıcı bir ürün satın almak istiyor.
          * Ürünün fiyatı öğrencilere %10 indirimli olarak yansıyacaktır.
          * Default olarak TL ödeme yapacaktır.
          * Ürün fiiyatını isterse Dolar veya Euro ödeyebilecektir.
          * Döviz karşılığı merkez bankası servisinden alınacaktır.
          * //Data:
          * Ürün: Televizyon/1000TL
          * Normal bir müşteri/TL
          * ÇIKTI:
          * Engin Demiroğ ürünü satın aldı.1000 TL ödeme alındı
          * //Data:
          * Ürün:Televizyon/1000TL
          * Öğrenci/TL
          * ÇIKTI:
          * Tahir Çalışkan ürünü satın aldı.900 TL ödeme alındı
          * //Data:
          * Ürün:Televizyon/1000tl
          * Öğrenci/Dolar
          * ÇIKTI:
          * Engin Demiroğ ürünü satın aldı.170 Dolar ödeme alındı
          * **/
        class Customer : IEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsStudent { get; set; }
            public bool IsOfficer { get; set; }

        }
        interface IEntity
        {
        }
        class Product : IEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }

        }
        class ProductManager : IProductService
        {
           private IBankService _bankService;
           public ProductManager(IBankService bankService)
           {
               _bankService = bankService;
           }
           public void Sell(Product product, Customer customer)
           {
                decimal price=product.Price;
                if (customer.IsStudent)
                {
                    price=product.Price * (decimal)0.90;
                }
                if (customer.IsOfficer)
                {
                    price=product.Price * (decimal)0.80;

                }
                price=_bankService.ConvertRate(new CurrencyRate { Currency = 1, Price = 1000 });
                Console.WriteLine(price);
                Console.ReadLine();
            }
            
        }
        interface IProductService
        {
            void Sell(Product product, Customer customer);
        }


        class FakeBankService :IBankService
        {
            public decimal ConvertRate(CurrencyRate currencyRate) 
            {

                return currencyRate.Price /(decimal) 5.30;
            }
        }
        }
    interface IBankService
    {
        decimal ConvertRate(CurrencyRate currencyRate);
    }
    class CurrencyRate
        {
          public decimal Price { get; set; }
          public int Currency { get; set; }
        }
        class CentralBankServiceAdapter:IBankService
        {
            public decimal ConvertRate(CurrencyRate currencyRate)
            {
                CentralBankServise centralBankServise = new CentralBankServise();
                return centralBankServise.ConvertRate(currencyRate);
            }
        }
        //Merkez bankası
        class CentralBankServise
        {
            public decimal ConvertRate(CurrencyRate currencyRate) 
            {
            return currencyRate.Price/(decimal)5.25;
            }
        }
}


