using System;
using Asteroids.Logic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.View
{
    /// <summary>
    /// Класс, реализующий работу с устройствами ввода.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        /// <summary>
        /// Игровой менеджер.
        /// </summary>
        public GameManager Manager;

        /// <summary>
        /// Передача перемещения.
        /// </summary>
        /// <param name="context">Контекст действия.</param>
        public void ProcessMovement(InputAction.CallbackContext context)
        {
            var movement = context.ReadValue<Vector2>();
            Manager.ProcessMoveData(movement);
        }

        /// <summary>
        /// Передача выстрела обычными пулями.
        /// </summary>
        /// <param name="context">Контекст действия.</param>
        public void ProcessPrimaryFire(InputAction.CallbackContext context)
        {
            var state = context.ReadValue<float>();
            Manager.ProcessPrimaryFireClick(state > 0.5f);
        }

        /// <summary>
        /// Передача выстрела лазером.
        /// </summary>
        /// <param name="context">Контекст действия.</param>
        public void ProcessSecondaryFire(InputAction.CallbackContext context)
        {
            var state = context.ReadValue<float>();
            Manager.ProcessSecondaryFireClick(state > 0.5f);
        }
    }
}