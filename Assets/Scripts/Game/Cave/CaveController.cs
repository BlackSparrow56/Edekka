using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using Game.Items;

namespace Game.Cave
{
    public class CaveController : MonoBehaviour
    {
        [SerializeField] private int capacity;

        [SerializeField] private float offset;

        [SerializeField] private GameObject ledgePrefab;
        [SerializeField] private GameObject backgroundTilePrefab;

        [SerializeField] private List<Mineral> possibleMinerals;
        [SerializeField] private List<Sprite> ledgeSprites;

        [SerializeField] private Game.Inventory.Inventory playerInventory;
        [SerializeField] private Hint subscreenHint;

        [SerializeField] private Camera worldCamera;

        private Ledge _currentLedge;
        private List<GameObject> _backgroundTiles = new List<GameObject>();

        private int _currentIteration = 0;
        private Vector3 _cameraAnchor;

        private void Start()
        {
            _cameraAnchor = worldCamera.transform.position;

            ChangeLedge();
            CreateBackground();
            MoveBackground();

            _currentIteration++;
        }

        private void Update()
        {
            worldCamera.transform.position = Vector3.Lerp(worldCamera.transform.position, _cameraAnchor, Time.deltaTime / 3);
        }

        private void CreateBackground()
        {
            float width = backgroundTilePrefab.transform.localScale.x;

            for (int i = 0; i < capacity - 1; i++)
            {
                var backgroundTile = Instantiate(backgroundTilePrefab, transform);
                backgroundTile.transform.localPosition = Vector3.right * (width * i);

                _backgroundTiles.Add(backgroundTile);
            }
        }

        private void LedgeDestroyed()
        {
            MoveCameraAnchor();
            DoIteration();
            GiveMinerals();
        }

        private void DoIteration()
        {
            ChangeLedge();
            MoveBackground();

            _currentIteration++;
        }

        private void ChangeLedge()
        {
            Destroy(_currentLedge?.gameObject);
            _currentLedge = CreateLedge();

            _currentLedge.onLedgeDestroyed += LedgeDestroyed;
            _currentLedge.Init(Random.Range(3, 6));
        }

        private Ledge CreateLedge()
        {
            var ledge = Instantiate(ledgePrefab, transform);
            ledge.transform.localPosition = Vector3.right * ((_currentIteration) * offset);
            ledge.GetComponent<SpriteRenderer>().sprite = ledgeSprites[Random.Range(0, ledgeSprites.Count)];

            return ledge.GetComponent<Ledge>();
        }

        private void GiveMinerals()
        {
            List<Mineral> findedMinerals = new List<Mineral>();

            foreach (var mineral in possibleMinerals)
            {
                if (Random.Range(0f, 100f) < mineral.findingChance)
                {
                    findedMinerals.Add(mineral);
                    playerInventory.Items.Add(mineral);
                }
            }

            string text = $"Найдено: ";
            for (int i = 0; i < findedMinerals.Count; i++)
            {
                if (i > 0)
                {
                    text += ", ";
                }

                text += findedMinerals[i].name;

                if (i == findedMinerals.Count - 1)
                {
                    text += ".";
                }
            }

            subscreenHint.ShowText(text, 0.5f);
        }

        private void MoveBackground()
        {
            foreach(var tile in _backgroundTiles)
            {
                tile.transform.position += Vector3.right * offset;
            }
        }

        private void MoveCameraAnchor()
        {
            _cameraAnchor += Vector3.right * offset;
        }
    }
}