using System;

namespace ApplicantChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Applicant challenge : Rimgaudas Mažulis\n");
            Pyramid pyramid = new Pyramid();
            pyramid.PrintMaxSum();
        }
    }
}
