using AlternateReality.Interfaces;
using AlternateReality.Management;
using UnityEngine;

namespace AlternateReality.Projectile
{
    public class BaseProjectile : MonoBehaviour, IPoolable
    {
        protected float speed;
        protected float radius;
        protected int accuracy;
        protected int damage;
        
        private float _increment;

        protected void Start()
        {
            speed = 0f;
            radius = 0.5f;
            accuracy = 10;
            damage = 1;

            _increment = 0.25f;
        }

        protected void Movement()
        {
            Vector3 next = transform.position + transform.forward * (Time.deltaTime * speed);
            float distance = radius / accuracy;

            for (int i = -accuracy; i <= accuracy; i++)
            {
                float height = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(distance * i, 2));
                
                Debug.DrawRay(next + transform.TransformDirection(new Vector3(distance * i, 0f, 0f)), transform.TransformDirection(Vector3.forward) * height, Color.red);
                
                if (Physics.Raycast(next + transform.TransformDirection(new Vector3(distance * i, 0f, 0f)),
                    transform.TransformDirection(Vector3.forward), out RaycastHit hit, height, 1 << 9))
                {
                    Vector3 direction = Vector3.Reflect(transform.TransformDirection(Vector3.forward), hit.normal);
                    float angle = Vector3.SignedAngle(transform.TransformDirection(Vector3.forward), direction, Vector3.up);

                    transform.Rotate(Vector3.up, angle);

                    if (hit.transform.gameObject.layer == 9)
                    {
                        IHittable iho = hit.transform.GetComponent<IHittable>();
                        
                        iho?.Hit(damage);
                    }

                    speed += _increment;

                    break;
                }
            }
            
            transform.position += transform.forward * (Time.deltaTime * speed);
            
            if (transform.position.z <= -4.75f)
            {
                PoolManagement.Instance.Spawn("ExplosionFx", transform.position);
                GameManagement.Instance.End();
                Destroy(gameObject);
            }
            
            DrawReflections(transform.position, transform.TransformDirection(Vector3.forward), 3);
        }


        protected void Update()
        {
            Movement();
        }


        public void SetDirection(float angle, int direction)
        {
            if (speed == 0f)
            {
                speed = 10f;
            }
            
            transform.rotation = Quaternion.Euler(0f, angle + 90f * direction, 0f);
        }

        private void DrawReflections(Vector3 from, Vector3 to, int reflections)
        {
            Vector3 start = from;
            Vector3 direction = to;
            
            for (int i = 0; i < reflections; i++)
            {
                if (Physics.Raycast(start, direction, out RaycastHit hit, Mathf.Infinity, 1 << 9))
                {
                    Debug.DrawLine(start, hit.point, i == 0 ? Color.green : Color.red);
                    
                    start = hit.point;
                    direction = Vector3.Reflect(direction, hit.normal);
                }
            }
        }

        public void Spawn()
        {
            
        }

        public void Remove()
        {
            
        }
    }
}
