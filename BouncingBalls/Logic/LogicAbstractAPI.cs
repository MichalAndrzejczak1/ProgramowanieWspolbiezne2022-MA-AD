using System;
using BouncingBalls.Data;

namespace BouncingBalls.Logic
{
    /// <summary>
    /// Abstrakcyjne API do zarządzania poruszaniem się obiektów.
    /// </summary>
    public abstract class LogicAbstractAPI
    {
        /// <summary>
        /// Zdarzenie wywoływane podczas zmiany położenia obiektów.
        /// </summary>
        public abstract event EventHandler CordinatesChanged;
        /// <summary>
        /// Dodaje nowy poruszający się obiekt.
        /// </summary>
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
        /// <param name="miliseconds">Czas od ostatniej aktualizacji w milisekundach.</param>
        public abstract void Update(float miliseconds);
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
        /// Ustala krok aktualizacji zmiany położenia poruszających się obiektów.
        /// </summary>
        /// <param name="miliseconds">Liczba milisekund co ile pozycja poruszających się obiektów będzie aktualizowana.</param>
        public abstract void SetInterval(int miliseconds);

        /// <summary>
        /// Tworzy warstwę logiki dla kul.
        /// </summary>
        /// <param name="width">Szerokość obszaru, po którym poruszają się kule.</param>
        /// <param name="height">Wysokość obszaru, po którym poruszają się kule.</param>
        /// <param name="data">API warstwy danych.</param>
        /// <returns>API logiki.</returns>
        public static LogicAbstractAPI CreateLayer(int width, int height, DataAbstractAPI data = default(DataAbstractAPI), ATimer timer = default(ATimer))
        {
            return new BallLogic(width, height, data ?? DataAbstractAPI.Create(), timer ?? ATimer.CreateWPFTimer());
        }
        /// <summary>
        /// Zwraca promień kuli.
        /// </summary>
        /// <param name="ball">Poruszający się obiekt będący kulą.</param>
        /// <returns>Promień kuli.</returns>
        public static double GetBallRadius(MovingObject ball)
        {
            return DataAbstractAPI.GetBallRadius(ball);
        }

        #region Layer implementation
        /// <summary>
        /// Implementacja logiki w postaci poruszających się kul.
        /// </summary>
        internal class BallLogic : LogicAbstractAPI
        {
            public override event EventHandler CordinatesChanged { add=> timer.Tick+=value; remove => timer.Tick-=value; }

            public BallLogic(int width, int height, DataAbstractAPI dataLayerAPI, ATimer wpfTimer)
            {
                dataLayer = dataLayerAPI;
                service = new Logic.BallService();
                boardHeight = height;
                boardWidth = width;
                r = new Random();
                timer = wpfTimer;
                SetInterval(30);
                timer.Tick += (sender, args) => Update(timer.Interval.Milliseconds);
            }

            public override int Add()
            {
                int ray = r.Next(10, 25);
                double x = r.NextDouble() * (boardWidth - ray * 2.0);
                double y = r.NextDouble() * (boardHeight - ray * 2.0);
                double speedX = (r.NextDouble() - 0.5) / 10.0;
                double speedY = (r.NextDouble() - 0.5) / 10.0;

                MovingObject ball = DataAbstractAPI.CreateBall(x, y, speedX, speedY, ray);
                return dataLayer.Add(ball);
            }

            public override void Update(float miliseconds)
            {
                for(int i=0; i<dataLayer.Count(); i++)
                {
                    dataLayer.Get(i).Move(miliseconds);

                    service.WallBounce(dataLayer.Get(i), boardWidth, boardHeight);
                }
            }

            public override void Remove(MovingObject movingObject)
            {
                dataLayer.Remove(movingObject);
            }

            public override MovingObject Get(int i)
            {
                return dataLayer.Get(i);
            }

            public override void Start()
            {
                timer.Start();
            }

            public override void Stop()
            {
                timer.Stop();
            }

            public override int Count()
            {
                return dataLayer.Count();
            }

            public override void SetInterval(int miliseconds)
            {
                timer.Interval = TimeSpan.FromMilliseconds(miliseconds);
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
            private readonly DataAbstractAPI dataLayer;
            /// <summary>
            /// Usługa dla poruszających się kul.
            /// </summary>
            private readonly BallService service = default(Logic.BallService);
            /// <summary>
            /// Szerokość obszaru, po którym poruszają się kule.
            /// </summary>
            private int boardWidth;
            /// <summary>
            /// Wysokość obszaru, po którym poruszają się kule.
            /// </summary>
            private int boardHeight;
            /// <summary>
            /// Generator liczb pseudolosowych.
            /// </summary>
            private Random r;
            /// <summary>
            /// Klasa zarządzająca aktualizacją położenia kul.
            /// </summary>
            ATimer timer;
        }
    }
    #endregion Layer implementation
}
