using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BouncingBalls.Data
{
    /// <summary>
    /// Abstrakcyjne API dla logowania informacji.
    /// </summary>
    public abstract class LoggerAbstractApi
    {
        public abstract void Info(String eventType, object obj);
        public abstract void Info(String eventType, List<object> objects);
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
            messages = new Queue<Message>();
            task = Run(30, CancellationToken.None);
        }

        public override void Info(string eventType, object ball)
        {
            mutex.WaitOne();
            messages.Enqueue(new Message($"{DateTime.Now:yyyy-MM-dd HH-mm-ss-ffff}", eventType, ball));
            mutex.ReleaseMutex();
        }

        public override void Info(string eventType, List<object> objects)
        {
            mutex.WaitOne();
            messages.Enqueue(new Message($"{DateTime.Now:yyyy-MM-dd HH-mm-ss-ffff}", eventType, objects));
            mutex.ReleaseMutex();
        }

        #region Private stuff
        private async Task Run(int interval, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var m = GetMessages();
                    while (m.Count > 0)
                    {
                        File.AppendAllText(filename, m.Dequeue().Serialize() + "\n");
                    }
                }

                await Task.Delay((int)(interval), cancellationToken);
            }
        }

        private Queue<Message> GetMessages()
        {
            mutex.WaitOne();
            var m = new Queue<Message>(messages);
            messages.Clear();
            mutex.ReleaseMutex();
            return m;
        }

        private string filename;
        private readonly Queue<Message> messages;
        private readonly Mutex mutex = new Mutex();
        private Task task;
        #endregion Private stuff
    }

    internal class Message
    {
        public string Timestamp;
        public string EventType;
        public object Obj;

        public Message(string timestamp, string eventType, object obj)
        {
            Timestamp = timestamp;
            EventType = eventType;
            Obj = obj;
        }

        public string Serialize()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    #endregion Logger implementation
}
