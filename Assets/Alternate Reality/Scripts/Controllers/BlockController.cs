using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AlternateReality.Controllers
{
    public class BlockController : BaseController
    {
        private float _range;

        protected override void Start()
        {
            speed = Random.Range(0.1f, 1f);
            _range = Random.Range(0.1f, 0.5f);
        }

        protected override void Movement()
        {
            
        }

        protected override void Animate()
        {
            float y = Mathf.PingPong(Time.time * speed, 1f) * _range - (_range / 2);

            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
    }
}