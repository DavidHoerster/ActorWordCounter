﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Akka.Actor;
using TestAkka1.Messages;
using TestAkka1.Writers;

namespace TestAkka1.Actors
{
    public class LineReaderActor  : ReceiveActor
    {
        public static Props Create(IWriteStuff writer)
        {
            return Props.Create(() => new LineReaderActor(writer));
        }

        private readonly IWriteStuff _writer;
        public LineReaderActor(IWriteStuff writer)
        {
            _writer = writer;
            Receive<ReadLineForCounting>(msg =>
            {
                var cleanFileContents = Regex.Replace(msg.Line, @"[^\u0000-\u007F]", " ");

                var wordArray = cleanFileContents.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in wordArray)
                {
                    var wordCounter = Context.Child(word);
                    if (wordCounter == ActorRefs.Nobody)
                    {
                        wordCounter = Context.ActorOf(WordCounterActor.Create(_writer, word), word);
                    }

                    wordCounter.Tell(new CountWord());
                }
            });

            Receive<Complete>(msg =>
            {
                var children = Context.GetChildren();
                foreach (var child in children)
                {
                    child.Tell(new DisplayWordCount());
                }
            });
        }
    }
}