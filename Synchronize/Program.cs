using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronize {
    class Program {
        static void Main(string[] args) {

            const int n = 1000000;
            const int maxValue = 100;
            var random = new Random();
            var table = new int[n];
            var check = new TableTasks();
            for(int i = 0; i < n; ++i) {
                table[i] = random.Next(maxValue);
            }
            Console.WriteLine("\nSum of table values: {0}",check.sumOfTableValues(n,table));
            Console.ReadKey();
        }
    }
    class TableTasks {
        public UInt64 sumOfTableValues(int n, int[] table) {
            UInt64 tableSum = 0;
            var tasks = new List<Task>();
            for(int i = 0; i < n; ++i) {
                int tmp = i;
                tasks.Add(Task.Run(() => {
                    lock(this) {
                        tableSum += (UInt64)table[tmp];
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());
            return tableSum;
        }
    }
}
