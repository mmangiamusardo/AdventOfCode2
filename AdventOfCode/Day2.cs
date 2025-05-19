using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode
{
    public class Day2
    {
        public static void GetSafeReports()
        {
            string inputReports = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";

            inputReports = @"8 6 4 4 1";

            int safeReportCount = 0;
            int unsafeReportCount = 0;

            string[] linesReport = inputReports.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            List<Report> reports = new List<Report>(); 

            foreach (string lr in linesReport)
            {
                var report = new Report(lr);

                /*
                string isIncreasing_Decreasing = report.IsIncreasing ? "Increasing" : "Decreasing";
                string isSafe_Unsafe = report.IsSafe ? "SAFE" : "UNSAFE";

                Console.WriteLine($"Report: {lr} {isIncreasing_Decreasing} / {isSafe_Unsafe}");
                */

                var value_next = report.ReportValues.Select(v => v.Value.ToString() + "(" + v.Next + ") d:" + v.Distance + "s:" + v.Sign);

                var reportsJoined = string.Join(" ", value_next);

                Console.Write($"{reportsJoined}");

                Console.WriteLine();
                

                Thread.Sleep(2000);
            }

            Console.ReadKey();
        }

        public class ReportValue 
        {
            public int? Prev { get; private set; }
            public int Value { get; private set; }

            public int? Next { get; private set; }
            
            public int Sign { get; private set; }

            public bool IsEdge {
                get 
                {
                    return !Prev.HasValue || !Next.HasValue;
                } 
            }

            public int? Distance { get; private set; }

            public ReportValue(int current, int? prev, int? next)
            {
                Value = current;

                Prev = prev;
                Next = next;

                if (Next.HasValue)
                {
                    Distance = Math.Abs(Next.Value - Value);
                    Sign = Math.Sign(Next.Value - Value);
                }
                else 
                {
                    Distance = null;
                    Sign = 0;
                }
            }
        }

        public class Report 
        {
            public bool IsIncreasing { get; private set; }
            public bool IsDecreasing { get; private set; }

            public bool IsSafe { get; private set; }
            //public List<int> ReportValues { get; private set; }

            public List<ReportValue> ReportValues = new List<ReportValue>();

            public Report(string report)
            {
                var values = Regex
                    .Matches(report, @"\d+")
                    .Cast<Match>()
                    .Select(a => int.Parse(a.Value))
                    .ToList();

                for (int i = 0; i < values.Count; i++)
                {
                    int? previous = null;
                    int? next = null;

                    if (i == 0)
                    {
                        previous = null;
                        next = values[i + 1];
                    }
                    else if (i == values.Count - 1) 
                    {
                        next = null;
                        previous = values[i - 1];
                    }
                    else if (i < values.Count - 1)
                    {
                        next = values[i + 1];
                        previous = values[i - 1];
                    }

                    var reportValue = new ReportValue(values[i], previous, next);

                    ReportValues.Add(reportValue);
                }

                var sumSign = ReportValues.Sum(v => v.Sign);

                if (Math.Abs(sumSign) == ReportValues.Where(v => v.Distance.HasValue || v.IsEdge).Count() - 1) 
                {
                    if (sumSign > 0)
                    {
                        IsIncreasing = true;
                        IsDecreasing = !IsIncreasing;
                    }
                    else 
                    {
                        IsDecreasing = true;
                        IsIncreasing = !IsDecreasing;
                    }
                }
            }
        }
    }
}
