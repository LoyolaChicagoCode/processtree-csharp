using System;
using System.Collections.Generic;

namespace FakePS
{
    static class Program
    {
        private static readonly Random _rand = new Random();
        private static int rand(int minValue, int maxValue){
            return _rand.Next(minValue, maxValue);
        }

        static void Main(string[] args){
            var n = args.Length == 0 
                        ? 20 
                        : Int32.Parse(args[0]);
            var i = 2;
            var ps = new Dictionary<int, ICollection<int>>
                {
                    {0, new List<int>{1}},
                    {1, new List<int>()},
                };
            while(i <= n){
                var nextPid = i;
                var randomKey = rand(1, ps.Keys.Count - 1);
                if (!ps.ContainsKey(nextPid)) {
                    ps.Add(nextPid, new List<int>());
                }
                ps[randomKey].Add(nextPid);
                i += 1;
            }
            Console.WriteLine("PID PPID CMD");
            ps.Each(parentWithChildren=>
                parentWithChildren.Value.Each(child=>
                    Console.WriteLine("{0} {1} Fake Process {2}", child, parentWithChildren.Key, child)));
        }

        private static void Each<T>(this IEnumerable<T> list, Action<T> action){
            foreach (var item in list){
                action(item);
            }
        }
    }
}
