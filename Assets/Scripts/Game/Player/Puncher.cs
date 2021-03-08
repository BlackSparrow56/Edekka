using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Player
{
    public class Puncher : MonoBehaviour
    {
        [SerializeField] private Arm leftArm;
        [SerializeField] private Arm rightArm;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                leftArm.Punch();
            }
            if (Input.GetMouseButtonDown(1))
            {
                rightArm.Punch();
            }
        }
    }
}
