using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Вывод в графический интерфейс.
    /// </summary>
    public interface IGameUI
    {
        /// <summary>
        /// Обновление информации о лазере.
        /// </summary>
        /// <param name="time">Текущее время отката.</param>
        /// <param name="maxTime">Максимальное время отката.</param>
        void UpdateLaserCounddown(float time, float maxTime);
        
        /// <summary>
        /// Обновление количества лазеров.
        /// </summary>
        /// <param name="count">Текущее количество.</param>
        /// <param name="max">Максимальное количество.</param>
        void UpdateLasersCount(int count, int max);
        
        /// <summary>
        /// Обновление времени отката регенерации количества лазеров.
        /// </summary>
        /// <param name="time">Время отката.</param>
        /// <param name="maxTime">Максимальное время отката.</param>
        void UpdateLaserRegenerationCountdown(float time, float maxTime);
        
        /// <summary>
        /// Обновление информации о скорости игрока.
        /// </summary>
        /// <param name="velocity">Скорость игрока.</param>
        void UpdatePlayerVelocity(Vector2 velocity);
        
        /// <summary>
        /// Обновление информации о позиции игрока.
        /// </summary>
        /// <param name="position">Позиция игрока.</param>
        void UpdatePlayerPosition(Vector2 position);
        
        /// <summary>
        /// Обновление информации о количестве очков игрока.
        /// </summary>
        /// <param name="score">Количество очков игрока.</param>
        void UpdateScore(int score);

        /// <summary>
        /// Обновление информации о текущем углу поворота.
        /// </summary>
        /// <param name="angle">Угол поворота.</param>
        void UpdatePlayerAngle(float angle);
    }
}