using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.View
{
    /// <summary>
    /// Немного косметики для исчезновения выстрела лазера.
    /// </summary>
    public class FadeBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Время исчезновения.
        /// </summary>
        public float TimeFade = 0.5f;

        /// <summary>
        /// Текущее время.
        /// </summary>
        private float _currentTimeFade = 0f;

        /// <summary>
        /// Целевые компоненты изменения альфа-канала.
        /// </summary>
        private Image[] _images;

        /// <summary>
        /// Удаление после скрытия.
        /// </summary>
        public bool DestroyAfterFading = false;

        /// <summary>
        /// Инициализация компонента.
        /// </summary>
        private void Awake()
        {
            _images = GetComponentsInChildren<Image>();
        }

        /// <summary>
        /// Установка альфа-канала в зависимости от текущего времени.
        /// </summary>
        private void Update()
        {
            if (!(_currentTimeFade < TimeFade))
            {
                if (DestroyAfterFading)
                {
                    Destroy(gameObject);
                }

                return;
            }

            _currentTimeFade += Time.deltaTime;
            foreach (var image in _images)
            {
                var color = image.color;
                color.a = 1f - (_currentTimeFade / TimeFade);
                image.color = color;
            }
        }
    }
}