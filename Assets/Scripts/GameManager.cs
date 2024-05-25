using UnityEngine;
using Game.Player;
using Game.Items;
using System;
using Game.Inventory;

namespace Game.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private PlayerScript player;

        [Header("GameObjects")]
        [SerializeField] private GameObject item;
        [SerializeField] private GameObject pickText;
        [SerializeField] private GameObject limitText;

        [Header("Transforms")]
        [SerializeField] private Transform itemsParent;
        [SerializeField] private Transform plane;

        [SerializeField] private InventoryScript inventoryScript;

        [SerializeField] private GameAssets gameAssets;

        [SerializeField] private ItemType itemType;

        private string[] itemNames = new string[] { ItemType.Sphere.ToString(), ItemType.Cube.ToString(), ItemType.Cylinder.ToString(), ItemType.Capsule.ToString()};

        private void OnEnable()
        {
            inventoryScript.LimitExceeded += OnLimitExceed;
        }

        private void OnDisable()
        {
            inventoryScript.LimitExceeded -= OnLimitExceed;
        }

        void Start()
        {
            for (int i = 0; i < 50; i++)
            {
                InstantiateItems();
            }

            //InvokeRepeating(nameof(InstantiateItems), 1f, 5f);
        }

        private void InstantiateItems()
        {
            GameObject insObj = Instantiate(item, itemsParent);

            float x = plane.localScale.x * 5;
            float z = plane.localScale.z * 5;

            insObj.transform.position = new Vector3(UnityEngine.Random.Range(-x, x), 1f, UnityEngine.Random.Range(-z, z));

            ItemScript items = insObj.GetComponent<ItemScript>();

            int randomIndex = UnityEngine.Random.Range(0, itemNames.Length);

            Sprite icon = null;
            Mesh mesh = null;

            itemType = (ItemType) Enum.Parse(typeof(ItemType), itemNames[randomIndex]);

            items.SetItemName(itemNames[randomIndex]);

            switch (itemType)
            {
                case ItemType.Sphere:
                    {
                        icon = gameAssets.Sphere;
                        mesh = gameAssets.sphereMesh;
                    }
                    break;

                case ItemType.Cube:
                    {
                        icon = gameAssets.Cube;
                        mesh = gameAssets.cubeMesh;
                    }
                    break;

                case ItemType.Capsule:
                    {
                        icon = gameAssets.Capsule;
                        mesh = gameAssets.capsuleMesh;
                    }
                    break;

                case ItemType.Cylinder:
                    {
                        icon = gameAssets.Cylinder;
                        mesh = gameAssets.cylinderMesh;
                    }
                    break;
            }

            items.SetIcon(icon);

            items.SetMesh(mesh);

            items.CloseToPlayer += OnCloseToPlayer;

            inventoryScript.SetItemsList(items);
        }

        private void OnCloseToPlayer(bool isCloseToPlayer, ItemScript itemsScript)
        {
            pickText.SetActive(isCloseToPlayer);

            player.SetIsCloseToPlayer(isCloseToPlayer);

            player.SetItem(isCloseToPlayer ? itemsScript : null);
        }

        private void OnLimitExceed()
        {
            limitText.SetActive(true);

            Invoke(nameof(OffLimitText), 2f);
        }

        private void OffLimitText()
        {
            limitText.SetActive(false);
        }
    }

    public enum ItemType
    {
        Cube, Sphere, Cylinder, Capsule
    }
}