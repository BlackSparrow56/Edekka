using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class Hint : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private float _multiplier;

        public void ShowText(string text, float duration)
        {
            this.text.text = text;
            this.text.alpha = 1f;

            _multiplier = 1f / duration;
        }

        private void Update()
        {
            float progress = Time.deltaTime * _multiplier;
            float alpha = Mathf.Lerp(text.alpha, 0f, progress);
            text.alpha = alpha;
        }
    }
}
