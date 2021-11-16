using System;
using Asteroids.Logic;
using UnityEngine;

namespace Asteroids.View
{
    /// <summary>
    /// Менеджер игры.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public GameWindow GameWindow;
        public GameUI GameUI;
        
        private Logic.GameManager _gameManager;

        public GameEntity PlayerPrefab;
        public GameEntity BigMeteorPrefab;
        public GameEntity MiddleMeteorPrefab;
        public GameEntity SmallMeteorPrefab;
        public GameEntity BulletPrefab;
        public GameEntity LazerPrefab;
        public GameEntity UfoPrefab;

        public GameObject MainMenuPanel;
        public GameObject GameOverPanel;
        public GameObject InGameUI;
        
        private void Start()
        {
            _gameManager = new Logic.GameManager(GameWindow, GameUI);
            _gameManager.SetPlayerSpawningFunction(SpawnPlayer);
            _gameManager.SetUfoSpawningFunction(SpawnUfo);
            _gameManager.SetMeteorSpawningFunction(SpawnMeteor);
            _gameManager.SetBulletSpawningFunction(SpawnBullet);
            _gameManager.SetLazerSpawningFunction(SpawnLazer);
            _gameManager.OnGameOver += OnGameOver;
        }

        private void OnGameOver()
        {
            GameOverPanel.SetActive(true);
        }

        public void Restart()
        {
            MainMenuPanel.SetActive(false);
            GameOverPanel.SetActive(false);
            InGameUI.SetActive(true);
            _gameManager.Restart();
        }
        
        private IGameEntity SpawnUfo()
        {
            return Instantiate(UfoPrefab, GameWindow.transform);
        }

        private IGameEntity SpawnLazer()
        {
            return Instantiate(LazerPrefab, GameWindow.transform);
        }

        private IGameEntity SpawnBullet()
        {
            return Instantiate(BulletPrefab, GameWindow.transform);
        }

        private void Update()
        {
            _gameManager.Update();
        }

        private IGameEntity SpawnPlayer()
        {
            return Instantiate(PlayerPrefab, GameWindow.transform);
        }

        public void ProcessMoveData(Vector2 delta)
        {
            _gameManager.ProcessMovementData(delta);
        }

        public void ProcessPrimaryFireClick(bool state)
        {
            _gameManager.SetPlayerPrimaryShootingState(state);
        }

        public void ProcessSecondaryFireClick(bool state)
        {
            _gameManager.SetPlayerSecondaryShootingState(state);
        }

        private IGameEntity SpawnMeteor(MeteorType meteorType)
        {
            switch (meteorType)
            {
                case MeteorType.Big: return Instantiate(BigMeteorPrefab, GameWindow.transform);
                case MeteorType.Middle: return Instantiate(MiddleMeteorPrefab, GameWindow.transform);
                case MeteorType.Small: return Instantiate(SmallMeteorPrefab, GameWindow.transform);
            }

            return null;
        }

        private void OnDestroy()
        {
            _gameManager.Clear();
            _gameManager = null;
        }
    }
}