using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.Decor
{
    public class SleepingEffect : MonoBehaviour
    {
        [SerializeField] private int lettersCount;
        [SerializeField] private float duration;
        [SerializeField] private Vector2 direction;
        [SerializeField] private AnimationCurve sizeCurve;

        [SerializeField] private GameObject letterPrefab;

        [SerializeField] private Canvas canvas;

        private List<TMP_Text> _letters = new List<TMP_Text>();

        private void Init()
        {
            Clear();

            for (int i = 0; i < lettersCount; i++)
            {
                var letter = Instantiate(letterPrefab, canvas.transform);
                var component = letter.GetComponent<TMP_Text>();

                _letters.Add(component);
            }
        }

        private void Clear()
        {
            foreach (var letter in _letters)
            {
                Destroy(letter.gameObject);
            }

            _letters.Clear();
        }

        private void Update()
        {
            if (lettersCount != _letters.Count)
            {
                Init();
            }

            for (int i = 0; i < _letters.Count; i++)
            {
                var letter = _letters[i];

                var progress = (Time.time % duration / duration) + ((float) i / _letters.Count);
                if (progress > 1)
                {
                    progress -= 1f;
                }

                letter.transform.localPosition = Vector2.Lerp(Vector2.zero, direction, progress);
                letter.transform.localScale = Vector2.one * sizeCurve.Evaluate(progress);
                letter.alpha = 1f - progress;
            }
        }
    }
}
