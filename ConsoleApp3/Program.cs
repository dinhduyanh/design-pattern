using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Program
    {
        static Queue<Thread> threadQueue = new Queue<Thread>();
        static List<Thread> threads;
        static void Main()
        {
            // Create and push threads into the queue
            //PushThreadToQueue(new Thread(() => DoWork(1)));
            //PushThreadToQueue(new Thread(() => DoWork(2)));
            //PushThreadToQueue(new Thread(() => DoWork(3)));

            //// Start processing the threads
            //ProcessThreads();

            //// Wait until all threads finish
            //WaitForThreads();

            //Console.WriteLine("All threads have finished.");

            // Creates and initializes a BitVector32 with all bit flags set to FALSE.
            BitVector32 myBV = new BitVector32(0);

            // Creates masks to isolate each of the first five bit flags.
            int myBit1 = BitVector32.CreateMask();
            int myBit2 = BitVector32.CreateMask(myBit1);
            int myBit3 = BitVector32.CreateMask(myBit2);
            int myBit4 = BitVector32.CreateMask(myBit3);
            int myBit5 = BitVector32.CreateMask(myBit4);

            // Sets the alternating bits to TRUE.
            Console.WriteLine("Setting alternating bits to TRUE:");
            Console.WriteLine("   Initial:         {0}", myBV.ToString());
            myBV[myBit1] = true;
            Console.WriteLine("   myBit1 = TRUE:   {0}", myBV.ToString());
            myBV[myBit3] = true;
            Console.WriteLine("   myBit3 = TRUE:   {0}", myBV.ToString());
            myBV[myBit5] = true;
            Console.WriteLine("   myBit5 = TRUE:   {0}", myBV.ToString());
        }

        static void PushThreadToQueue(Thread thread)
        {
            lock (threadQueue)
            {
                threadQueue.Enqueue(thread);
            }
        }

        static void ProcessThreads()
        {
            threads = new List<Thread> { };
            while (threadQueue.Count > 0)
            {
                Thread thread;
                lock (threadQueue)
                {
                    thread = threadQueue.Dequeue();
                    threads.Add(thread);
                }

                thread.Start();

                
            }
        }

        static void WaitForThreads()
        {
            while (true)
            {
                
                //lock (threadQueue)
                //{
                //    threads = threadQueue.ToArray();
                //}

                bool allFinished = true;
                foreach (var thread in threads)
                {
                    if (thread.IsAlive)
                    {
                        allFinished = false;
                        break;
                    }
                }

                if (allFinished)
                {
                    break;
                }

                Thread.Sleep(100);
            }
        }

        static bool DoWork(int i)
        {
            // Simulating some work
            Thread.Sleep(2000 * i);
            Console.WriteLine($"Sub-thread finished [{i}]:" + Thread.CurrentThread.ManagedThreadId);
            return true;
        }
    }
}
