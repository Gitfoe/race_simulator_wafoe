using System;
using Model;
using Model.Classes;
using Model.Interfaces;
using Controller;
using Controller.Classes;
using View.Classes;
using System.Threading;
using System.Collections.Generic;

namespace View
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(100); // Timeout after 1 minute and 40 seconds
            Data.Initialize();
            //Data.NextRace();
            Visualisation.DrawTrack(Data.GrandPrix.Tracks.Peek());

            for (; ; ) // Unlimited loop to not immediately close it
            { }

            //string[] _startGridWest =       { "----",
            //                                             "  ← ",
            //                                             " ←  ",
            //                                             "----" };

            //foreach (var item in _startGridWest)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}
