using System.Collections.Generic;
using UnityEngine;

namespace Game.Decor
{
    public class Robot : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float range;
        [SerializeField] private float threshold;

        [SerializeField] private GameObject player;
        [SerializeField] private List<GameObject> wheels;

        private void Update()
        {
            var targetPosition = (Vector2) player.transform.position - Vector2.right * range;
            var delta = transform.position.x - targetPosition.x;
            if (Mathf.Abs(delta) > threshold)
            {
                transform.position = transform.position + Vector3.left * Mathf.Sign(delta) * speed * Time.deltaTime;
                
                foreach (var wheel in wheels)
                {
                    wheel.transform.rotation *= Quaternion.Euler(0f, 0f, 120f * delta * Time.deltaTime);
                }
            }
        }
    }
}
