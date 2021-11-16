namespace Asteroids.Logic
{
    /// <summary>
    /// Класс, описывающий пулю игрока.
    /// </summary>
    public class Bullet : EntityBase
    {
        /// <summary>
        /// Может ли быть уничтожен на границе экрана.
        /// </summary>
        /// <returns>Истина, если может.</returns>
        public override bool IsCanBeDeletedWhenOffscreen()
        {
            return true;
        }
    }
}