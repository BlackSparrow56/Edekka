using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.Final
{
    public class Slides : MonoBehaviour
    {
        [SerializeField] private float messageDuration;
        [SerializeField] private float interval;

        [SerializeField] private List<string> messages;

        [SerializeField] private TMP_Text text;

        private void Start()
        {
            StartCoroutine(ShowCoroutine());
        }

        private IEnumerator ShowCoroutine()
        {
            foreach (var message in messages)
            {
                text.text = message;

                float time = 0;
                while (time < interval)
                {
                    text.alpha = time / interval;
                    time += Time.deltaTime;

                    yield return null;
                }

                yield return new WaitForSeconds(messageDuration);
            }

            float time2 = 0;
            while (time2 < interval)
            {
                text.alpha = 1f - (time2 / interval);
                time2 += Time.deltaTime;

                yield return null;
            }

            Application.Quit();
        }
    }
}
