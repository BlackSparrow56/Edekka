using System;
using UnityEngine;
using Game.Player;

namespace Game.Cave
{
    public class Ledge : MonoBehaviour
    {
        public Action onLedgeDestroyed = () => { };

        private int _durability;

        public void Init(int durability)
        {
            _durability = durability;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var arm = collider.gameObject.GetComponent<Arm>();
            if (arm != null && arm.IsPunching)
            {
                arm.HitRock();
                _durability -= 1;
                if (_durability <= 0)
                {
                    onLedgeDestroyed.Invoke();
                    Destroy(gameObject);
                }
            }
        }
    }
}
