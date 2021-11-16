using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Объект в игровой сцене. 
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Угол поворота.
        /// </summary>
        private float _angle;

        /// <summary>
        /// Позиция.
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// Скорость.
        /// </summary>
        private Vector2 _velocity;

        /// <summary>
        /// Соотносимый объект в реализации.
        /// </summary>
        private IGameEntity _gameEntity;

        /// <summary>
        /// Позиция.
        /// </summary>
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                _gameEntity.UpdatePosition(_position);
            }
        }

        /// <summary>
        /// Скорость.
        /// </summary>
        public Vector2 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        /// <summary>
        /// Угол поворота объекта.
        /// </summary>
        public float Angle
        {
            get => _angle;
            set
            {
                _angle = value % 360f;
                _gameEntity.UpdateRotation(_angle);
            }
        }

        /// <summary>
        /// Может ли быть удален. Данное поле отражает флаг удаления в следующем кадре.
        /// </summary>
        public bool CanBeDeleted = false;

        /// <summary>
        /// Инициализация объекта.
        /// </summary>
        protected EntityBase()
        {
        }

        /// <summary>
        /// Настройка объекта и соотношение с игровым объектом.
        /// </summary>
        /// <param name="gameEntity">Игровой объект в реализации.</param>
        public void Setup(IGameEntity gameEntity)
        {
            _gameEntity = gameEntity;
            _gameEntity.EntityFunc = () => this;
            _gameEntity.OnCollision += OnCollision;
        }

        /// <summary>
        /// Метод, вызываемый при обновлении состояния.
        /// </summary>
        /// <param name="gameManager">Менеджер игры.</param>
        public virtual void Update(GameManager gameManager)
        {
        }

        /// <summary>
        /// Метод, вызываемый при столкновении с другим объектом в реализации.
        /// </summary>
        /// <param name="entity">Объект в реализации.</param>
        private void OnCollision(IGameEntity entity)
        {
            OnCollision(entity.EntityFunc?.Invoke());
        }

        /// <summary>
        /// Метод, вызываемый при столкновении с другим объектом в логике.
        /// </summary>
        /// <param name="entityBase">Логическое представление объекта.</param>
        protected virtual void OnCollision(EntityBase entityBase)
        {
        }

        /// <summary>
        /// Может ли быть удален, если попадает за пределы экрана.
        /// </summary>
        public virtual bool IsCanBeDeletedWhenOffscreen()
        {
            return false;
        }

        /// <summary>
        /// Мгновенное удаление упоминаний объекта.
        /// </summary>
        public void ForceClear()
        {
            _gameEntity?.Clear();
        }

        /// <summary>
        /// Затирание и удаление из списка объектов.
        /// </summary>
        ~EntityBase()
        {
            ForceClear();
        }
    }
}