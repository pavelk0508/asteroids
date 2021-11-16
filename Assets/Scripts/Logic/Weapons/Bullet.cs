using UnityEngine;

namespace Asteroids.Logic
{
    public class Bullet : EntityBase
    {
        public override bool IsCanBeDeletedWhenOffscreen()
        {
            return true;
        }

        public override void Update(GameManager gameManager)
        {
            var horizontal = gameManager.GameWindow.GetHorizontalAxis();
            var vertical = gameManager.GameWindow.GetVerticalAxis();
            var normal = Vector3.Cross(horizontal, vertical);
            Angle = Vector3.SignedAngle(horizontal, Velocity, normal);
        }
    }
}