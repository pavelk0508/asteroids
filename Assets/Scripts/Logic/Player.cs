using UnityEngine;

namespace Asteroids.Logic
{
    public class Player : EntityBase
    {
        public float VelocityModifier = 1000f;

        public float LazerCountdown { get; private set; } = 1f;
        public float CurrentLazerCountdown = 1f;

        private float _bulletShootTime = 0f;
        private float _periodBulletShoot = 0.25f;

        private float _angleSensetivity = 180f;
        private float _inputVelocityX = 0f;
        private float _inputVelocityY = 0f;

        public bool PrimaryShooting = false;
        public bool SecondaryShooting = false;

        public int MaxLazerCount = 3;
        public int LazerCount = 3;

        public readonly float LazerAppendingCountDown = 10f;
        public float CurrentLazerAppendingCountDown { get; private set; }

        public float MaxVelocity = 200f;
        public bool IsDead = false;

        public override void Update(GameManager gameManager)
        {
            var normal = Vector3.Cross(gameManager.GameWindow.GetHorizontalAxis(), gameManager.GameWindow.GetVerticalAxis());
            Angle = Angle - _inputVelocityX * _angleSensetivity * gameManager.GameWindow.GetTimeStep();
            var quaternion = Quaternion.AngleAxis(Angle, normal);
            var direction = (Vector2) (quaternion * Vector2.right);
            Velocity = Vector2.ClampMagnitude(Velocity + direction * _inputVelocityY * VelocityModifier * gameManager.GameWindow.GetTimeStep(), MaxVelocity);

            if (_inputVelocityY < 0.5f)
            {
                Velocity = Vector2.Lerp(Velocity, Vector2.zero, Time.deltaTime);
            }

            ProcessPrimaryShooting(gameManager, direction);
            ProcessSecondaryShooting(gameManager);

            if (IsDead)
            {
                gameManager.IsPlaying = false;
                gameManager.InvokeGameOver();
            }
        }

        private void ProcessPrimaryShooting(GameManager gameManager, Vector2 direction)
        {
            if (_bulletShootTime > _periodBulletShoot)
            {
                if (PrimaryShooting)
                {
                    gameManager.AddBullet(Position, direction.normalized * 1000f);
                    _bulletShootTime = 0f;
                }
            }
            else
            {
                _bulletShootTime += gameManager.GameWindow.GetTimeStep();
            }
        }

        private void ProcessSecondaryShooting(GameManager gameManager)
        {
            if (CurrentLazerAppendingCountDown < LazerAppendingCountDown)
            {
                CurrentLazerAppendingCountDown += gameManager.GameWindow.GetTimeStep();
            }
            else
            {
                CurrentLazerAppendingCountDown = LazerAppendingCountDown;
                if (LazerCount < MaxLazerCount)
                {
                    LazerCount++;
                    CurrentLazerAppendingCountDown = 0f;
                }
            }
            
            if (CurrentLazerCountdown >= LazerCountdown)
            {
                CurrentLazerCountdown = LazerCountdown;
                if (SecondaryShooting && LazerCount > 0)
                {
                    gameManager.AddLazer(Position, Angle);
                    CurrentLazerCountdown = 0f;
                    LazerCount--;
                }
            }
            else
            {
                CurrentLazerCountdown += gameManager.GameWindow.GetTimeStep();
            }
        }

        protected override void OnCollision(EntityBase entityBase)
        {
            if (entityBase is EnemyBase)
            {
                IsDead = true;
            }
        }

        public void ProcessMoveInput(Vector2 input)
        {
            _inputVelocityX = input.x;
            _inputVelocityY = Mathf.Clamp01(input.y);
        }
    }
}