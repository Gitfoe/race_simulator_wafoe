using System;
using Model;
using Model.Classes;
using Model.Interfaces;
using Controller;
using Controller.Classes;
using System.Threading;

namespace View
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(100); // Timeout after 1 minute and 40 seconds
            Data.Initialize();
            Data.NextRace();
            Console.WriteLine(Data.CurrentRace.Track.Name);

            for (; ; ) // Unlimited loop to not immediately close it
            { }
        }
    }
}
