using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Лазер.
    /// </summary>
    public class Laser : EntityBase
    {
        /// <summary>
        /// Время исчезновения.
        /// </summary>
        private float _timeFade = 0.5f;
        
        /// <summary>
        /// Обработка исчезновения после выстрела.
        /// </summary>
        /// <param name="gameManager">Менеджер игры.</param>
        public override void Update(GameManager gameManager)
        {
            _timeFade -= Time.deltaTime;
            if (_timeFade <= 0)
            {
                CanBeDeleted = true;
            }
        }
    }
}