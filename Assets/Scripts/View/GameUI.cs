using Asteroids.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.View
{
    public class GameUI : MonoBehaviour, IGameUI
    {
        public Text LazerCountdownField;
        public Text LazerGenerationCountdownField;
        public Text LazerCountField;
        public Text VelocityField;
        public Text PositionField;
        public Text ScoreField;
        
        public Text TotalScoreField;
        
        public void UpdateLazerCounddown(float time, float maxTime)
        {
            LazerCountdownField.text = $"Откат лазера: {time:F} / {maxTime:F}";
        }

        public void UpdateLazersCount(int count, int max)
        {
            LazerCountField.text = $"Количество лазера: {count} / {max}";
        }

        public void UpdateLazerAppendingCountdown(float time, float maxTime)
        {
            LazerGenerationCountdownField.text = $"Генерация лазера: {time:F} / {maxTime:F}";
        }

        public void UpdateVelocity(Vector2 velocity)
        {
            VelocityField.text = $"Скорость: {velocity}";
        }

        public void UpdatePosition(Vector2 position)
        {
            PositionField.text = $"Позиция: {position}";
        }

        public void UpdateScore(int score)
        {
            ScoreField.text = $"Количество очков: {score}";
            TotalScoreField.text = ScoreField.text;
        }
    }
}