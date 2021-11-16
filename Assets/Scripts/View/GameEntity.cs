using Asteroids.Logic;
using UnityEngine;

namespace Asteroids.View
{
    /// <summary>
    /// Игровой объект реализации.
    /// </summary>
    public class GameEntity : MonoBehaviour, IGameEntity
    {
        /// <summary>
        /// Событие столкновения.
        /// </summary>
        public event IGameEntity.CollisionEventHandler OnCollision;

        /// <summary>
        /// Объект-контейнер границ объекта.
        /// </summary>
        protected RectTransform _rectTransform;

        /// <summary>
        /// Нужно ли плавное исчезновение.
        /// </summary>
        public bool NeedFade = true;
        
        /// <summary>
        /// Инициализация и получение границ окна.
        /// </summary>
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// Обновление позиции.
        /// </summary>
        /// <param name="position">Новая позиция.</param>
        public void UpdatePosition(Vector3 position)
        {
            _rectTransform.anchoredPosition = position;
        }

        /// <summary>
        /// Обновление поворота.
        /// </summary>
        /// <param name="angle">Новый угол поворота.</param>
        public void UpdateRotation(float angle)
        {
            _rectTransform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        /// <summary>
        /// Проверка столкновения с другим объектом.
        /// </summary>
        /// <param name="other">Другой объект.</param>
        private void OnTriggerEnter(Collider other)
        {
            var entity = other.GetComponent<GameEntity>();
            if (entity)
            {
                OnCollision?.Invoke(entity);
            }
        }

        /// <summary>
        /// Метод, вызываемый при уничтожении объекта.
        /// </summary>
        public void Clear()
        {
            if (this)
            {
                if (NeedFade)
                {
                    Fading();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// Уничтожение с анимацией.
        /// </summary>
        private void Fading()
        {
            var copy = Instantiate(gameObject, transform.parent);
            var fadeBehaviour = copy.AddComponent<FadeBehaviour>();
            fadeBehaviour.DestroyAfterFading = true;
            Destroy(gameObject);
        }
    }
}