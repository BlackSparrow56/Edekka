using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Game.Items;
using Game.UI.Gears;

namespace Game.Gears
{
    public class GearsController : MonoBehaviour
    {
        [SerializeField] private Color activeColor;
        [SerializeField] private Color passiveColor;

        [SerializeField] private GearUI gearUI;

        [SerializeField] private List<Gear> gears;

        [SerializeField] private Inventory.Inventory inventory;

        private void UpdateColors()
        {
            for (int i = 0; i < gears.Count; i++)
            {
                var gear = gears[i];

                if (gear.IsActive)
                {
                    gear.SetColor(activeColor);
                    gear.gameObject.SetActive(true);
                }
                else if (i > 0 && gears[i - 1].IsActive || (!gear.IsActive && i == 0))
                {
                    gear.SetColor(passiveColor);
                    gear.gameObject.SetActive(true);
                }
                else
                {
                    gear.SetColor(Color.clear);
                    gear.gameObject.SetActive(false);
                }
            }
        }

        private void Craft(List<Item> input, Item output)
        {
            Dictionary<string, int> inputStacks = new Dictionary<string, int>();

            foreach (var i in input.GroupBy(el => el.name))
            {
                inputStacks.Add(i.Key, i.Count());
            }

            foreach (var kv in inputStacks)
            {
                for (int i = 0; i < kv.Value; i++)
                {
                    inventory.Items.Remove(inventory.Items.First(el => el.name == kv.Key));
                }
            }

            inventory.Items.Add(output);
        }

        private bool TryCraft(List<Item> input, Item output)
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

            return may;
        }

        private void OnEnable()
        {
            foreach (var gear in gears)
            {
                gear.onActiveStateChanged += UpdateColors;
                gear.onCraftConfirmed += Craft;
                gear.onTryCraft += TryCraft;

                gear.GearUI = gearUI;
            }

            UpdateColors();
        }

        private void OnDisable()
        {
            foreach (var gear in gears)
            {
                gear.onActiveStateChanged -= UpdateColors;
                gear.onCraftConfirmed -= Craft;
                gear.onTryCraft -= TryCraft;

                gear.GearUI = null;
            }
        }
    }
}
