using Game.Items;
using Game.Player;
using Game.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gears
{
    public class CreateRocket : MonoBehaviour
    {
        [SerializeField] private List<Item> input;

        [SerializeField] private GameObject button;

        [SerializeField] private GameObject panel;

        [SerializeField] private TMP_Text text;
        [SerializeField] private Hint hint;

        [SerializeField] private Inventory.Inventory inventory;

        private Logic logic;

        private bool isOpened = false;

        public void ToggleUI()
        {
            isOpened = !isOpened;
            panel.SetActive(isOpened);

            if (isOpened)
            {
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            string text = $"Для создания ракеты Вам понадобятся: \n";

            Dictionary<string, int> inputStacks = new Dictionary<string, int>();

            foreach (var kv in input.GroupBy(el => el.name))
            {
                inputStacks.Add(kv.Key, kv.Count());
            }

            int i = 0;
            foreach (var kv in inputStacks)
            {
                text += $"{kv.Key} ({kv.Value})";

                if (i != inputStacks.Count - 1)
                {
                    text += ", ";
                }
                else
                {
                    text += ".\n";
                }

                i++;
            }

            this.text.text = text;
        }

        public void LoadFinalScene()
        {
            Dictionary<string, int> inputStacks = new Dictionary<string, int>();
            Dictionary<string, int> itemsStacks = new Dictionary<string, int>();

            foreach (var i in input.GroupBy(el => el.name))
            {
                inputStacks.Add(i.Key, i.Count());
            }

            foreach (var i in inventory.Items.GroupBy(el => el.name))
            {
                itemsStacks.Add(i.Key, i.Count());
            }

            bool may = inputStacks.All(kv => itemsStacks.ContainsKey(kv.Key)) && inputStacks.All(kv => itemsStacks[kv.Key] > kv.Value);

            if (may)
            {
                Destroy(logic);
                SceneManager.LoadScene(1);
            }
            else
            {
                ToggleUI();
                hint.ShowText($"Вам не хватает ресурсов на создание ракеты!", 0.5f);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            var player = collider.gameObject.GetComponent<Logic>();
            if (player != null)
            {
                button.SetActive(true);
                logic = player;
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            var player = collider.gameObject.GetComponent<Logic>();
            if (player != null)
            {
                button.SetActive(false);
            }
        }
    }
}