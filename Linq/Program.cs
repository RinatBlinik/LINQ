using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            Q6("2,5,7-10,11,17-18");
            Console.WriteLine();
            Q7("10, 5, 0, 8, 10, 1, 4, 0, 10, 1");
            Q8(('c', 6));
            //Q9
            Console.WriteLine();
            string s = "0,6,12,18,24,30,36,42,48,53,58,63,68,72,77,80,84,87,90,92,95,96,98,99,99,100,99,99,98,96,95,92,90,87,84,80,77,72,68,63,58,53,48,42,36,30,24,18,12,6,0,-6,-12,-18,-24,-30,-36,-42,-48,-53,-58,-63,-68,-72,-77,-80,-84,-87,-90,-92,-95,-96,-98,-99,-99,-100,-99,-99,-98,-96,-95,-92,-90,-87,-84,-80,-77,-72,-68,-63,-58,-53,-48,-42,-36,-30,-24,-18,-12,-6";
            Q9(s);
            Console.WriteLine();
            Q10("Yes,Yes,No,Yes,No,Yes,No,No,No,Yes,Yes,Yes,Yes,No,Yes,No,No,Yes,Yes");
            Console.WriteLine();
            Q11("Dog,Cat,Rabbit,Dog,Dog,Lizard,Cat,Cat,Dog,Rabbit,Guinea Pig,Dog");
            Console.WriteLine();
            Q12("1,2,1,1,0,3,1,0,0,2,4,1,0,0,0,0,2,1,0,3,1,0,0,0,6,1,3,0,0,0", 0);
            Q12("1,2,1,1,0,3,1,0,0,2,4,1,0,0,0,0,2,1,0,3,1,0,0,0,6,1,3,0,0,0", 1);
            Q12("1,2,1,1,0,3,1,0,0,2,4,1,0,0,0,0,2,1,0,3,1,0,0,0,6,1,3,0,0,0", 3);
            Q13("Santi Cazorla, Per Mertesacker, Alan Smith, Thierry Henry, Alex Song, Paul Merson, Alexis Sánchez, Robert Pires, Dennis Bergkamp, Sol Campbell");
            Q13("Alexis Cazorla, Per Mertesacker, Alan Smith, Per Henry, Alex Song, Paul Merson, Alexis Sánchez, Robert Pires, Dennis Bergkamp, Sol Campbell");
            Console.ReadLine();
        }

        public static void Q6(string s)
        {
            foreach (var item in s.Split(new char[] { ',', '-' })
                .Select(str => int.Parse(str)))
            {
                Console.Write($"{item} ");
            } 
        }

        public static void Q7(string s)
        {
            var sum = s.Split(',')
                 .Select(str => int.Parse(str))
                 .OrderBy(i => i)
                 .Skip(3)
                 .Sum();
            Console.WriteLine(sum);
        }

        public static IEnumerable<(char Left, int Right)> Q8I((char left, int right) pos)
        {
            char l = pos.left;
            int r = pos.right;
            while (l.CompareTo('a')>0 && r>1)
            {
                l = (char)(((int)l) - 1);
                r--;
                yield return (l, r);               
            }
            l = pos.left;
            r = pos.right;
            while (l.CompareTo('a') > 0 && r < 8)
            {
                l = (char)(((int)l) - 1);
                r++;
                yield return (l, r);
            }
        }
        public static void Q8((char left, int right) pos)
        {
            foreach (var item in Q8I((pos.left, pos.right)))
            {
                Console.Write($"{item.Left}{item.Right} ");
            }
            
        }
        public static void Q9(string s)
        {
            var res = s.Split(',')
                .Where((s, idx) => (idx + 1) % 5 == 0);
            foreach (var item in res)
            {
                Console.Write($"{item}, ");
            } 
        }
        public static void Q10(string s)
        {
            var votes = s.Split(',')
                .GroupBy(v => v)
                .OrderByDescending(g => g.Key)
                .Select(g => (Vote: g.Key, Count: g.Count()))
                .Aggregate((vYes, vNo) => ("Diff",vYes.Count - vNo.Count));
            Console.WriteLine(votes);
        }

        public static void Q11(string s)
        {
            var res = s.Split(',')
                .GroupBy(a => a);
            foreach (var g in res)
            {
                Console.Write($"{g.Key}:{g.Count()} ");
            }
        }
        public static void Q12(string s, int daysales)
        {
            var all = s.Split(',')
                .Select((s, idx) => (Sales: int.Parse(s), Idx: idx))
                .OrderBy(t => t.Idx);
            var iterator = all.GetEnumerator();
            int longestSequenceLength = 0;
            while(iterator.MoveNext())
            {
                int tmpLength = 0;
                int currentSales = iterator.Current.Sales;
                while(currentSales==daysales)
                {
                    tmpLength++;
                    if (tmpLength > longestSequenceLength)
                    {
                        longestSequenceLength = tmpLength;
                    }
                    if (iterator.MoveNext())
                    {
                        currentSales= iterator.Current.Sales;
                    }
                    else
                    {
                        break;
                    }
                }
                
            }
            Console.WriteLine($"Longest sequence with {daysales} sales is of long {longestSequenceLength}");
        }
        public static void Q13(string s)
        {
            var res = s.Split(',')
                .Select(n=> n.Trim().Split(' '))
                .Select(name => (First: name[0], Last: name[1]))
                .GroupBy(name => name.First)
                .Where(g => g.Count() > 1);
            if(res.Count()==0)
            {
                Console.WriteLine("No names share the same First Name!");
                return;
            }
            foreach (var g in res)
            {
                Console.WriteLine($"First Name - {g.Key} is shared for:");
                foreach (var name in g)
                {
                    Console.Write($"{name}, ");
                }
                Console.WriteLine();
            }
        }
    }
}
