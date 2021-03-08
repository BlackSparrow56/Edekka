using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Final
{
    public class Camera : MonoBehaviour
    {
        [SerializeField] private GameObject rocket;
        [SerializeField] private float followSpeed;
        [SerializeField] private float shakingMultiplier;

        private Vector3 cameraAnchor;

        private void Start()
        {
            cameraAnchor = rocket.transform.position - Vector3.forward * 10f;
        }

        private void Update()
        {
            float progress = Time.deltaTime * followSpeed;
            var targetPosition = Vector3.Lerp(cameraAnchor, rocket.transform.position, progress);

            var delta = targetPosition - cameraAnchor;
            delta.x = 0;
            delta.z = 0;

            cameraAnchor = cameraAnchor + delta;

            float randomAngle = Random.Range(0f, 360f);
            float shake = Random.Range(0f, shakingMultiplier * delta.magnitude);

            var randomVector = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f);
            transform.position = cameraAnchor + randomVector * shake;
        }
    }
}