using System;
using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Интерфейс игрового объекта (для view).
    /// </summary>
    public interface IGameEntity
    {
        /// <summary>
        /// Функция, которая возвращает связанный объект из логики.
        /// </summary>
        Func<EntityBase> EntityFunc { get; set; }

        /// <summary>
        /// Делегат события столкновения.
        /// </summary>
        public delegate void CollisionEventHandler(IGameEntity gameEntity);

        /// <summary>
        /// Событие, вызываемое при столкновении.
        /// </summary>
        public event CollisionEventHandler OnCollision;
    
        /// <summary>
        /// Метод, вызываемый при обновлении позиции.
        /// </summary>
        /// <param name="position">Позиция.</param>
        void UpdatePosition(Vector3 position);
        
        /// <summary>
        /// Метод, вызываемый при обновлении поворота.
        /// </summary>
        /// <param name="angle">Угол.</param>
        void UpdateRotation(float angle);

        /// <summary>
        /// Метод, вызываемый при удалении объекта.
        /// </summary>
        void Clear();
    }
}