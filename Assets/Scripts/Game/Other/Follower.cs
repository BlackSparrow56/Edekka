using UnityEngine;

namespace Game.Other
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private float speed;

        [SerializeField] private float maxX;

        [SerializeField] private bool y;
        [SerializeField] private bool z;

        private void Update()
        {
            float progress = Time.deltaTime * speed;
            Vector3 targetPosition = Vector3.Lerp(transform.position, target.transform.position, progress);
            Vector3 delta = targetPosition - transform.position;

            if (!y) delta.y = 0;
            if (!z) delta.z = 0;

            if ((transform.position + delta).x < maxX)
            {
                transform.Translate(delta);
            }
        }
    }
}