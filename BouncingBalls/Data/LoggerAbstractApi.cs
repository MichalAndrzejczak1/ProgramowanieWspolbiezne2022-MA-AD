using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace BouncingBalls.Data
{
    /// <summary>
    /// Abstrakcyjne API dla logowania informacji.
    /// </summary>
    public abstract class LoggerAbstractApi
    {
        internal abstract void Info(string data);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static LoggerAbstractApi Create()
        {
            return new BallLogger($"BouncingBallsLog {DateTime.Now:yyyy-MM-dd HH-mm-ss-ffff}");
        }
    }


    #region Logger implementation
    internal class BallLogger : LoggerAbstractApi
    {
        internal BallLogger(string logname)
        {
            filename = logname + ".log";
            messages = new ConcurrentQueue<String>();
            task = Run(30, CancellationToken.None);
        }

        internal override void Info(string data)
        {
            messages.Enqueue(data);
            waitHandle.Set();
        }


        #region Private stuff
        private async Task Run(int interval, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    while (!messages.IsEmpty)
                    {
                        string message;
                        if (messages.TryDequeue(out message))
                            File.AppendAllText(filename, message + "\n");
                    }
                }
                await waitHandle.WaitAsync();
            }
        }

        private readonly string filename;
        private readonly ConcurrentQueue<String> messages;
        private Task task;
        private readonly AsyncAutoResetEvent waitHandle = new AsyncAutoResetEvent(false);
        #endregion Private stuff
    }


    #endregion Logger implementation
}
