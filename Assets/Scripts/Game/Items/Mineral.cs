using UnityEngine;

namespace Game.Items
{
    [CreateAssetMenu(menuName = "Game/Items/Mineral", fileName = "Mineral")]
    public class Mineral : Item
    {
        [Range(0f, 100f)]
        public float findingChance;
    }
}
