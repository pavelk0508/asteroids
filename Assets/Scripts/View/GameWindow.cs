using Asteroids.Logic;
using UnityEngine;

namespace Asteroids.View
{
    /// <summary>
    /// Игровое окно.
    /// </summary>
    public class GameWindow : MonoBehaviour, IGameWindow
    {
        /// <summary>
        /// Активный канвас.
        /// </summary>
        private Canvas _canvas;
        
        /// <summary>
        /// Границы активного канваса.
        /// </summary>
        private RectTransform _canvasRect;

        /// <summary>
        /// Отступ по вертикали.
        /// </summary>
        public float VerticalOffscreenLimit = 40f;

        /// <summary>
        /// Отступ по горизонтали.
        /// </summary>
        public float HorizontalOffscreenLimit = 40f;

        /// <summary>
        /// Инициализация системы.
        /// </summary>
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvasRect = _canvas.GetComponent<RectTransform>();
        }
        
        /// <summary>
        /// Получение времени кадра.
        /// </summary>
        /// <returns>Время кадра.</returns>
        public float GetTimeStep()
        {
            return Time.deltaTime;
        }

        /// <summary>
        /// Проверка - содержит ли окно данную точку.
        /// </summary>
        /// <param name="position">Позиция точки.</param>
        /// <returns>Истина, если содержит.</returns>
        public bool IsContains(Vector3 position)
        {
            return _canvasRect.rect.Contains(position);
        }

        /// <summary>
        /// Возвращает новую точку, которая при переходе за границы окна попадает на противоположную сторону.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <returns>Координаты точки.</returns>
        public Vector3 RepeatPoint(Vector3 position)
        {
            if (position.x < _canvasRect.rect.x - HorizontalOffscreenLimit)
            {
                position.x += _canvasRect.rect.width + HorizontalOffscreenLimit * 2f;
            }
            if (position.y < _canvasRect.rect.y - VerticalOffscreenLimit)
            {
                position.y += _canvasRect.rect.height + VerticalOffscreenLimit * 2f;
            }
            if (position.x > _canvasRect.rect.x + _canvasRect.rect.width + HorizontalOffscreenLimit)
            {
                position.x -= _canvasRect.rect.width + HorizontalOffscreenLimit * 2f;
            }
            if (position.y > _canvasRect.rect.y + _canvasRect.rect.height + VerticalOffscreenLimit)
            {
                position.y -= _canvasRect.rect.height + VerticalOffscreenLimit * 2f;
            }

            return position;
        }

        /// <summary>
        /// Получение границ игрового окна.
        /// </summary>
        /// <returns>Размер и зона игрового окна.</returns>
        public Rect GetRect()
        {
            return _canvasRect.rect;
        }

        /// <summary>
        /// Получение горизонтальной оси.
        /// </summary>
        /// <returns>Вектор.</returns>
        public Vector2 GetHorizontalAxis()
        {
            return transform.rotation * Vector3.right;
        }

        /// <summary>
        /// Получение вертикальной оси.
        /// </summary>
        /// <returns>Вектор.</returns>
        public Vector2 GetVerticalAxis()
        {
            return transform.rotation * Vector3.up;
        }
    }
}