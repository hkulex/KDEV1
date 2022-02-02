namespace AlternateReality.Interfaces
{
    public interface IHittable
    {
        void Hit(int damage);
        void Die();
    }
}