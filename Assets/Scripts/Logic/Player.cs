using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Класс, описывающий игрока.
    /// </summary>
    public class Player : EntityBase
    {
        /// <summary>
        /// Коэффициент влияние двигателя на траекторию движения.
        /// </summary>
        public float VelocityModifier = 1000f;

        /// <summary>
        /// Время отката лазера.
        /// </summary>
        public readonly float LazerCountdown = 1f;
        
        /// <summary>
        /// Текущее время отката лазера.
        /// </summary>
        public float CurrentLazerCountdown = 1f;

        /// <summary>
        /// Текущее время отката обычного выстрела.
        /// </summary>
        private float _bulletShootTime = 0f;
        
        /// <summary>
        /// Время отката обычного выстрела
        /// </summary>
        public readonly float PeriodBulletShoot = 0.25f;

        /// <summary>
        /// Уголовая скорость (град./сек) при нажатии на управление поворотом.
        /// </summary>
        private float _angularVelocity = 180f;

        /// <summary>
        /// Значение ввода (x-горизонтальная ось, y-вертикальная).
        /// </summary>
        private Vector2 _inputMovement;

        /// <summary>
        /// Производится ли сейчас выстрел обычными пулями.
        /// </summary>
        public bool PrimaryShooting = false;
        
        /// <summary>
        /// Производится ли сейчас выстрел из лазера.
        /// </summary>
        public bool SecondaryShooting = false;

        /// <summary>
        /// Максимальное количество лазера.
        /// </summary>
        public readonly int MaxLazerCount = 3;

        /// <summary>
        /// Текущее количество лазера.
        /// </summary>
        public int LazerCount { get; private set; } = 3;

        /// <summary>
        /// Время регенерации лазера.
        /// </summary>
        public readonly float LazerGenerationCountDown = 10f;
        
        /// <summary>
        /// Текущее время регенерации лазера.
        /// </summary>
        public float CurrentLazerGenerationCountDown { get; private set; }

        /// <summary>
        /// Максимальная скорость движения от двигателя.
        /// </summary>
        public readonly float MaxVelocity = 200f;
        
        /// <summary>
        /// Жив ли игрок?
        /// </summary>
        public bool IsDead = false;

        /// <summary>
        /// Обновление всех систем игрока.
        /// </summary>
        /// <param name="gameManager">Игровой менеджер.</param>
        public override void Update(GameManager gameManager)
        {
            var normal = Vector3.Cross(gameManager.GameWindow.GetHorizontalAxis(), gameManager.GameWindow.GetVerticalAxis());
            Angle = Angle - _inputMovement.x * _angularVelocity * gameManager.GameWindow.GetTimeStep();
            var quaternion = Quaternion.AngleAxis(Angle, normal);
            var direction = (Vector2) (quaternion * Vector2.right);
            Velocity = Vector2.ClampMagnitude(Velocity + direction * _inputMovement.y * VelocityModifier * gameManager.GameWindow.GetTimeStep(), MaxVelocity);

            if (_inputMovement.y < 0.5f)
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

        /// <summary>
        /// Проверка возможности выстрела обычными пулями.
        /// </summary>
        /// <param name="gameManager">Игровой менеджер.</param>
        /// <param name="direction">Направление выстрела.</param>
        private void ProcessPrimaryShooting(GameManager gameManager, Vector2 direction)
        {
            if (_bulletShootTime > PeriodBulletShoot)
            {
                if (PrimaryShooting)
                {
                    var bulletInstance = gameManager.AddBullet(Position, direction.normalized * 1000f);
                    bulletInstance.Angle = Angle;
                    _bulletShootTime = 0f;
                }
            }
            else
            {
                _bulletShootTime += gameManager.GameWindow.GetTimeStep();
            }
        }

        /// <summary>
        /// Проверка возможности выстрела из лазера.
        /// </summary>
        /// <param name="gameManager">Игровой менеджер.</param>
        private void ProcessSecondaryShooting(GameManager gameManager)
        {
            if (CurrentLazerGenerationCountDown < LazerGenerationCountDown)
            {
                CurrentLazerGenerationCountDown += gameManager.GameWindow.GetTimeStep();
            }
            else
            {
                CurrentLazerGenerationCountDown = LazerGenerationCountDown;
                if (LazerCount < MaxLazerCount)
                {
                    LazerCount++;
                    CurrentLazerGenerationCountDown = 0f;
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

        /// <summary>
        /// Проверка столкновения с противниками.
        /// </summary>
        /// <param name="entityBase">Противник.</param>
        protected override void OnCollision(EntityBase entityBase)
        {
            if (entityBase is EnemyBase)
            {
                IsDead = true;
            }
        }

        /// <summary>
        /// Передача ввода пользователя в игрока.
        /// </summary>
        /// <param name="input">Направление движения.</param>
        public void ProcessMoveInput(Vector2 input)
        {
            _inputMovement.x = input.x;
            _inputMovement.y = Mathf.Clamp01(input.y);
        }
    }
}