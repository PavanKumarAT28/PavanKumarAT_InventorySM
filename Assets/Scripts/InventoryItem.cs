using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Game.Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        private string ItemName;

        [Header("Images")]
        [SerializeField] private Image icon;

        [Header("Buttons")]
        [SerializeField] private Button removeButton;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI countText;

        public Action<InventoryItem> RemoveOneItem { get; set; }

        private int count = 0;

        private void OnEnable()
        {
            removeButton.onClick.AddListener(RemoveButtonClick);
        }

        private void OnDisable()
        {
            removeButton.onClick.RemoveListener(RemoveButtonClick);
        }

        private void Awake()
        {
            removeButton.gameObject.SetActive(false);
            countText.text = "0";
        }

        private void RemoveButtonClick()
        {
            RemoveOneItem?.Invoke(this);
        }

        public void SetData(string itemName, Sprite icon, int count) //Sets the data for the UI
        {
            this.ItemName = itemName;
            this.icon.sprite = icon;
            this.count = count;

            if (count > 0)
            {
                removeButton.gameObject.SetActive(true);
            }
            else
            {
                removeButton.gameObject.SetActive(false);
            }

            countText.text = count.ToString();
        }

        public string GetItemName()
        {
            return this.ItemName;
        }

        public Sprite GetIcon()
        {
            return this.icon.sprite;
        }

        public int GetCount()
        {
            return count;
        }
    }
}