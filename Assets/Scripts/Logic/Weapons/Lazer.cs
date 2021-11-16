namespace Asteroids.Logic
{
    public class Lazer : EntityBase
    {
        private float _timeFade = 0.5f;
        
        public override void Update(GameManager gameManager)
        {
            _timeFade -= gameManager.GameWindow.GetTimeStep();
            if (_timeFade <= 0)
            {
                CanBeDeleted = true;
            }
        }
    }
}