using UnityEngine;

namespace Game.Final
{
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float width;
        [SerializeField] private float frequency;

        private void Update()
        {
            float x = Mathf.Sin(Time.time * frequency) * width / 2;
            float y = Time.time * speed;

            var targetPosition = new Vector2(x, y);
            var delta = targetPosition - (Vector2) transform.position;
            transform.position = targetPosition;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, delta);
        }
    }
}
