using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Летающая тарелка.
    /// </summary>
    public class Ufo : EnemyBase
    {
        /// <summary>
        /// Количество очков за уничтожение.
        /// </summary>
        public override int CountScore { get; } = 20;

        /// <summary>
        /// Текущий угол наклона.
        /// </summary>
        private float _currentAngle = 0f;

        /// <summary>
        /// Скорость перемещения.
        /// </summary>
        private float Speed = 100f;
        
        /// <summary>
        /// Обновление поведения.
        /// </summary>
        /// <param name="gameManager">Игровой контроллер.</param>
        public override void Update(GameManager gameManager)
        {
            Turning(gameManager);
            MoveToPlayer(gameManager);
            base.Update(gameManager);
        }

        /// <summary>
        /// Перемещение за игроком.
        /// </summary>
        /// <param name="gameManager"></param>
        private void MoveToPlayer(GameManager gameManager)
        {
            var targetVector = (gameManager.Player.Position - Position).normalized * Speed * gameManager.GameWindow.GetTimeStep();
            Velocity = Vector2.ClampMagnitude(Velocity + targetVector, Speed);
        }
        
        /// <summary>
        /// Небольшие наклоны для живой картинки.
        /// </summary>
        /// <param name="gameManager">Игровой менеджер.</param>
        private void Turning(GameManager gameManager)
        {
            _currentAngle += gameManager.GameWindow.GetTimeStep() * 2f;

            if (_currentAngle > 6.28f)
            {
                _currentAngle = 0f;
            }
            
            Angle = Mathf.Sin(_currentAngle) * 25f;
        }

    }
}