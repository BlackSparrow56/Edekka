using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Image buttonImage;

        [SerializeField] private Sprite pauseSprite;
        [SerializeField] private Sprite resumeSprite;

        private float _tempTimeScale;
        private bool _isPaused = false;

        public void Toggle()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                _tempTimeScale = Time.timeScale;
                Time.timeScale = 0f;
                buttonImage.sprite = resumeSprite;
            }
            else
            {
                Time.timeScale = _tempTimeScale;
                buttonImage.sprite = pauseSprite;
            }
        }
    }
}
