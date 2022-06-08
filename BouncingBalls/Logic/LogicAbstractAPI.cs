using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using BouncingBalls.Data;

namespace BouncingBalls.Logic
{
    /// <summary>
    /// Abstrakcyjne API do zarządzania poruszaniem się obiektów.
    /// </summary>
    public abstract class LogicAbstractApi : INotifyPropertyChanged
    {
        /// <summary>
        /// Event informujący o zmianie składowej obiektu.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
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
        /// <param name="movingBall">Poruszający się obiekt, który występuje na liście obiektów.</param>
        public abstract void Remove(MovingBall movingBall);
        /// <summary>
        /// Zwraca poruszający się obiekt o podanym numerze.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public abstract MovingBall Get(int i);
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
        /// Zwraca szerokość tablicy po której poruszają się kule.
        /// </summary>
        /// <returns>Szerokość tablicy po której poruszają się kule.</returns>
        public abstract double GetBoardWidth();
        /// <summary>
        /// Zwraca wysokość tablicy po której poruszają się kule.
        /// </summary>
        /// <returns>Wysokość tablicy po której poruszają się kule.</returns>
        public abstract double GetBoardHeight();

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
        /// Informuje czy symulacja działa.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsRunning();


        /// <summary>
        /// Tworzy warstwę logiki dla kul.
        /// </summary>
        /// <param name="data">API warstwy danych.</param>
        /// <param name="logger">API logowania informacji.</param>
        /// <returns>API logiki.</returns>
        public static LogicAbstractApi CreateLayer(DataAbstractApi data = default(DataAbstractApi), LoggerAbstractApi logger = default(LoggerAbstractApi))
        {
            return new BallLogic(data ?? DataAbstractApi.Create(), logger ?? LoggerAbstractApi.Create());
        }
        /// <summary>
        /// Zwraca promień kuli.
        /// </summary>
        /// <param name="ball">Poruszający się obiekt będący kulą.</param>
        /// <returns>Promień kuli.</returns>
        public static double GetBallRadius(MovingBall ball)
        {
            return DataAbstractApi.GetBallRadius(ball);
        }

        #region Layer implementation
        /// <summary>
        /// Implementacja logiki w postaci poruszających się kul.
        /// </summary>
        internal class BallLogic : LogicAbstractApi
        {
            public BallLogic(DataAbstractApi dataLayerApi, LoggerAbstractApi loggerAbstractApi)
            {
                dataLayer = dataLayerApi;
                loggerApi = loggerAbstractApi;
                service = new BallService();
                r = new Random();
                Interval = 30;
                cancellationTokenSource = null;
            }

            public override int Add()
            {
                mutex.WaitOne();
                while (true)
                {
                    int ray = 15;
                    double x = r.NextDouble() * (dataLayer.BoardWidth - ray * 2.0);
                    double y = r.NextDouble() * (dataLayer.BoardHeight - ray * 2.0);
                    double speedX = (r.NextDouble() - 0.5) / 2.0;
                    double speedY = (r.NextDouble() - 0.5) / 2.0;

                    MovingBall ball = DataAbstractApi.CreateBall(dataLayer.Count(), x, y, speedX, speedY, ray);

                    if (dataLayer.GetAll().All(u => !service.Collision((MovingBall.Ball)u, (MovingBall.Ball)ball)))
                    {
                        int result = dataLayer.Add(ball);
                        ball.PropertyChanged += BallPositionChanged;
                        loggerApi?.Info("Creation", ball);
                        mutex.ReleaseMutex();
                        
                        return result;
                    }
                }
            }

            public override void Remove(MovingBall movingBall)
            {
                mutex.WaitOne();
                loggerApi?.Info("Removing", movingBall);
                dataLayer.Remove(movingBall);
                mutex.ReleaseMutex();
            }

            public override MovingBall Get(int i)
            {
                return dataLayer.Get(i);
            }

            public override double GetBoardHeight()
            {
                return dataLayer.BoardHeight;
            }

            public override void Start()
            {
                cancellationTokenSource = new CancellationTokenSource();
                cancellationToken = cancellationTokenSource.Token;
                foreach (MovingBall ball in dataLayer.GetAll())
                    ball.CreateMovementTask(Interval, cancellationToken);
            }

            public override void Stop()
            {
                if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
                    cancellationTokenSource.Cancel();
            }

            public override int Count()
            {
                return dataLayer.Count();
            }

            public override bool IsRunning()
            {
                return cancellationTokenSource != null && !cancellationToken.IsCancellationRequested;
            }

            public override double GetX(int objectNumber)
            {
                return dataLayer.Get(objectNumber).X;
            }

            public override double GetY(int objectNumber)
            {
                return dataLayer.Get(objectNumber).Y;
            }

            public override double GetBoardWidth()
            {
                return dataLayer.BoardWidth;
            }

            void BallPositionChanged(object sender, PropertyChangedEventArgs args)
            {
                MovingBall ball = (MovingBall)sender;
                Update(ball);
            }

            #region Private stuff
            /// <summary>
            /// Warstwa danych.
            /// </summary>
            private readonly DataAbstractApi dataLayer;
            /// <summary>
            /// Usługa dla poruszających się kul.
            /// </summary>
            private readonly BallService service;
            /// <summary>
            /// Generator liczb pseudolosowych.
            /// </summary>
            private Random r;
            private readonly Mutex mutex = new Mutex();
            private CancellationTokenSource cancellationTokenSource;
            private CancellationToken cancellationToken;
            private readonly LoggerAbstractApi loggerApi = null;

            void Update(MovingBall ball)
            {
                mutex.WaitOne();

                if (ball == null)
                {
                    mutex.ReleaseMutex();
                    return;
                }
                loggerApi?.Info("Moving", ball);
                if (service.WallBounce(ball, dataLayer.BoardWidth, dataLayer.BoardHeight))
                    loggerApi?.Info("WallBounce", ball);
                int bouncedBallId = service.BallBounce(dataLayer.GetAll(), ball.Id);
                if (bouncedBallId != -1)
                    loggerApi?.Info("BallBounce", new List<MovingBall> { ball, dataLayer.Get(bouncedBallId) });

                OnPropertyChanged(ball);
                mutex.ReleaseMutex();
            }

            /// <summary>
            /// Utwórz metodę OnPropertyChanged, aby wywołać zdarzenie. Jako parametr zostanie użyta nazwa członka wywołującego.
            /// </summary>
            /// <param name="ball">Kula do aktualizacji.</param>
            /// <param name="name">Nazwa parametru.</param>
            private void OnPropertyChanged(MovingBall ball, [CallerMemberName] string name = null)
            {
                PropertyChanged?.Invoke(ball, new PropertyChangedEventArgs(name));
            }

            #endregion Private stuff
        }
    }
    #endregion Layer implementation
}
