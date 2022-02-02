using System;
using UnityEngine;

namespace AlternateReality.Controllers
{
    public abstract class BaseController : MonoBehaviour
    {
        protected Vector3 velocity;
        protected float speed;

        protected virtual void Update()
        {
            Movement();
            Animate();
        }
        protected abstract void Start();
        protected abstract void Movement();
        protected abstract void Animate();
        
    }
}