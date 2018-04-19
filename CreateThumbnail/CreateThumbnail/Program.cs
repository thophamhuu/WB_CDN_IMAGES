using CreateThumbnail.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateThumbnail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Configs.Initialize();

            int key = -1;
            Start:
            Menu();
            Int32.TryParse(Console.ReadLine().ToString(), out key);
            while (key != 0)
            {
                ICreateThumbnail _thumbnail = null;
                switch (key)
                {
                    case 1:
                        _thumbnail = new ProductThumbnail();
                        break;
                    case 2: _thumbnail = new CategoryThumbnail(); break;
                    case 3: _thumbnail = new OtherThumbnail(); break;
                    case 4:Configs.UpdateSettings(); break;
                    case 5: Configs.UpdateLocalPath(); break;
                }

                if (_thumbnail != null)
                    _thumbnail.CreateThumbnails();
                goto Start;
            }
        }
        private static void Menu()
        {
            Console.WriteLine("1. Create Thumbnails For Product");
            Console.WriteLine("2. Create Thumbnails For Category");
            Console.WriteLine("3. Create Thumbnails Other");
            Console.WriteLine("4. Config Database Connection String");
            Console.WriteLine("5. Config Image Store Location");
            Console.WriteLine("0. Exits");
            Console.Write("Chose:");
        }
    }
}
