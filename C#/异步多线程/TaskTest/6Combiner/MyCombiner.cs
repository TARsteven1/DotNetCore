using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Net;
using System.Threading;

namespace TaskTest._6Combiner
{
    //异步函数有一个让其保持一致的协议（可以一致的返回Task），这能让其保证良好的结果：可以使用以及编写Task祝贺器，也就是可以组合Task，但是并不关心Task具体做什么。
    //CLR提供了两个Task组合器
    //Task.WhenAny
    //Task.WhenAll
    class MyCombiner
    {
        //前提
        async Task<int> Delay1() { await Task.Delay(1000); return 1; }
        async Task<int> Delay2() { await Task.Delay(1000); return 2; }
        async Task<int> Delay3() { await Task.Delay(1000); return 3; }

        #region Task.WhenAny
        //Task.WhenAny：当一组Task中任何一个Task完成时，Task.WhenAny会返回完成的Task。
        async Task MyTaskWhenAny()
        {
            Task<int> winningTask = await Task.WhenAny(Delay1(), Delay2(), Delay3());//因为Task.WhenAny本身就返回一个Task,我们对他进行await，就会返回最先完成的Task。
            Console.WriteLine("Done");
            Console.WriteLine(winningTask.Result);//返回1
        }
        //上例完全是非阻塞的，包括最后一行（当访问result属性时，winningTask已完成），但最好还是对winningTask进行await，因为异常无需AggregateExceotion包装就会重新抛出：
        //Console.WirteLine(await winningTask);


        //实际上，我们可以在一步中执行两个await：
        // Task<int> winningTask = awaitawait Task.WhenAny(Delay1(),Delay2(),Delay3());

        //如果“没赢”的Task后续发生了错误，那么异常将不会被观察到，除非你后续对它们进行await（或者查询其Exception属性）
        //WhenAny很适合为不支持超时或取消的操作添加这些功能：
        async Task MyTaskWhenAny2()
        {
            Task<string> task = SomeAsyncFunc();
            Task winner = await (Task.WhenAny(task, Task.Delay(5000)));//为Task设置超时
            if (winner != task)
            {
                throw new TimeoutException();
            }
            string reuslt = await task;//Unwrap result/re-throw
        }
        // 注意：本例子中返回的结果是Task类型。

        private Task<string> SomeAsyncFunc()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Task.WhenAll
        //当传给它的所有的Task都完成后，Task.WhenAll会返回一个Task。
        async Task MyTaskWhenAll()
        {
            await Task.WhenAll(Delay1(), Delay2(), Delay3());//本例就会在3秒后结束。
        }

        async Task MyTaskWhenAll2()
        {
            //通过轮流对3个task进行awiat，也可以得到类似的结果：
            Task task1 = Delay1(), task2 = Delay2(), task3 = Delay3();
            await task1; await task2; await task3;

            //不同点是（除了3个await的低效）：如果task1出错，我们就无需等待task2和task3了，它的错误也不会被观察到。

        }

        // 与之相对，Task.WhenAll直到所有Task完成，它才会完成，及时有错误发生。如果有多个错误，他们在的异常会包裹在Task的AggregateException里
        //await组合的Task，只会抛出第一个异常，想要看到所有的异常，你需要这样做：
        async Task MyTaskWhenAllEx()
        {
            Task task1 = Task.Run(() => { throw null; });
            Task task2 = Task.Run(() => { throw null; });
            Task all = Task.WhenAll(task1, task2);
            try
            {
                await all;
            }
            catch
            {
                Console.WriteLine(all.Exception.InnerExceptions.Count);
            }
        }
        //对一组Task调用WhenAll会返回Task，也就是所有Task的组合结果。
        //如果进行await，那么就会得到TResult[]:
        async Task MyTaskWhenAllExR()
        {
            Task<int> task1 = Task.Run(() => 1);
            Task<int> task2 = Task.Run(() => 2);
            int[] results = await Task.WhenAll(task1, task2);
        }
        //实例
        async Task<int> GetTotalSize(string[] uris)
        {
            IEnumerable<Task<byte[]>> downloadTasks = uris.Select(uri => new WebClient().DownloadDataTaskAsync(uri));
            byte[][] contents = await Task.WhenAll(downloadTasks);
            return contents.Sum(c => c.Length);
        }
        //语法优化
       /* async Task<int> GetTotalSize(string[] uris,bool IsSame)
        {
            IEnumerable<Task<int>> downloadTasks = uris.Select(async uri => await new WebClient().DownloadDataTaskAsync(uri).Length);
            int[] contentLengths = await Task.WhenAll(downloadTasks);
            return contentLengths.Sum();
        }*/
        #endregion
    }
    static class MyExt
    {
        #region 自定义task组合器

        //可以编写自定义的Task组合器。最简单的组合器接收一个task，看下例：
        async static Task<TResult> WithTimeout<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            Task winner = await Task.WhenAny(task, Task.Delay(timeout)).ConfigureAwait(false);
            if (winner != task) throw new TimeoutException();
            return await task.ConfigureAwait(false);
        }

        //这就是为等待的task添加了超时功能
        //因为这可能是一个库方法，无需与外界共享状态，所以在await时我们使用了ConfigureAwait（false）来避免弹回到UI的同步上下文。
        //通过在Task完成时取消Task.Delay我们可以改进上例的效率（避免了计时器的小开销）：
        async static Task<TResult> WithTimeout<TResult>(this Task<TResult> task, TimeSpan timeout, bool IsSame)
        {
            var cancelSource = new CancellationTokenSource();
            var delay = Task.Delay(timeout, cancelSource.Token);
            Task winner = await Task.WhenAny(task, delay).ConfigureAwait(false);
            if (winner == task)
                cancelSource.Cancel();
            else
                throw new TimeoutException();
            return await task.ConfigureAwait(false);
        }

        //自定义task组合器 通过cancellationToken 放弃task
         static Task<TResult> WithCancellation<TResult>(this Task<TResult> task, CancellationToken cancelToken)
        {
            var tcs = new TaskCompletionSource<TResult>();
            var reg = cancelToken.Register(() => tcs.TrySetCanceled());
            task.ContinueWith(ant =>
            {
                reg.Dispose();
                if (ant.IsCanceled)
                    tcs.TrySetCanceled();
                else if (ant.IsFaulted)
                    tcs.TrySetException(ant.Exception.InnerException);
                else
                    tcs.TrySetResult(ant.Result);
            });
            return tcs.Task;
        }

        //接下来在看一个例子，这个组合器功能类似WhenAll,如果一个Task出错，那么其余的Task也立即出错：
        async static Task<TResult[]> WhenAllOrError<TResult>(params Task<TResult>[] tasks)
        {
            var killJoy = new TaskCompletionSource<TResult[]>();
            foreach (var task in tasks)
            {
                task.ContinueWith(ant =>
                {
                    if (ant.IsCanceled)
                        killJoy.TrySetCanceled();
                    else if (ant.IsFaulted)
                        killJoy.TrySetException(ant.Exception.InnerException);
                });
            }
            return await await Task.WhenAny(killJoy.Task, Task.WhenAll(tasks)).ConfigureAwait(false);
        }
        //上述代码中，TaskCompletionSourced的任务就是当任意一个Task出错时，结束工作。所以我们没有调用SetResult方法，只调用了它的TrySetCanceled和TrySetException方法。这里ContinueWith要比GetAwaiter().OnCompleted更方便，因为我们不访问Task的result，并且此刻不想弹回到UI线程。
        #endregion
    }
}
