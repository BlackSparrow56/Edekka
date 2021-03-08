using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class SinusoidalHint : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float frequency;

        private void Update()
        {
            float sin = Mathf.Sin(Time.time * frequency);
            float alpha = (sin + 1) / 2;

            text.alpha = alpha;
        }
    }
}
