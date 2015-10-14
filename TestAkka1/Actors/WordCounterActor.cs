using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using TestAkka1.Messages;
using TestAkka1.Writers;

namespace TestAkka1.Actors
{
    public class WordCounterActor : ReceiveActor
    {

        public static Props Create(IWriteStuff writer, String word)
        {
            return Props.Create(() => new WordCounterActor(writer, word));
        }

        private readonly IWriteStuff _writer;
        private String _theWord;
        private Int32 _count;
        public WordCounterActor(IWriteStuff writer, String word)
        {
            _writer = writer;
            _theWord = word;
            _count = 0;

            Receive<CountWord>(msg =>
            {
                _count++;
            });

            Receive<DisplayWordCount>(msg =>
            {
                if (_count > 25)
                {
                    _writer.WriteLine("The word {0} appeared {1} times", _theWord, _count);
                }
            });
        }
    }
}
