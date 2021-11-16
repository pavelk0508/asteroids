using Asteroids.Logic;
using UnityEngine;

namespace Asteroids.View
{
    public class GameWindow : MonoBehaviour, IGameWindow
    {
        private Canvas _canvas;
        private RectTransform _canvasRect;

        public float VerticalOffscreenLimit = 40f;
        public float HorizontalOffscreenLimit = 40f;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvasRect = _canvas.GetComponent<RectTransform>();
        }
        
        public float GetTimeStep()
        {
            return Time.deltaTime;
        }

        public bool IsContains(Vector3 position)
        {
            return _canvasRect.rect.Contains(position);
        }

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

        public Rect GetRect()
        {
            return _canvasRect.rect;
        }

        public Vector2 GetHorizontalAxis()
        {
            return transform.rotation * Vector3.right;
        }

        public Vector2 GetVerticalAxis()
        {
            return transform.rotation * Vector3.up;
        }
    }
}