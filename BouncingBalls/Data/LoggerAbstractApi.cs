using System;
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
        public abstract void Info(String eventType, MovingBall ball);
        public abstract void Info(String eventType, List<MovingBall> balls);
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
            messages = new Queue<String>();
            task = Run(30, CancellationToken.None);
        }

        public override void Info(string eventType, MovingBall ball)
        {
            mutex.WaitOne();
            var l = new List<MovingBall> { ball };
            var m = new Message($"{DateTime.Now:yyyy-MM-dd HH-mm-ss-ffff}", eventType, l);
            messages.Enqueue(m.Serialize());
            mutex.ReleaseMutex();
            waitHandle.Set();
        }

        public override void Info(string eventType, List<MovingBall> balls)
        {
            mutex.WaitOne();
            var m = new Message($"{DateTime.Now:yyyy-MM-dd HH-mm-ss-ffff}", eventType, balls);
            messages.Enqueue(m.Serialize());
            mutex.ReleaseMutex();
            waitHandle.Set();
        }

        #region Private stuff
        private async Task Run(int interval, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    var m = GetMessages();
                    if(m != null)
                        while (m.Count > 0)
                        {
                            File.AppendAllText(filename, m.Dequeue() + "\n");
                        }
                }
                await waitHandle.WaitAsync(cancellationToken);
            }
        }

        private Queue<String> GetMessages()
        {
            mutex.WaitOne();
            if(messages.Count ==0)
            {
                mutex.ReleaseMutex();
                return null;
            }
            var m = new Queue<string>(messages);
            messages.Clear();
            mutex.ReleaseMutex();
            return m;
        }
        private class Message
        {
            private string timestamp;
            private string eventType;
            private List<MovingBall> balls;

            public string Timestamp { get => timestamp; }
            public string EventType { get => eventType; }
            public List<MovingBall> Balls { get => balls; }

            public Message(string timestamp, string eventType, List<MovingBall> balls)
            {
                this.timestamp = timestamp;
                this.eventType = eventType;
                this.balls = balls;
            }

            public string Serialize()
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(this);
            }
        }

        private readonly string filename;
        private readonly Queue<String> messages;
        private readonly Mutex mutex = new Mutex();
        private Task task;
        private readonly AsyncAutoResetEvent waitHandle = new AsyncAutoResetEvent(false);
        #endregion Private stuff
    }


    #endregion Logger implementation
}
