using System.Collections.Generic;
using UnityEngine;
using Game.Items;

namespace Game.Inventory
{
    public class Inventory : MonoBehaviour
    {
        private List<Item> _items = new List<Item>();
        public List<Item> Items => _items;

        public void SetItems(List<Item> items)
        {
            _items = items;
        }
    }
}
