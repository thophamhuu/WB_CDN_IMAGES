using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateThumbnail
{
    public sealed class Configs
    {
        private readonly static string localPathFile = "C:/CreateThumbnail/LocalPath.txt";
        private readonly static string settingsFile = "C:/CreateThumbnail/Settings.txt";
        private static volatile Configs instance;
        private static object syncRoot = new Object();
        private string settings { get; set; }
        private string localPath { get; set; }
        private Configs()
        {
            using (StreamReader sr = File.OpenText(localPathFile))
            {
                this.localPath = sr.ReadToEnd().Trim();
            }
            using (StreamReader sr = File.OpenText(settingsFile))
            {
                this.settings = sr.ReadToEnd().Trim();
            }
        }
        public static void Initialize()
        {
            if (!Directory.Exists("C:/CreateThumbnail"))
            {
                Directory.CreateDirectory("C:/CreateThumbnail");
            }
            if (!File.Exists(localPathFile))
            {
                string text = "D:/worldbuy/www";
                using (StreamWriter sw = File.CreateText(localPathFile))
                    sw.WriteLine(text);
            }
            if (!File.Exists(settingsFile))
            {
                string text = @"DataConnectionString: Data Source=.;Initial Catalog=worldbuy_e_04102018;Integrated Security=False;User Id=sa;Password=123456;MultipleActiveResultSets=True";
                using (StreamWriter sw = File.CreateText(settingsFile))
                    sw.WriteLine(text);
            }
            instance = new Configs();
        }
        public static void UpdateLocalPath()
        {
            if (!Directory.Exists("C:/CreateThumbnail"))
            {
                Directory.CreateDirectory("C:/CreateThumbnail");
            }
            Enter:
            string text = Instance.localPath;
            Console.WriteLine("Current Local Path: " + text);
            Console.Write("Enter Local Path(empty to exits):");
            
            text = Console.ReadLine();
            if (!String.IsNullOrEmpty(text))
            {
                while (!Directory.Exists(text))
                {
                    Console.WriteLine("Not Exits Folder");
                    goto Enter;
                }
                using (StreamWriter fs = File.CreateText(localPathFile))
                {
                    fs.WriteLine(text);
                }
            }

            instance = new Configs();
        }
        public static void UpdateSettings()
        {
            if (!Directory.Exists("C:/CreateThumbnail"))
            {
                Directory.CreateDirectory("C:/CreateThumbnail");
            }
            string text = Instance.Settings;
            Console.WriteLine("Current Database Connection Path: " + text);
            Console.Write("Enter Database Connection(empty to exits):");

            text = Console.ReadLine();
            if (!String.IsNullOrEmpty(text))
            {
                using (StreamWriter fs = File.CreateText(settingsFile))
                {
                    fs.WriteLine(text);
                }
            }
            instance = new Configs();
        }
        public static Configs Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Configs();
                    }
                }
                return instance;
            }
        }
        public string LocalPath
        {
            get
            {
                return this.localPath;
            }
        }
        public string Settings
        {
            get
            {
                return this.settings;
            }
        }
    }
}