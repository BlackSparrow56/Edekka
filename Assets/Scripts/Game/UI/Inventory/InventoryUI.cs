using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game.UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Hint hint; 

        [SerializeField] new private TMP_Text name;
        [SerializeField] private TMP_Text description;

        [SerializeField] private GameObject itemUIPrefab;

        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private Transform itemsContent;

        [SerializeField] private Game.Inventory.Inventory inventory;

        private List<ItemUI> _items = new List<ItemUI>();
        private ItemUI _currentItem;

        private bool _isOpened = false;
        public bool IsOpened => _isOpened;

        public void ToggleInventory()
        {
            if (!_isOpened && inventory.Items.Count == 0)
            {
                hint.ShowText($"Вы не можете открыть инвентарь, потому что он пуст.", 0.5f);

                return;
            }

            _isOpened = !_isOpened;
            if (_isOpened)
            {
                UpdateInventory();
            }

            inventoryPanel.SetActive(_isOpened);
        }

        private void UpdateInventory()
        {
            foreach (var item in _items)
            {
                Destroy(item.gameObject);
            }
            _items.Clear();

            foreach (var item in inventory.Items)
            {
                var firstOrDefault = _items.FirstOrDefault(value => value.Name == item.name);
                if (firstOrDefault == default)
                {
                    var itemUI = Instantiate(itemUIPrefab, itemsContent);
                    var component = itemUI.GetComponent<ItemUI>();

                    component.Init(item.name, item.description);
                    component.IncreaseCount();

                    _items.Add(component);
                }
                else
                {
                    firstOrDefault.IncreaseCount();
                }
            }

            if (_items != null && _items.Count != 0)
            {
                Select(_items[0]);
            }
        }

        private void Select(ItemUI item)
        {
            _currentItem?.Deselect();
            _currentItem = item;
            _currentItem.Select();

            name.text = _currentItem.Name;
            description.text = _currentItem.Description;
        }

        private void Update()
        {
            if (!_isOpened) return;

            if (Input.GetKeyDown(KeyCode.W))
            {
                Select(_items[ClampIndex(_items.IndexOf(_currentItem) - 1, _items.Count)]);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Select(_items[ClampIndex(_items.IndexOf(_currentItem) + 1, _items.Count)]);
            }

            int ClampIndex(int number, int count)
            {
                if (number < 0)
                {
                    return count - 1;
                }
                else if (number >= count)
                {
                    return 0;
                }
                else
                {
                    return number;
                }
            }
        }
    }
}
