using System;
using AdventOfCode2020.Day4PassportProcessing;
using AdventOfCode2020.Day2PasswordPhilosophy;
using static AdventOfCode2020.Day1ExpenseReport.ExpenseReport;
using static AdventOfCode2020.Day2PasswordPhilosophy.PasswordPhilosophy;
using static AdventOfCode2020.Day3TobogganTrajectory.TobogganTrajectory;

namespace AdventOfCode2020
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("------ Day 1 ------");
            Console.WriteLine();
            //Day 1 - Part 1
            Console.WriteLine($"The two numbers that sum to 2020 multiply to: {FixReport()}");
            //Day 1 - Part 2
            Console.WriteLine($"The three numbers that sum to 2020 multiply to: {FixReportAgain()}");
            
            Console.WriteLine();
            Console.WriteLine("------ Day 2 ------");
            Console.WriteLine();
            //Day 2 - Part 1
            Console.WriteLine($"The number of valid passwords (range) is: {CountValidPasswords<PasswordInfoWithRange>()}");
            //Day 2 - Part 2
            Console.WriteLine($"The number of valid passwords (positions) is: {CountValidPasswords<PasswordInfoWithPositions>()}");
            
            Console.WriteLine();
            Console.WriteLine("------ Day 3 ------");
            Console.WriteLine();
            //Day 3 - Part 1
            var slope = new Slope(3, 1);
            Console.WriteLine($"With a slope of {slope.Drop}, {slope.Run} you will encounter {CountTreesOnRoute(slope)} trees");

            var slopes = new[]
            {
                new Slope(1, 1),
                new Slope(3, 1),
                new Slope(5, 1),
                new Slope(7, 1),
                new Slope(1, 2)
            };
            //Day 3 - Part 2
            Console.WriteLine($"If you multiply the number of trees on all routes, you get {CheckAllRoutes(slopes)}");
            
            Console.WriteLine();
            Console.WriteLine("------ Day 4 ------");
            Console.WriteLine();
            //Day 4 - Part 1
            Console.WriteLine($"The number of passports with all required fields is {new PassportProcessing().CountPassportsWithAllRequiredFields()}");
            //Day 4 - Part 2
            Console.WriteLine($"The number of valid passports is {new PassportProcessing().CountValidPassports()}");
        }
    }
}