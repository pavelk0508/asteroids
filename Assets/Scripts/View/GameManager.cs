using Asteroids.Logic;
using UnityEngine;

namespace Asteroids.View
{
    /// <summary>
    /// Менеджер игры.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Окно игры.
        /// </summary>
        public GameWindow GameWindow;

        /// <summary>
        /// Игровой интерфейс.
        /// </summary>
        public GameUI GameUI;

        /// <summary>
        /// Менеджер игры-обработчик всех поведений.
        /// </summary>
        private Logic.GameManager _gameManager;

        /// <summary>
        /// Префаб игрока.
        /// </summary>
        public GameEntity PlayerPrefab;

        /// <summary>
        /// Префаб большого метеорита.
        /// </summary>
        public GameEntity BigMeteorPrefab;

        /// <summary>
        /// Префаб среднего метеорита.
        /// </summary>
        public GameEntity MiddleMeteorPrefab;

        /// <summary>
        /// Префаб маленького метеорита.
        /// </summary>
        public GameEntity SmallMeteorPrefab;

        /// <summary>
        /// Префаб обычной пули игрока.
        /// </summary>
        public GameEntity BulletPrefab;

        /// <summary>
        /// Префаб лазера.
        /// </summary>
        public GameEntity LaserPrefab;

        /// <summary>
        /// Префаб летающей тарелки.
        /// </summary>
        public GameEntity UfoPrefab;

        /// <summary>
        /// Панель главного меню.
        /// </summary>
        public GameObject MainMenuPanel;

        /// <summary>
        /// Панель окончания игры.
        /// </summary>
        public GameObject GameOverPanel;

        /// <summary>
        /// Панель внутриигрового UI.
        /// </summary>
        public GameObject InGameUI;

        /// <summary>
        /// Инициализация менеджера.
        /// </summary>
        private void Start()
        {
            _gameManager = new Logic.GameManager(GameWindow, GameUI)
            {
                PlayerSpawnFunc = SpawnPlayer,
                UfoSpawnFunc = SpawnUfo,
                MeteorSpawnFunc = SpawnMeteor,
                BulletSpawnFunc = SpawnBullet,
                LaserSpawnFunc = SpawnLaser
            };
            
            _gameManager.OnGameOver += OnGameOver;
        }

        /// <summary>
        /// Метод, который вызывается при смерти игрока.
        /// </summary>
        private void OnGameOver()
        {
            GameOverPanel.SetActive(true);
        }

        /// <summary>
        /// Перезапуск игры.
        /// </summary>
        public void Restart()
        {
            MainMenuPanel.SetActive(false);
            GameOverPanel.SetActive(false);
            InGameUI.SetActive(true);
            _gameManager.Restart();
        }

        /// <summary>
        /// Создание летающей тарелки.
        /// </summary>
        /// <returns>Экземпляр летающей тарелки.</returns>
        private IGameEntity SpawnUfo()
        {
            return Instantiate(UfoPrefab, GameWindow.transform);
        }

        /// <summary>
        /// Создание лазера.
        /// </summary>
        /// <returns>Экземпляр лазера.</returns>
        private IGameEntity SpawnLaser()
        {
            return Instantiate(LaserPrefab, GameWindow.transform);
        }

        /// <summary>
        /// Создание пули.
        /// </summary>
        /// <returns>Экземпляр пули.</returns>
        private IGameEntity SpawnBullet()
        {
            return Instantiate(BulletPrefab, GameWindow.transform);
        }

        /// <summary>
        /// Создание игрока.
        /// </summary>
        /// <returns>Экземпляр игрока.</returns>
        private IGameEntity SpawnPlayer()
        {
            return Instantiate(PlayerPrefab, GameWindow.transform);
        }

        /// <summary>
        /// Создание экземпляра метеорита.
        /// </summary>
        /// <param name="meteorType">Тип метеорита.</param>
        /// <returns>Экземпляр метеорита.</returns>
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

        /// <summary>
        /// Обновление состояния системы.
        /// </summary>
        private void Update()
        {
            _gameManager.Update();
        }

        /// <summary>
        /// Передача вектора движения игроку.
        /// </summary>
        /// <param name="delta">Дельта движения по нажатым клавишам.</param>
        public void ProcessMoveData(Vector2 delta)
        {
            _gameManager.ProcessMovementData(delta);
        }

        /// <summary>
        /// Задать стрельбу обычными пулями.
        /// </summary>
        /// <param name="state">Истина, если стрельбу нужно разрешить.</param>
        public void ProcessPrimaryFireClick(bool state)
        {
            _gameManager.SetPlayerPrimaryShootingState(state);
        }

        /// <summary>
        /// Задать стрельбу из лазера.
        /// </summary>
        /// <param name="state">Истина, если стрельбу нужно разрешить.</param>
        public void ProcessSecondaryFireClick(bool state)
        {
            _gameManager.SetPlayerSecondaryShootingState(state);
        }

        /// <summary>
        /// Очистка памяти после окончания игры.
        /// </summary>
        private void OnDestroy()
        {
            _gameManager.Clear();
            _gameManager = null;
        }
    }
}