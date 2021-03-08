using UnityEngine;

namespace Game.Player
{
    public class Logic : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        [SerializeField] private GameObject camp;
        [SerializeField] private GameObject mine;

        [SerializeField] private Camera campCamera;
        [SerializeField] private Camera mineCamera;

        private Vector3 _defaultPosition;
        private bool _atCamp = true;

        private void Start()
        {
            _defaultPosition = player.transform.position;
        }

        private void OnBecameInvisible()
        {
            _atCamp = !_atCamp;

            if (_atCamp) ToCamp();
            else ToMine();
        }

        private void ToCamp()
        {
            player.transform.SetParent(null);

            mine.SetActive(false);
            camp.SetActive(true);

            player.transform.SetParent(camp.transform);
            player.transform.position = 
                campCamera.transform.position + _defaultPosition;
        }

        private void ToMine()
        {
            player.transform.SetParent(null);

            camp.SetActive(false);
            mine.SetActive(true);

            player.transform.SetParent(mine.transform);
            player.transform.position = 
                mineCamera.transform.position + _defaultPosition;
        }
    }
}