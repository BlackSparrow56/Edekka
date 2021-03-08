using UnityEngine;

namespace Game.Player
{
    public class Rubber : MonoBehaviour
    {
        [SerializeField] private float elasticCoefficient;

        [SerializeField] private GameObject body;

        [SerializeField] private GameObject head;
        [SerializeField] private Vector2 headOffset;

        [SerializeField] private Arm leftArm;
        [SerializeField] private Vector2 leftArmOffset;

        [SerializeField] private Arm rightArm;
        [SerializeField] private Vector2 rightArmOffset;

        private void OnValidate()
        {
            head.transform.position = body.transform.position + body.transform.TransformVector(headOffset);

            leftArm.Anchor = body.transform.position + body.transform.TransformVector(leftArmOffset);
            rightArm.Anchor = body.transform.position + body.transform.TransformVector(rightArmOffset);

            leftArm.transform.position = body.transform.position + body.transform.TransformVector(leftArmOffset);
            rightArm.transform.position = body.transform.position + body.transform.TransformVector(rightArmOffset);
        }

        private void Update()
        {
            var headAnchor = body.transform.position + body.transform.TransformVector(headOffset);
            head.transform.position = headAnchor;

            var leftArmAnchor = body.transform.position + body.transform.TransformVector(leftArmOffset);
            leftArm.Anchor = leftArmAnchor;

            var rightArmAnchor = body.transform.position + body.transform.TransformVector(rightArmOffset);
            rightArm.Anchor = rightArmAnchor;
        }
    }
}
