using System;
using System.Diagnostics;
using System.Threading;

public record Order(int Id, decimal Amount);

public class Program
{
    private static void ProcessOrder(Order order)
    {
        Console.WriteLine(
            $"Заказ {order.Id}: поток " +
            $"{Environment.CurrentManagedThreadId}"
        );

        // Имитация затратной работы.
        Thread.Sleep(100);

        decimal total = order.Amount * 1.05m;

        Console.WriteLine(
            $"Заказ {order.Id} завершён. Сумма: {total:F2}"
        );
    }

    public static void Main()
    {
        Order[] orders =
        {
            new(1, 100),
            new(2, 200),
            new(3, 300),
            new(4, 400),
            new(5, 500),
            new(6, 600),
            new(7, 700),
            new(8, 800)
        };

        // TODO 1. Измерьте время последовательной обработки.
        // Обработайте все заказы циклом через ProcessOrder.
        // Сохраните результат Stopwatch.Elapsed.
        Stopwatch sw = Stopwatch.StartNew();
        foreach (Order o in orders)
        {
            ProcessOrder(o);
        }
        sw.Stop();
        Console.WriteLine($"\nTime consumed : {sw.Elapsed}");

        Console.WriteLine("\n------\n");

        // TODO 2. Создайте массив Thread размером orders.Length.

        Stopwatch sw1 = Stopwatch.StartNew();

        Thread[] threads = new Thread[orders.Length];

        // TODO 3. Для каждого заказа создайте поток,
        // который вызывает ProcessOrder для этого заказа.

        for (int i = 0; i < threads.Length; i++)
        {
            Order curOrder = orders[i];

            threads[i] = new Thread
            (() =>
            {
                ProcessOrder(curOrder);
            });
            // TODO 4. Запустите все созданные потоки.
            threads[i].Start();
            // TODO 5. Дождитесь завершения каждого потока
            // с помощью Join().
            threads[i].Join();
        }

        // TODO 6. Измерьте длительность многопоточной
        // обработки и выведите оба результата.

        sw.Stop();
        Console.WriteLine($"\nTime consumed : {sw.Elapsed}");
    }
}