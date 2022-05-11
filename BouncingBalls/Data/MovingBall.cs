using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace BouncingBalls.Data
{
    /// <summary>
    /// Bazowa klasa dla wszystkich poruszających się obiektów.
    /// </summary>
    public abstract class MovingBall : INotifyPropertyChanged
    {
        /// <summary>
        /// Numer obiektu z listy, służy jako identyfikator. Liczone od 0.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Event informujący o zmianie składowej obiektu.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Położenie w poziomie.
        /// </summary>
        public double X { get; set; } 
        /// <summary>
        /// Położenie w pionie.
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Prędkość w poziomie, wartość co jaką obiekt przesunie się co milisekundę.
        /// </summary>
        public double SpeedX { get; set; }
        /// <summary>
        /// Prędkość w pionie, wartość co jaką obiekt przesunie się co milisekundę.
        /// </summary>
        public double SpeedY { get; set; }
        /// <summary>
        /// Promień kuli.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Porusza obiektem po określonym czasie milisekund.
        /// </summary>
        /// <param name="interval">Ile milisekund minęło od ostatniej aktualizacji.</param>
        public abstract void Move(double interval);
        /// <summary>
        /// Tworzy nowe zadanie ruchu obiektu.
        /// </summary>
        /// <param name="interval">Czas w milisekundach, co jaki kulka wykona ruch</param>
        /// <param name="cancellationToken"></param>
        public abstract void CreateMovementTask(int interval, CancellationToken cancellationToken);

        /// <summary>
        /// Utwórz metodę OnPropertyChanged, aby wywołać zdarzenie. Jako parametr zostanie użyta nazwa członka wywołującego.
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Poruszająca się kula.
        /// </summary>
        internal class Ball : MovingBall
        {

            /// <summary>
            /// Tworzy kulę.
            /// </summary>
            /// <param name="id">Numer obiektu z listy, służy jako identyfikator.</param>
            /// <param name="x">Położenie w poziomie.</param>
            /// <param name="y">Położenie w pionie.</param>
            /// <param name="speedX">Prędkość w poziomie, wartość co jaką obiekt przesunie się co milisekundę.</param>
            /// <param name="speedY">Prędkość w pionie, wartość co jaką obiekt przesunie się co milisekundę.</param>
            /// <param name="radius">Promień kuli.</param>
            public Ball(int id,double x, double y, double speedX, double speedY, double radius)
            {
                Id = id;
                X = x;
                Y = y;
                SpeedX = speedX;
                SpeedY = speedY;
                Radius = radius;
            }
            /// <summary>
            /// Porusza kulą po określonym czasie milisekund.
            /// </summary>
            /// <param name="interval">Ile milisekund minęło od ostatniej aktualizacji.</param>
            public override void Move(double interval)
            {
                X += SpeedX * interval;
                Y += SpeedY * interval;
            }

            public override void CreateMovementTask(int interval, CancellationToken cancellationToken)
            {
                _ = Run(interval, cancellationToken);
            }

            private async Task Run(int interval, CancellationToken cancellationToken)
            {
                while(!cancellationToken.IsCancellationRequested)
                {
                    stopwatch.Reset();
                    stopwatch.Start();
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        Move(interval);
                        OnPropertyChanged();
                    }
                    stopwatch.Stop();

                    await Task.Delay((int)(interval-stopwatch.ElapsedMilliseconds), cancellationToken);
                }
            }

            
            private readonly Stopwatch stopwatch = new Stopwatch();
        }
    }
}
