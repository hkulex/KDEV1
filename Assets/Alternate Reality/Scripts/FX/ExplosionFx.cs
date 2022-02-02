using System.Collections;
using AlternateReality.Interfaces;
using AlternateReality.Management;
using UnityEngine;

namespace AlternateReality.FX
{
    public class ExplosionFx : MonoBehaviour, IPoolable
    {
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void Spawn()
        {
            _particleSystem.Play();
            
            StartCoroutine(OnComplete());
        }

        private IEnumerator OnComplete()
        {
            yield return new WaitForSeconds(2f);
            
            PoolManagement.Instance.Remove(gameObject);
        }

        public void Remove()
        {
            _particleSystem.Clear();
        }
    }
}