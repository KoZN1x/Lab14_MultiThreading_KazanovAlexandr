using System.Runtime.InteropServices;

namespace Lab14_Multithreading_KazanovAlexandr
{
    internal class Program
    {

        static object syncObject = new();
        static void Main(string[] args)
        {
            int listCount = 10000000;
            int threadsNumber = 10;
            int range = listCount / threadsNumber;
            List<int> list = new List<int>(listCount);
            for (int i = 0; i < listCount; i++)
            {
                list.Add(i);
            }
            for (int i = 0; i < threadsNumber; i++)
            {
                int start = (i * range);
                int end = (i == threadsNumber - 1) ? listCount : (i + 1) * range;
                var thread = new Thread(() => CompleteTask(list, start, end));
                thread.Start();
            }
        }

        static void CompleteTask(List<int> list, int start, int end)
        {
            lock (syncObject)
            {
                for (int i = start; i < end; i++)
                {
                    if (list[i] % 3 == 0) Console.WriteLine($"mod 3 == {list[i]}. ThreadID: {Thread.CurrentThread.ManagedThreadId}");
                }
            }
        }
    }
}