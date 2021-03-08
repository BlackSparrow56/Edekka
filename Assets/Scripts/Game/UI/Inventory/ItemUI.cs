using UnityEngine;
using TMPro;

namespace Game.UI.Inventory
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private string _name;
        private string _description;
        private int _count;

        public string Name => _name;
        public string Description => _description;

        public void Init(string name, string description)
        {
            _name = name;
            _description = description;

            UpdateName();
        }

        public void IncreaseCount()
        {
            _count++;
            UpdateName();
        }

        public void Select()
        {
            text.fontStyle = FontStyles.Underline;
        }

        public void Deselect()
        {
            text.fontStyle = FontStyles.Normal;
        }

        private void UpdateName()
        {
            text.text = $"{_name} ({_count})";
        }
    }
}
