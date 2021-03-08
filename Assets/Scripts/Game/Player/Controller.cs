using UnityEngine;

namespace Game.Player
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float accelerationSpeed;

        [SerializeField] private float defaultBodyRotation;
        [SerializeField] private float maxBodyRotation;

        [SerializeField] private float bodyHeight;
        [SerializeField] private float bodyHeightRange;

        [SerializeField] private GameObject body;

        [SerializeField] private Rigidbody2D rb;

        private float _acceleration = 0f;

        private void Move(float direction)
        {
            float progress = Time.deltaTime * accelerationSpeed;
            _acceleration = Mathf.Lerp(_acceleration, Mathf.Sign(direction) * speed, progress);
        }

        private void RotateBody()
        {
            float rot = defaultBodyRotation + (_acceleration / speed) * maxBodyRotation;
            body.transform.rotation = Quaternion.Euler(0f, 0f, rot);
        }

        private void Update()
        {
            float bodyX = body.transform.position.x;
            body.transform.position = new Vector2(bodyX, bodyHeight + (Mathf.Sin(Time.time * 10f) * bodyHeightRange / 2));

            var horizontal = Input.GetAxisRaw("Horizontal");
            if (horizontal != 0f)
            {
                Move(horizontal);
            }
            else
            {
                float progress = Time.deltaTime * accelerationSpeed;
                _acceleration = Mathf.Lerp(_acceleration, 0, progress);
            }

            rb.velocity = Vector2.right * _acceleration;
            RotateBody();
        }
    }
}
