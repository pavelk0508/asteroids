using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Интерфейс игрового окна.
    /// </summary>
    public interface IGameWindow
    {
        /// <summary>
        /// Возвращает время кадра.
        /// </summary>
        /// <returns>Время кадра.</returns>
        float GetTimeStep();
        
        /// <summary>
        /// Проверка - содержит ли окно данную точку.
        /// </summary>
        /// <param name="position">Позиция точки.</param>
        /// <returns>Истина, если содержит.</returns>
        bool IsContains(Vector3 position);

        /// <summary>
        /// Возвращает новую точку, которая при переходе за границы окна попадает на противоположную сторону.
        /// </summary>
        /// <param name="position">Позиция.</param>
        /// <returns>Координаты точки.</returns>
        Vector3 RepeatPoint(Vector3 position);

        /// <summary>
        /// Получение границ игрового окна.
        /// </summary>
        /// <returns>Размер и зона игрового окна.</returns>
        Rect GetRect();

        /// <summary>
        /// Получение горизонтальной оси.
        /// </summary>
        /// <returns>Вектор.</returns>
        Vector2 GetHorizontalAxis();

        /// <summary>
        /// Получение вертикальной оси.
        /// </summary>
        /// <returns>Вектор.</returns>
        Vector2 GetVerticalAxis();
    }
}