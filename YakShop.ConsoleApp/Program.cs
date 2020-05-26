using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

using YakShop.Models;

namespace YakShop.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Check proper arguments and display error.
            if (args.Length != 2)
            {
                throw new ArgumentOutOfRangeException("Required arguments 2: first argument XML file to read, second argument elapsed days. Try again.");
            }
            // initialise parsed string and days elapsed 
            var xmlString = new StringBuilder();
            int days = 0;

            // Try read arguments.
            try
            {
                days = Convert.ToInt32(args[1]);
                if (days < 0)
                {
                    throw new FormatException("Days must be a positive integer");
                }
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            // Read xml, ask sanitization 
            using (var reader = new StreamReader(args[0]))
            {
                while (reader.Peek() >= 0)
                    xmlString.AppendLine(reader.ReadLine());
            }

            var yakHerd = XmlParser(xmlString.ToString());
            // Generate our stock using the herd and days.
            Stock stock = new Stock(yakHerd, days);

            System.Console.WriteLine("====== Stock ======");
            System.Console.WriteLine("{0} liters of milk", stock.litersOfMik);
            System.Console.WriteLine("{0} skins of wool \n", stock.skinsOfWool);

            System.Console.WriteLine("======= Herd ======");
            foreach (Yak yak in yakHerd)
            {
                System.Console.WriteLine("{0} {1} years old", yak.Name, yak.Age);
            }
            System.Console.WriteLine("\n====== Thank you for shoping from YakShop ======\nAdios!\n");

        }


        // Helper functions. 
        public static List<Yak> XmlParser(string xmlStr)
        {

            // Create our Herb list
            var yakHerd = new List<Yak>();
            // Our xml document.
            XmlDocument xDoc = new XmlDocument();
            // Load document using StringReader.
            xDoc.Load(new StringReader(xmlStr));
            // Use tag provided by example
            XmlNodeList ladyakNodes = xDoc.GetElementsByTagName("labyak");
            // Generated id.
            int id = 0;
            // for each node of tags create and add a Yak
            foreach (XmlNode node in ladyakNodes)
            {
                yakHerd.Add(
                    new Yak(id, node.Attributes.GetNamedItem("name").InnerText,
                    Convert.ToDouble(node.Attributes.GetNamedItem("age").InnerText),
                    node.Attributes.GetNamedItem("sex").InnerText.Equals("m") ? Sex.m : Sex.f)
                );
                id++;
            }
            return yakHerd;
        }
    }


}
