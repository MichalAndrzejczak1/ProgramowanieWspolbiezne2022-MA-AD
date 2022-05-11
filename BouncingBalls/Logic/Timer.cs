using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;

namespace BouncingBalls.Logic
{
    /// <summary>
    /// API dla timerów.
    /// </summary>
    public abstract class ATimer
    {
        /// <summary>
        /// Przechowuje zdarzenia wyzwalane co określony czas Interval.
        /// </summary>
        public abstract event EventHandler Tick;
        /// <summary>
        /// Czas w milisekundach co jaki zostają wyzwalane zdarzenia Tick.
        /// </summary>
        public abstract TimeSpan Interval { get; set; }
        /// <summary>
        /// Rozpoczyna działanie timera.
        /// </summary>
        public abstract void Start();
        /// <summary>
        /// Przerywa działanie timera.
        /// </summary>
        public abstract void Stop();
        /// <summary>
        /// Tworzy timer dla biblioteki WPF.
        /// </summary>
        /// <returns></returns>
        public static ATimer CreateWpfTimer()
        {
            return new WpfTimer();
        }
        /// <summary>
        /// Implementacja API timera dla biblioteki WPF.
        /// </summary>
        internal class WpfTimer : ATimer
        {
            public override event EventHandler Tick { add => timer.Tick += value; remove => timer.Tick -= value; }
            public override TimeSpan Interval { get => timer.Interval; set => timer.Interval = value; }

            public WpfTimer()
            {
                timer = new DispatcherTimer();
            }

            public override void Start()
            {
                timer.Start();
            }

            public override void Stop()
            {
                timer.Stop();
            }
            #region Private stuff
            /// <summary>
            /// Timer WPF.
            /// </summary>
            private DispatcherTimer timer;
            #endregion Private stuff
        }
    }
}
