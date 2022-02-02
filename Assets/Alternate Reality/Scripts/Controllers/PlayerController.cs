using System.Collections;
using AlternateReality.Management;
using AlternateReality.Projectile;
using AlternateReality.View;
using UnityEngine;

namespace AlternateReality.Controllers
{
    [RequireComponent(typeof(Animator))]
    public class PlayerController : BaseController
    {
        private Animator _animator;
        private bool _isSwinging;
        private bool _isAbleToHit;

        protected override void Start()
        {
            _animator = GetComponent<Animator>();

            velocity = new Vector3();
            speed = 6f;
            
            _isSwinging = false;
            _isAbleToHit = true;
        }


        protected override void Movement()
        {
            if (ViewManagement.Instance.ActiveView.name != Views.GAME_VIEW)
            {
                return;
            }
            
            velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")) * speed;
            
            Vector3 position = transform.position + velocity * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, -3.5f, 3.5f);
            position.z = Mathf.Clamp(position.z, -5f, 0f);

            transform.position = position;

            if (_isSwinging)
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _isSwinging = true;
                _animator.SetTrigger("Swing Left");

                StartCoroutine(Simulate(-140f, 10f, 0.4f, 0.25f, 1));
            } 
            else if (Input.GetMouseButtonDown(1))
            {
                _isSwinging = true;
                _animator.SetTrigger("Swing Right");

                StartCoroutine(Simulate(140f, -10f, 0.4f, 0.25f, -1));
            }
        }

        // simulate the swing with rays
        private IEnumerator Simulate(float from, float to, float duration, float delay = 0f, int dir = 0)
        {
            yield return new WaitForSeconds(delay);
            
            float time = 0;

            while (time < duration)
            {
                float angle = Mathf.Lerp(from, to, time / duration);
                Vector3 positition = transform.TransformDirection(Vector3.forward);
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
                Vector3 direction = rotation * positition;

                if (_isAbleToHit)
                {
                    if (Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), direction, out RaycastHit hit, 2f, 1 << 8))
                    {
                        BaseProjectile bp = hit.transform.GetComponent<BaseProjectile>();

                        if (bp)
                        {
                            _isAbleToHit = false;
                            
                            Debug.Log("hit ball at local angle: " + angle);
                            bp.SetDirection(angle, dir); 
                        }
                    }
                }
                
                Debug.DrawRay(transform.position + new Vector3(0f, 0.5f, 0f), direction * 2f, Color.magenta);
                
                time += Time.deltaTime;

                yield return null;
            }
            
            _isSwinging = false;
            _isAbleToHit = true;
        }


        protected override void Animate()
        {
            _animator.SetFloat("VelocityX", velocity.x);
            _animator.SetFloat("VelocityZ", velocity.z);
        }
    }
}