using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Створіть клас «Магазин», який має зберігати таку інформацію:
// назва магазину;
// адреса магазину;
// тип магазину: продовольчий, господарський, одяг, взуття.
//Реалізуйте у класі методи та властивості, необхідні для функціонування класу.
//Клас має реалізовувати інтерфейс IDisposable. Напишіть код для тестування функціональності класу.
//Напишіть код для виклику методу Dispose.


namespace ClassShop
{
    public enum ShopType
    {
        Food,
        Household,
        Clothing,
        Shoes
    }

    class Shop : IDisposable
    {
        private bool disposedValue;

        public string Name { get; set; }
        public ShopType Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public Shop(string name, ShopType description, string address, string city)
        {
            Name = name;
            Description = description;
            Address = address;
            City = city;
        }

        public void PrintInfo()
        {
            Console.WriteLine("Название магазина: " + Name);
            Console.WriteLine("Адрес магазина: " + Address);
            Console.WriteLine("Тип магазина: " + Description.ToString());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue) // проверка того, что объект еще не был "очищен"
            {
                if (disposing)
                {
                    Console.WriteLine("dispose освобождение управляемых ресурсов  объекта: " + Name);
                }

                Console.WriteLine("dispose освобождение НЕуправляемых ресурсов  объекта: " + Name);
                disposedValue = true; // флаг - признак "очистки" объекта
            }
            else Console.WriteLine("dispose уже вызывался ранее для этого объекта: " + Name);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            Console.WriteLine("блокирование вызова финализатора для объекта: " + Name);
            GC.SuppressFinalize(this);
        }

        ~Shop()
        {
            Console.WriteLine("Finalizator for object: " + Name);
            Dispose(disposing: false);
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            //с использованием косвенного/прямого вызова  dispose
            using (var shop = new Shop("Класс", ShopType.Food, "Тракторостроителей, 10", "Харьков"))
            {
                shop.PrintInfo();
                Console.WriteLine("Памяти в куче занято (в байтах): {0}", GC.GetTotalMemory(false));
                Console.WriteLine("Имеется поколений: {0}", GC.MaxGeneration + 1);
                Console.WriteLine($"Объект ({shop.Name}) в поколении : {GC.GetGeneration(shop)}");
                shop.Dispose(); // прямой вызов dispose
            }
            GC.Collect();
            Console.WriteLine("____________________________________________________________________________________");
            Console.WriteLine("Памяти в куче занято (в байтах): {0}", GC.GetTotalMemory(false));
        }
    }
}
