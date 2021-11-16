using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Описание метеорита.
    /// </summary>
    public class Meteor : EnemyBase
    {
        /// <summary>
        /// Тип метеорита.
        /// </summary>
        public MeteorType MeteorType = MeteorType.Big;

        /// <summary>
        /// Угловое ускорение.
        /// </summary>
        public float AngularVelocity = 30f;

        /// <summary>
        /// Количество очков за уничтожение.
        /// </summary>
        public override int CountScore { get; } = 10;

        /// <summary>
        /// Обновление поведения.
        /// </summary>
        /// <param name="gameManager">Игровой контроллер.</param>
        public override void Update(GameManager gameManager)
        {
            Angle += AngularVelocity * Time.deltaTime;
            base.Update(gameManager);
        }

        /// <summary>
        /// Создание частей после уничтожения.
        /// </summary>
        /// <param name="gameManager">Менеджер игры.</param>
        protected override void OnDead(GameManager gameManager)
        {
            if (MeteorType == MeteorType.Small)
            {
                return;
            }

            var meteor1 = gameManager.AddMeteor(MeteorType + 1, Position, Quaternion.Euler(0, 0, 30f) * Velocity * 2f);
            var meteor2 = gameManager.AddMeteor(MeteorType + 1, Position, Quaternion.Euler(0, 0, -30f) * Velocity * 2f);
            meteor1.Angle = Angle;
            meteor2.Angle = Angle;
            meteor2.AngularVelocity -= meteor1.AngularVelocity;

            switch (MeteorType)
            {
                case MeteorType.Big:
                    meteor2.Angle += 180f;
                    break;
                case MeteorType.Middle:
                    meteor2.Angle += 90f;
                    break;
            }
        }
    }
}