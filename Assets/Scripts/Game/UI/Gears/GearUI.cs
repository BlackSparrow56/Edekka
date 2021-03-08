using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using Game.Items;
using TMPro;

namespace Game.UI.Gears
{
    public class GearUI : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text craftButtonText;

        [SerializeField] private Hint hint;

        public Action onButtonClicked = () => { };
        public Action onCraftPerformed = () => { };

        public void Craft()
        {
            onButtonClicked.Invoke();
            Close();
        }

        public void Open(OpenArgs args)
        {
            if (args.isActive)
            {
                craftButtonText.text = $"Крафт";

                string text = $"{name}\nВы можете объединить следующие элементы: \n";

                Dictionary<string, int> inputStacks = new Dictionary<string, int>();

                foreach (var kv in args.input.GroupBy(el => el.name))
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

                text += $"для получения одного более ценного: \n";
                text += args.output.name;

                descriptionText.text = text;

                onButtonClicked = Click;

                void Click()
                {
                    if (args.canCraft)
                    {
                        hint.ShowText("Крафт прошёл успешно", 0.5f);
                        onCraftPerformed.Invoke();
                    }
                    else
                    {
                        hint.ShowText("Недостаточно ингридиентов!", 0.5f);
                    }
                }
            }
            else
            {
                craftButtonText.text = $"Изготовить";

                string text = 
                    $"Вам нужно сначала изготовить этот станок." +
                    $"Для этого Вам понадобятся: ";

                Dictionary<string, int> inputStacks = new Dictionary<string, int>();

                foreach (var kv in args.gearCraftInput.GroupBy(el => el.name))
                {
                    inputStacks.Add(kv.Key, kv.Count());
                }

                int i = 0;
                foreach (var kv in inputStacks)
                {
                    text += $"{kv.Key} ({kv.Value})";

                    if (i != args.input.Count - 1)
                    {
                        text += ", ";
                    }
                    else
                    {
                        text += "\n";
                    }

                    i++;
                }

                onButtonClicked = Click;

                void Click()
                {
                    if (args.canCraft)
                    {
                        hint.ShowText("Станок изготовлен успешно", 0.5f);
                        onCraftPerformed.Invoke();
                    }
                    else
                    {
                        hint.ShowText("Недостаточно ингридиентов!", 0.5f);
                    }
                }

                descriptionText.text = text;
            }

            panel.SetActive(true);
        }

        public void Close()
        {
            panel.SetActive(false);

            onButtonClicked = () => { };
            onCraftPerformed = () => { };
        }
    }

    public struct OpenArgs
    {
        public string name;
        public bool isActive;
        public List<Item> gearCraftInput;
        public List<Item> input;
        public Item output;
        public bool canCraft;
    }
}
