using AlternateReality.Interfaces;
using AlternateReality.Management;
using AlternateReality.Utility;
using UnityEngine;

namespace AlternateReality.Blocks
{
    public class BaseBlock : MonoBehaviour, IHittable, IPoolable
    {
        protected int health;
        protected int score;
        protected Coordinate coordinate;

        public void SetPosition(int row, int column)
        {
            coordinate = new Coordinate(row, column);
        }

        public void Hit(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                EventManagement.HitBlockEvent(this, coordinate.Row, coordinate.Column);
            }
        }

        public void Die()
        {
            OnDie();
            
            PoolManagement.Instance.Spawn("ExplosionFx", transform.position);
            PoolManagement.Instance.Remove(gameObject);
        }

        protected virtual void OnDie() { }

        public void Spawn()
        {
            OnSpawn();
        }

        protected virtual void OnSpawn() { }

        public void Remove()
        {
            OnRemove();
        }

        protected virtual void OnRemove() { }
        
        public int Row => coordinate.Row;
        public int Column => coordinate.Column;
        public int Score => score;
    }
}