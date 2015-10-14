using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using TestAkka1.Messages;

namespace TestAkka1.Actors
{
    public class WordCounterActor : ReceiveActor
    {

        public static Props Create(String word)
        {
            return Props.Create(() => new WordCounterActor(word));
        }

        private String _theWord;
        private Int32 _count;
        public WordCounterActor(String word)
        {
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
                    Console.WriteLine("The word {0} appeared {1} times", _theWord, _count);
                }
            });
        }
    }
}
