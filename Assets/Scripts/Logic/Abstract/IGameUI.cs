using UnityEngine;

namespace Asteroids.Logic
{
    public interface IGameUI
    {
        void UpdateLazerCounddown(float time, float maxTime);
        void UpdateLazersCount(int count, int max);
        void UpdateLazerAppendingCountdown(float time, float maxTime);
        void UpdateVelocity(Vector2 velocity);
        void UpdatePosition(Vector2 position);
        void UpdateScore(int score);
    }
}