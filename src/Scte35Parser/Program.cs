using System;

namespace Scte35Parser
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var parser = new Scte35Parser();
            var splice = parser.ParseFromBase64("/DAlAAAAAsrYAP/wFAWAAAUNf+/+ARIzUP4Ae5igAAEBAQAAIirEHw==");
            Console.WriteLine("Hello World!");
        }
    }
}