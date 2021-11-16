using System;
using Asteroids.Logic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.View
{
    public class InputController : MonoBehaviour
    {
        public GameManager Manager;
        
        public void ProcessMovement(InputAction.CallbackContext context)
        {
            var movement = context.ReadValue<Vector2>();
            Manager.ProcessMoveData(movement);
        }

        public void ProcessPrimaryFire(InputAction.CallbackContext context)
        {
            var state = context.ReadValue<float>();
            Manager.ProcessPrimaryFireClick(state > 0.5f);
        }
        
        public void ProcessSecondaryFire(InputAction.CallbackContext context)
        {
            var state = context.ReadValue<float>();
            Manager.ProcessSecondaryFireClick(state > 0.5f);
        }
    }
}