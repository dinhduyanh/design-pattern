using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelExample
{
    internal class Program
    {
        private static MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        static async Task Main(string[] args)
        {
            //var start = DateTime.Now;
            //Console.WriteLine("Parallel For Loop");
            //var x = Parallel.For(0, 10, count =>
            //{
            //    DoSomething(count);
            //});

            //Console.WriteLine("Total: " + new DateTime((DateTime.Now - start).Ticks).Second);
            //Console.ReadLine();
            bool equal = string.Equals("Hello   ", "hello", StringComparison.OrdinalIgnoreCase);

            await AddAsync(1);
            await AddAsync(2);
            await AddAsync(3);
            await AddAsync(4);
            await AddAsync(5);

            Parallel.ForEach(new List<int> { 5, 2 }, item =>
            {
                var matchingItems = _list.AsParallel().Where(i => i == item);
                Process(matchingItems);
            });
            
        }

        private static void Process(IEnumerable<int> matchingItems)
        {
            foreach (var item in matchingItems)
            {
                Console.WriteLine(item);
            }
        }

        public interface INumber<T>
        {
            T Value { get; set; }
        }

        public class Number<T> : INumber<T> // Utilize generics to avoid boxing
        {
            public T Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }

        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 10);
        private static readonly HashSet<int> _list = new HashSet<int>();

        public static async Task AddAsync(int item)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                _list.Add(item);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
        static void DoSomething(int i)
        {
            Console.WriteLine("Start: " + i);
            Thread.Sleep(1000 * (10 - i));
            Console.WriteLine("End: " + i);
        }
    }
}
