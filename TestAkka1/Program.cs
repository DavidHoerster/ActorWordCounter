using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using TestAkka1.Actors;
using TestAkka1.Messages;

namespace TestAkka1
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = PrintInstructionsAndGetFile();
            if (file == null)
            {
                return;
            }

            var system = ActorSystem.Create("helloAkka");

            var counter = system.ActorOf(CountSupervisor.Create(), "supervisor");
            counter.Tell(new StartCount(file));

            Console.ReadLine();
        }

        private static String PrintInstructionsAndGetFile()
        {
            Console.WriteLine("Word counter.  Select the document to count:");
            Console.WriteLine(" (1) Magna Carta");
            Console.WriteLine(" (2) Declaration of Independence");
            var choice = Console.ReadLine();
            String file = AppDomain.CurrentDomain.BaseDirectory + @"\Files\";

            if (choice.Equals("1"))
            {
                file += @"MagnaCarta.txt";
            }
            else if (choice.Equals("2"))
            {
                file += @"DeclarationOfIndependence.txt";
            }
            else
            {
                Console.WriteLine("Invalid -- bye!");
                return null;
            }

            return file;
        }
    }
}
