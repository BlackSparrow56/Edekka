using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items
{
    [CreateAssetMenu(menuName = "Game/Items/Item", fileName = "Item")]
    public class Item : ScriptableObject
    {
        new public string name;

        [TextArea]
        public string description;
    }
}
