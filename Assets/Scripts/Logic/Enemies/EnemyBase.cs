namespace Asteroids.Logic
{
    /// <summary>
    /// Базовое описание любого врага.
    /// </summary>
    public abstract class EnemyBase : EntityBase
    {
        /// <summary>
        /// Количество необходимых выстрелов в метеорит для уничтожения.
        /// </summary>
        public int HP = 1;

        /// <summary>
        /// Количество очков за уничтожение
        /// </summary>
        public abstract int CountScore { get; }

        /// <summary>
        /// Были ли начислены очки за уничтожение.
        /// </summary>
        private bool _scoresHasBeenAdded = false;

        /// <summary>
        /// Проверка на уничтожение.
        /// </summary>
        /// <param name="gameManager">Игровой менеджер.</param>
        public override void Update(GameManager gameManager)
        {
            if (HP > 0)
            {
                return;
            }

            if (!_scoresHasBeenAdded)
            {
                _scoresHasBeenAdded = true;
                gameManager.Score += CountScore;
            }

            if (CanBeDeleted)
            {
                return;
            }

            CanBeDeleted = true;

            OnDead(gameManager);
        }

        /// <summary>
        /// Проверка столкновения с различными объектами.
        /// </summary>
        /// <param name="entityBase">Целевой объект.</param>
        protected override void OnCollision(EntityBase entityBase)
        {
            CheckCollisionWithBullet(entityBase);
            CheckCollisionWithLazer(entityBase);
        }

        /// <summary>
        /// Проверка столкновения с лазером.
        /// </summary>
        /// <param name="entityBase">Лазер.</param>
        private void CheckCollisionWithLazer(EntityBase entityBase)
        {
            if (entityBase is Lazer)
            {
                HP = 0;
                CanBeDeleted = true;
            }
        }

        /// <summary>
        /// Проверка столкновения с пулей.
        /// </summary>
        /// <param name="entityBase">Пуля.</param>
        private void CheckCollisionWithBullet(EntityBase entityBase)
        {
            if (entityBase is not Bullet bullet)
            {
                return;
            }

            HP--;
            bullet.CanBeDeleted = true;
        }

        /// <summary>
        /// При смерти.
        /// </summary>
        protected virtual void OnDead(GameManager gameManager)
        {
        }
    }
}