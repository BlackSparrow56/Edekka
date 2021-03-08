using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Items;
using Game.Player;
using Game.UI.Gears;

namespace Game.Gears
{
    public class Gear : MonoBehaviour
    {
        [SerializeField] new private string name;

        [SerializeField] private List<Item> gearCraftInput;

        [SerializeField] private List<Item> input;
        [SerializeField] private Item output;

        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private GameObject button;

        [SerializeField] private bool isActive;
        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                onActiveStateChanged.Invoke();
            }
        }

        public GearUI GearUI
        {
            get;
            set;
        }

        public Action onActiveStateChanged = () => { };
        public Action<List<Item>, Item> onCraftConfirmed = (input, output) => { };

        public Func<List<Item>, Item, bool> onTryCraft;

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public void OpenUI()
        {
            if (isActive)
            {
                GearUI.onCraftPerformed = Craft;
            }
            else
            {
                GearUI.onCraftPerformed = CraftGear;
            }

            GearUI.Open(CreateArgs());
        }

        public void Craft()
        {
            onCraftConfirmed.Invoke(input, output);
        }

        private void CraftGear()
        {
            IsActive = true;
        }

        private OpenArgs CreateArgs()
        {
            var args = new OpenArgs
            {
                name = this.name,
                isActive = isActive,
                gearCraftInput = gearCraftInput,
                input = input,
                output = output,
                canCraft = isActive ? onTryCraft.Invoke(input, output) : onTryCraft.Invoke(gearCraftInput, output)
            };

            return args;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.gameObject.GetComponent<Logic>();
            if (player != null)
            {
                button.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var player = collision.gameObject.GetComponent<Logic>();
            if (player != null)
            {
                button.SetActive(false);
            }
        }
    }
}
