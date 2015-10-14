using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using TestAkka1.Messages;

namespace TestAkka1.Actors
{
    public class CountSupervisor : ReceiveActor
    {
        public static Props Create()
        {
            return Props.Create(() => new CountSupervisor());
        }


        public CountSupervisor()
        {
            Receive<StartCount>(msg =>
            {
                var fileInfo = new FileInfo(msg.FileName);
                var lineReader = Context.ActorOf(LineReaderActor.Create(), fileInfo.Name);

                using (var reader = fileInfo.OpenText())
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lineReader.Tell(new ReadLineForCounting(line));
                    }
                }

                lineReader.Tell(new Complete());
            });
        }
    }
}
