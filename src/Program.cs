﻿using System;
using System.Diagnostics;

namespace PrimeNumberCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                ShowUsage();
            }
            else
            {
                var command = args[0];
                var input = args[1];

                if (!ulong.TryParse(input, out var result))
                {
                    Console.WriteLine("PrimeCalc: Only positive 64-bit integers are valid for the second argument.");
                }
                else
                {
                    switch (command)
                    {
                        case "-isprime":
                            ShowPrimeFacts(result);
                            break;
                        case "-count":
                            var i = 0;
                            foreach (var prime in PrimeNumberUtils.GetFirstNPrimes(result))
                            {
                                Console.WriteLine($"{++i}: {prime}");
                            }
                            break;
                        default:
                            ShowUsage();
                            break;
                    }    
                }
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("PrimeCalc:");
            Console.WriteLine(" Usage: [-isprime X] to check whether X is prime.");
            Console.WriteLine("     or [-count N] to return a count of the first N primes.");
        }

        private static void ShowPrimeFacts(ulong input)
        {
            try
            {
                var stopWatch = Stopwatch.StartNew();
                bool isPrime = input.IsPrime();
                var isPrimeStr = isPrime ? "prime" : "not prime";
                Console.WriteLine($"{input} is {isPrimeStr}");

                if (!isPrime)
                {
                    foreach (var factor in PrimeNumberUtils.GetPrimeFactorization(input))
                    {
                        var power = factor.Value > 1 ? $" ^ {factor.Value}" : string.Empty;
                        Console.WriteLine($" > {factor.Key}{power}");
                    }
                }

                stopWatch.Stop();
                if (stopWatch.ElapsedMilliseconds < 1000)
                {
                    Console.WriteLine("Calculation took less than a second.");
                }
                else
                {
                    Console.WriteLine($"Calculation took about {stopWatch.ElapsedMilliseconds / 1000} seconds.");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"An error was encountered: {e.Message}");
            }
        }
    }
}
