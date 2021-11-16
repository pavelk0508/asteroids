using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Logic
{
    /// <summary>
    /// Основной менеджер игрового процесса.
    /// </summary>
    public sealed class GameManager
    {
        /// <summary>
        /// Делегат простого события.
        /// </summary>
        public delegate void SimpleEventHandler();

        /// <summary>
        /// В момент смерти игрока.
        /// </summary>
        public event SimpleEventHandler OnGameOver;

        /// <summary>
        /// Делегат функции создания объекта в реализации.
        /// </summary>
        public delegate IGameEntity SpawningFunction();

        /// <summary>
        /// Делегат функции создания метеорита с указанием его типа.
        /// </summary>
        public delegate IGameEntity MeteorSpawningFunction(MeteorType meteorType);

        /// <summary>
        /// Функция, которая вызывается при создании игрока для получения либо экземпляра, либо текущего объекта.
        /// </summary>
        private event SpawningFunction _playerSpawning;

        /// <summary>
        /// Функция, которая вызывается при создании объекта-пули для получения либо экземпляра, либо текущего объекта.
        /// </summary>
        private event SpawningFunction _bulletSpawning;

        /// <summary>
        /// Функция, которая вызывается при создании объекта-лазера для получения либо экземпляра, либо текущего объекта.
        /// </summary>
        private event SpawningFunction _lazerSpawning;

        /// <summary>
        /// Функция, которая вызывается при создании метеорита для получения либо экземпляра, либо текущего объекта.
        /// </summary>
        private event MeteorSpawningFunction _meteorSpawning;
        
        /// <summary>
        /// Функция, которая вызывается при создании летающей тарелки для получения либо экземпляра, либо текущего объекта.
        /// </summary>
        private event SpawningFunction _ufoSpawning;

        /// <summary>
        /// Список всех объектов в сцене.
        /// </summary>
        private List<EntityBase> _entities = new List<EntityBase>();

        /// <summary>
        /// Окно игрового процесса.
        /// </summary>
        public IGameWindow GameWindow { get; private set; }

        /// <summary>
        /// Игрок.
        /// </summary>
        private Player _player;

        /// <summary>
        /// Игрок.
        /// </summary>
        public Player Player => _player;

        /// <summary>
        /// UI.
        /// </summary>
        private IGameUI _gameUI;

        /// <summary>
        /// Количество метеоритов.
        /// </summary>
        private int _meteorsCount = 10;

        /// <summary>
        /// Количество очков.
        /// </summary>
        public int Score = 0;

        /// <summary>
        /// В игре ли?
        /// </summary>
        public bool IsPlaying = false;
        
        /// <summary>
        /// Количество метеоритов.
        /// </summary>
        public int MeteorsCount
        {
            get => _meteorsCount;
            set => _meteorsCount = Mathf.Clamp(value, 1, 100);
        }

        /// <summary>
        /// Количество метеоритов в сцене.
        /// </summary>
        public int CurrentMeteorsCount => _entities.FindAll(a => a is Meteor).Count;

        /// <summary>
        /// Время ожидания летающей тарелки.
        /// </summary>
        public float CountdownSpawnUfo = 10f;

        /// <summary>
        /// Текущее время ожидание летающей тарелки.
        /// </summary>
        private float _currentCountdownSpawnUfo = 0f;

        /// <summary>
        /// Инициализация окна с указанием объекта-окна из реализации.
        /// </summary>
        /// <param name="gameWindow">Объект-окно в реализации.</param>
        /// <param name="gameUI">Окно интерфейса.</param>
        public GameManager(IGameWindow gameWindow, IGameUI gameUI)
        {
            GameWindow = gameWindow;
            _gameUI = gameUI;
        }

        /// <summary>
        /// Старт.
        /// </summary>
        public void Play()
        {
            _player = AddEntity<Player>(_playerSpawning);
            IsPlaying = true;
        }

        /// <summary>
        /// Создание метеора и расположение его в окне.
        /// </summary>
        /// <param name="meteorInstance">Экземпляр объекта.</param>
        private void GenerateMeteor(IGameEntity meteorInstance)
        {
            var meteorEntity = AddEntity<Meteor>(() => meteorInstance);

            var rect = GameWindow.GetRect();

            var sides = new[]
            {
                new[] {rect.x, rect.x + rect.width, rect.y, rect.y},
                new[] {rect.x + rect.width, rect.x + rect.width, rect.y, rect.y + rect.height},
                new[] {rect.x, rect.x + rect.width, rect.y + rect.height, rect.y + rect.height},
                new[] {rect.x, rect.x, rect.y, rect.y + rect.height}
            };

            var selectedSide = sides[Random.Range(0, sides.Length)];

            var x = Mathf.Lerp(selectedSide[0], selectedSide[1], Random.Range(0f, 1f));
            var y = Mathf.Lerp(selectedSide[2], selectedSide[3], Random.Range(0f, 1f));

            meteorEntity.Position = new Vector3(x, y, 0);
            meteorEntity.Velocity = new Vector3(Random.Range(10f, 30f), Random.Range(10f, 30f));
        }

        /// <summary>
        /// Указание функции, вызываемой при создании экземпляра игрока.
        /// </summary>
        /// <param name="playerSpawningFunction">Функция создания или переиспользования игрока.</param>
        public void SetPlayerSpawningFunction(SpawningFunction playerSpawningFunction)
        {
            _playerSpawning = playerSpawningFunction;
        }

        /// <summary>
        /// Указание функции, вызываемой при создании экземпляра пули.
        /// </summary>
        /// <param name="bulletSpawningFunction">Функция создания или переиспользования пули.</param>
        public void SetBulletSpawningFunction(SpawningFunction bulletSpawningFunction)
        {
            _bulletSpawning = bulletSpawningFunction;
        }

        /// <summary>
        /// Указание функции, вызываемой при создании экземпляра пули.
        /// </summary>
        /// <param name="lazerSpawningFunction">Функция создания или переиспользования лазера.</param>
        public void SetLazerSpawningFunction(SpawningFunction lazerSpawningFunction)
        {
            _lazerSpawning = lazerSpawningFunction;
        }

        /// <summary>
        /// Указание функции, вызываемой при создании экземпляра метеорита.
        /// </summary>
        /// <param name="meteorSpawningFunction">Функция создания или переиспользования метеорита.</param>
        public void SetMeteorSpawningFunction(MeteorSpawningFunction meteorSpawningFunction)
        {
            _meteorSpawning = meteorSpawningFunction;
        }
        
        /// <summary>
        /// Указание функции, вызываемой при создании экземпляра летающей тарелки.
        /// </summary>
        /// <param name="ufoSpawningFunction">Функция создания или переиспользования летающей тарелки.</param>
        public void SetUfoSpawningFunction(SpawningFunction ufoSpawningFunction)
        {
            _ufoSpawning = ufoSpawningFunction;
        }

        /// <summary>
        /// Функция предназначенная для передачи вектора движения объекта. 
        /// </summary>
        /// <param name="movementVector">Вектор движения.</param>
        public void ProcessMovementData(Vector2 movementVector)
        {
            _player?.ProcessMoveInput(movementVector);
        }

        /// <summary>
        /// Задать состояние стрельбы игрока.
        /// </summary>
        /// <param name="value">Истина, если нужно стрелять.</param>
        public void SetPlayerPrimaryShootingState(bool value)
        {
            if (_player != null)
            {
                _player.PrimaryShooting = value;
            }
        }

        /// <summary>
        /// Задать состояние стрельбы игрока лазером.
        /// </summary>
        /// <param name="value">Истина, если нужно стрелять.</param>
        public void SetPlayerSecondaryShootingState(bool value)
        {
            if (_player != null)
            {
                _player.SecondaryShooting = value;
            }
        }

        /// <summary>
        /// Обновление сцены.
        /// </summary>
        public void Update()
        {
            if (!IsPlaying)
            {
                return;
            }
            
            var entities = _entities.ToArray();
            var factMeteorsCount = 0;
            var ufoExistInScene = false;
            
            foreach (var entity in entities)
            {
                entity.Update(this);
                var newPosition = entity.Position + entity.Velocity * GameWindow.GetTimeStep();
                if (entity.CanBeDeleted || !GameWindow.IsContains(newPosition) && entity.IsCanBeDeletedWhenOffscreen())
                {
                    DeleteEntity(entity);
                }
                else
                {
                    entity.Position = GameWindow.RepeatPoint(newPosition);
                }

                if (entity is Meteor)
                {
                    factMeteorsCount++;
                }

                if (entity is Ufo)
                {
                    ufoExistInScene = true;
                }
            }

            UpdateUI();
            UpdateMeteorsCount(factMeteorsCount);
            UpdateUfo(ufoExistInScene);
        }

        /// <summary>
        /// Отслеживание летающей тарелки
        /// </summary>
        /// <param name="isExist">Существует ли в сцене.</param>
        private void UpdateUfo(bool isExist)
        {
            if (isExist)
            {
                return;
            }

            _currentCountdownSpawnUfo += GameWindow.GetTimeStep();
            if (_currentCountdownSpawnUfo > CountdownSpawnUfo)
            {
                GenerateUfo();
                _currentCountdownSpawnUfo = 0f;
            }
        }

        /// <summary>
        /// Генерация летающей тарелки.
        /// </summary>
        private void GenerateUfo()
        {
            var rect = GameWindow.GetRect();
            var sides = new[]
            {
                new[] {rect.x + rect.width, rect.x + rect.width, rect.y, rect.y + rect.height / 2f},
                new[] {rect.x, rect.x, rect.y, rect.y + rect.height / 2f}
            };

            var selectedSide = sides[Random.Range(0, sides.Length)];

            var x = Mathf.Lerp(selectedSide[0], selectedSide[1], Random.Range(0f, 1f));
            var y = Mathf.Lerp(selectedSide[2], selectedSide[3], Random.Range(0f, 1f));

            AddUfo(new Vector2(x, y));
        }

        /// <summary>
        /// Добавление метеоритов.
        /// </summary>
        /// <param name="factCount">Фактическое количество метеоритов в сцене.</param>
        private void UpdateMeteorsCount(int factCount)
        {
            if (factCount < MeteorsCount)
            {
                var meteorInstance = _meteorSpawning?.Invoke(MeteorType.Big);
                GenerateMeteor(meteorInstance);
            }
        }
        
        /// <summary>
        /// Обновление интерфейса.
        /// </summary>
        private void UpdateUI()
        {
            _gameUI.UpdatePlayerPosition(_player.Position);
            _gameUI.UpdatePlayerVelocity(_player.Velocity);
            _gameUI.UpdatePlayerAngle(_player.Angle);
            _gameUI.UpdateLazerCounddown(_player.CurrentLazerCountdown, _player.LazerCountdown);
            _gameUI.UpdateLazerRegenerationCountdown(_player.CurrentLazerGenerationCountDown, _player.LazerGenerationCountDown);
            _gameUI.UpdateLazersCount(_player.LazerCount, _player.MaxLazerCount);
            _gameUI.UpdateScore(Score);
        }

        /// <summary>
        /// Добавление объекта в сцену.
        /// </summary>
        /// <param name="spawningFunction">Функция для создания экземпляра в реализации.</param>
        /// <typeparam name="T">Тип объекта.</typeparam>
        /// <returns>Экземпляр созданного объекта.</returns>
        public T AddEntity<T>(SpawningFunction spawningFunction) where T : EntityBase, new()
        {
            var entity = new T();
            entity.Setup(spawningFunction?.Invoke());
            
            _entities ??= new List<EntityBase>();
            _entities.Add(entity);
            return entity;
        }

        /// <summary>
        /// Создание экземпляра пули.
        /// </summary>
        /// <param name="position">Позиция пули.</param>
        /// <param name="direction">Направление пули.</param>
        /// <returns>Экземпляр пули.</returns>
        public Bullet AddBullet(Vector3 position, Vector3 direction)
        {
            var entity = AddEntity<Bullet>(_bulletSpawning);
            entity.Position = position;
            entity.Velocity = direction;
            return entity;
        }

        /// <summary>
        /// Создание экземпляра пули-лазера.
        /// </summary>
        /// <param name="position">Позиция пули.</param>
        /// <param name="direction">Направление пули.</param>
        /// <returns>Экземпляр пули.</returns>
        public Lazer AddLazer(Vector3 position, float angle)
        {
            var entity = AddEntity<Lazer>(_lazerSpawning);
            entity.Position = position;
            entity.Angle = angle;
            return entity;
        }

        /// <summary>
        /// Создание метеорита указанного типа в указанном месте. 
        /// </summary>
        /// <param name="type">Тип.</param>
        /// <param name="position">Позиция.</param>
        /// <param name="velocity">Скорость.</param>
        public Meteor AddMeteor(MeteorType type, Vector3 position, Vector3 velocity)
        {
            type = (MeteorType) Mathf.Clamp((int) type, (int) MeteorType.Big, (int) MeteorType.Small);
            var meteorInstance = _meteorSpawning?.Invoke(type);
            var meteorEntity = AddEntity<Meteor>(() => meteorInstance);
            meteorEntity.MeteorType = type;
            meteorEntity.Position = position;
            meteorEntity.Velocity = velocity;
            return meteorEntity;
        }

        /// <summary>
        /// Создание экземпляра летающей тарелки.
        /// </summary>
        /// <param name="position">Позиция создания.</param>
        /// <returns>Летающая тарелка.</returns>
        public Ufo AddUfo(Vector2 position)
        {
            var ufoInstance = _ufoSpawning?.Invoke();
            var ufo = AddEntity<Ufo>(() => ufoInstance);
            ufo.Position = position;
            return ufo;
        }

        /// <summary>
        /// Вызвать окончание игры.
        /// </summary>
        public void InvokeGameOver()
        {
            OnGameOver?.Invoke();
        }
        
        /// <summary>
        /// Сброс всех объектов.
        /// </summary>
        public void Clear()
        {
            _entities.ForEach(a => a.ForceClear());
            _entities.Clear();
            _entities = null;
        }

        /// <summary>
        /// Перезапуск.
        /// </summary>
        public void Restart()
        {
            Score = 0;
            _currentCountdownSpawnUfo = 0f;
            Clear();
            Play();
        }

        /// <summary>
        /// Удаление одного объекта.
        /// </summary>
        /// <param name="entity">Объект.</param>
        public void DeleteEntity(EntityBase entity)
        {
            entity.ForceClear();
            _entities.Remove(entity);
        }

        /// <summary>
        /// Уничтожение менеджера.
        /// </summary>
        ~GameManager()
        {
            Debug.Log("[Asteroids]. Game manager has been destroyed! ");
        }
    }
}