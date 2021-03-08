using System;
using System.Collections;
using UnityEngine;

namespace Game.Player
{
    public class Arm : MonoBehaviour
    {
        [SerializeField] private float punchRange;
        [SerializeField] private float returnSpeed;

        [SerializeField] private AnimationCurve curve;

        [SerializeField] private AudioSource hitSource;

        private bool _isPunching;
        public bool IsPunching => _isPunching;

        public Action onRockHitted = () => { };

        public Vector2 Anchor
        {
            get;
            set;
        }

        public void Punch()
        {
            if (!_isPunching)
            {
                StartCoroutine(PunchCoroutine());
            }
        }

        public void SetReturnSpeed(float speed)
        {
            returnSpeed = speed;
        }

        public void HitRock()
        {
            hitSource.Play();
            onRockHitted.Invoke();
        }

        private void Update()
        {
            if (!_isPunching)
            {
                transform.position = Anchor;
            }
        }

        private IEnumerator PunchCoroutine()
        {
            _isPunching = true;

            var hit = Physics2D.Raycast(Anchor, Vector2.right, punchRange);

            Vector2 hitPoint;
            if (hit.distance != 0)
            {
                hitPoint = hit.point - Vector2.right * (transform.localScale.x / 2);
            }
            else
            {
                hitPoint = Anchor + Vector2.right * punchRange;
            }

            float time = 0;
            while (time < returnSpeed)
            {
                float progress = curve.Evaluate(time / returnSpeed);

                var position = Vector2.Lerp(Anchor, hitPoint, progress);
                transform.position = position;

                time += Time.deltaTime;
                yield return null;
            }

            _isPunching = false;
        }
    }
}
