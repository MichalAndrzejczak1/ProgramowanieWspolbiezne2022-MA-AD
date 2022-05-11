using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BouncingBalls.Data;

namespace BouncingBalls.Logic
{
    /// <summary>
    /// Abstrakcyjne API do zarządzania poruszaniem się obiektów.
    /// </summary>
    public abstract class LogicAbstractApi
    {
        /// <summary>
        /// Zdarzenie wywoływane podczas zmiany położenia obiektów.
        /// </summary>
        public event EventHandler CordinatesChanged;
        /// <summary>
        /// Czas co jaki występuje aktualizacja w milisekundach.
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// Dodaje nowy poruszający się obiekt.
        /// </summary>
        /// <returns>Numer nowo utworzonego obiektu.</returns>
        public abstract int Add();
        /// <summary>
        /// Usuwa poruszający się obiekt z listy obiektów.
        /// </summary>
        /// <param name="movingObject">Poruszający się obiekt, który występuje na liście obiektów.</param>
        public abstract void Remove(MovingObject movingObject);
        /// <summary>
        /// Zwraca poruszający się obiekt o podanym numerze.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public abstract MovingObject Get(int i);
        /// <summary>
        /// Aktualizuje pozycje poruszających się obiektów po podanym czasie w milisekundach.
        /// </summary>
        /// <param name="interval">Czas od ostatniej aktualizacji w milisekundach.</param>
        public abstract void Update(float interval);
        /// <summary>
        /// Zwraca położenie w poziomie dla konkretnego obiektu.
        /// </summary>
        /// <param name="objectNumber">Numer konkretnego obiektu.</param>
        /// <returns>Położenie w poziomie.</returns>
        public abstract double GetX(int objectNumber);
        /// <summary>
        /// Zwraca położenie w pionie dla konkretnego obiektu.
        /// </summary>
        /// <param name="objectNumber">Numer konkretnego obiektu.</param>
        /// <returns>Położenie w pionie.</returns>
        public abstract double GetY(int objectNumber);

        /// <summary>
        /// Rozpoczyna poruszanie się obiektami.
        /// </summary>
        public abstract void Start();
        /// <summary>
        /// Zatrzymuje poruszanie się obiektami.
        /// </summary>
        public abstract void Stop();
        /// <summary>
        /// Zwraca liczbę poruszających się obiektów.
        /// </summary>
        /// <returns>Liczba poruszających się obiektów.</returns>
        public abstract int Count();

        /// <summary>
        /// Tworzy warstwę logiki dla kul.
        /// </summary>
        /// <param name="width">Szerokość obszaru, po którym poruszają się kule.</param>
        /// <param name="height">Wysokość obszaru, po którym poruszają się kule.</param>
        /// <param name="data">API warstwy danych.</param>
        /// <returns>API logiki.</returns>
        public static LogicAbstractApi CreateLayer(int width, int height, DataAbstractApi data = default(DataAbstractApi))
        {
            return new BallLogic(data ?? DataAbstractApi.Create(width, height));
        }
        /// <summary>
        /// Zwraca promień kuli.
        /// </summary>
        /// <param name="ball">Poruszający się obiekt będący kulą.</param>
        /// <returns>Promień kuli.</returns>
        public static double GetBallRadius(MovingObject ball)
        {
            return DataAbstractApi.GetBallRadius(ball);
        }

        #region Layer implementation
        /// <summary>
        /// Implementacja logiki w postaci poruszających się kul.
        /// </summary>
        internal class BallLogic : LogicAbstractApi
        {
            public BallLogic(DataAbstractApi dataLayerApi)
            {
                dataLayer = dataLayerApi;
                service = new Logic.BallService();
                r = new Random();
                Interval = 30;
                task = Run();
            }

            public override int Add()
            {
                mutex.WaitOne();
                while (true)
                {
                    //var ray = r.Next(10, 25);
                    var ray = 15;
                    var x = r.NextDouble() * (dataLayer.BoardWidth - ray * 2.0);
                    var y = r.NextDouble() * (dataLayer.BoardHeight - ray * 2.0);
                    var speedX = (r.NextDouble() - 0.5) / 2.0;
                    var speedY = (r.NextDouble() - 0.5) / 2.0;

                    var ball = DataAbstractApi.CreateBall(x, y, speedX, speedY, ray);

                    if(dataLayer.GetAll().All(u => !service.Collision((MovingObject.Ball)u, (MovingObject.Ball)ball)))
                    {
                        var result = dataLayer.Add(ball);
                        mutex.ReleaseMutex();
                        return result;
                    }
                }
            }

            public override void Update(float interval)
            {
                mutex.WaitOne();
                if (isWorking)
                {
                    for(int i=0; i<dataLayer.Count(); i++)
                    {
                        dataLayer.Get(i).Move(interval);

                        service.WallBounce(dataLayer.Get(i), dataLayer.BoardWidth, dataLayer.BoardHeight);
                        service.BallBounce(dataLayer.GetAll(), i);
                    }
                    CordinatesChanged?.Invoke(this, EventArgs.Empty);
                }
                mutex.ReleaseMutex();
            }

            public override void Remove(MovingObject movingObject)
            {
                mutex.WaitOne();
                dataLayer.Remove(movingObject);
                mutex.ReleaseMutex();
            }

            public override MovingObject Get(int i)
            {
                return dataLayer.Get(i);
            }

            public override void Start()
            {
                isWorking = true;
            }

            public override void Stop()
            {
                isWorking = false;
            }

            public override int Count()
            {
                return dataLayer.Count();
            }

            public override double GetX(int objectNumber)
            {
                return dataLayer.Get(objectNumber).X;
            }

            public override double GetY(int objectNumber)
            {
                return dataLayer.Get(objectNumber).Y;
            }

            /// <summary>
            /// Warstwa danych.
            /// </summary>
            private readonly DataAbstractApi dataLayer;
            /// <summary>
            /// Usługa dla poruszających się kul.
            /// </summary>
            private readonly BallService service = default(Logic.BallService);
            /// <summary>
            /// Generator liczb pseudolosowych.
            /// </summary>
            private Random r;
            private readonly Mutex mutex = new Mutex();
            private Task task;
            private Boolean isWorking = false;
            private CancellationToken cancellationToken = CancellationToken.None;
            private readonly Stopwatch stopwatch = new Stopwatch();


            private async Task Run()
            {
                while(!cancellationToken.IsCancellationRequested)
                {
                    stopwatch.Reset();
                    stopwatch.Start();
                    if (!cancellationToken.IsCancellationRequested)
                        Update(Interval);
                    stopwatch.Stop();

                    await Task.Delay((int)(Interval-stopwatch.ElapsedMilliseconds), cancellationToken);
                }
            }
        }
    }
    #endregion Layer implementation
}
