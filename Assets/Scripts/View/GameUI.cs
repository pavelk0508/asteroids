using Asteroids.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.View
{
    /// <summary>
    /// Основной игровой UI.
    /// </summary>
    public class GameUI : MonoBehaviour, IGameUI
    {
        /// <summary>
        /// Поле вывода времени отката лазера.
        /// </summary>
        public Text LazerCountdownField;
        
        /// <summary>
        /// Поле вывода времени отката генерации лазера.
        /// </summary>
        public Text LazerGenerationCountdownField;
        
        /// <summary>
        /// Поле вывода количества лазера.
        /// </summary>
        public Text LazerCountField;
        
        /// <summary>
        /// Поле вывода скорости игрока.
        /// </summary>
        public Text VelocityField;
        
        /// <summary>
        /// Поле вывода позиции игрока.
        /// </summary>
        public Text PositionField;

        /// <summary>
        /// Поле вывода угла поворота игрока.
        /// </summary>
        public Text RotationField;

        /// <summary>
        /// Поле вывода количества очков игрока.
        /// </summary>
        public Text ScoreField;
        
        /// <summary>
        /// Поле вывода количества очков игрока на экране окончания игры.
        /// </summary>
        public Text TotalScoreField;
        
        /// <summary>
        /// Обновление информации о лазере.
        /// </summary>
        /// <param name="time">Текущее время отката.</param>
        /// <param name="maxTime">Максимальное время отката.</param>
        public void UpdateLazerCounddown(float time, float maxTime)
        {
            LazerCountdownField.text = $"Откат лазера: {time:F} / {maxTime:F}";
        }

        /// <summary>
        /// Обновление количества лазеров.
        /// </summary>
        /// <param name="count">Текущее количество.</param>
        /// <param name="max">Максимальное количество.</param>
        public void UpdateLazersCount(int count, int max)
        {
            LazerCountField.text = $"Количество лазера: {count} / {max}";
        }

        /// <summary>
        /// Обновление времени отката регенерации количества лазеров.
        /// </summary>
        /// <param name="time">Время отката.</param>
        /// <param name="maxTime">Максимальное время отката.</param>
        public void UpdateLazerRegenerationCountdown(float time, float maxTime)
        {
            LazerGenerationCountdownField.text = $"Генерация лазера: {time:F} / {maxTime:F}";
        }

        /// <summary>
        /// Обновление информации о скорости игрока.
        /// </summary>
        /// <param name="velocity">Скорость игрока.</param>
        public void UpdatePlayerVelocity(Vector2 velocity)
        {
            VelocityField.text = $"Скорость: {velocity}";
        }

        /// <summary>
        /// Обновление информации о позиции игрока.
        /// </summary>
        /// <param name="position">Позиция игрока.</param>
        public void UpdatePlayerPosition(Vector2 position)
        {
            PositionField.text = $"Позиция: {position}";
        }

        /// <summary>
        /// Обновление информации о количестве очков игрока.
        /// </summary>
        /// <param name="score">Количество очков игрока.</param>
        public void UpdateScore(int score)
        {
            ScoreField.text = $"Количество очков: {score}";
            TotalScoreField.text = ScoreField.text;
        }

        /// <summary>
        /// Обновление информации о текущем углу поворота.
        /// </summary>
        /// <param name="angle">Угол поворота.</param>
        public void UpdatePlayerAngle(float angle)
        {
            RotationField.text = $"Поворот: {angle:F}";
        }
    }
}