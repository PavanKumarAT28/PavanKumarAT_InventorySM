using Game.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class InventoryScript : MonoBehaviour
    {
        [SerializeField] private List<Item> items = new List<Item>();

        [SerializeField] private List<InventoryItem> inventoryItems;

        [SerializeField] private List<ItemScript> itemScripts = new List<ItemScript>();

        public Action LimitExceeded { get; set; }

        private int maxCount = 10;

        private void OnEnable()
        {
            for (int i = 0; i < inventoryItems.Count; i++) 
            {
                inventoryItems[i].RemoveOneItem += OnRemoveItem;
            }
        }

        private void OnDisable()
        {
            for(int i = 0; i < inventoryItems.Count; i++) 
            {
                inventoryItems[i].RemoveOneItem -= OnRemoveItem;
            }
        }

        private void OnRemoveItem(InventoryItem inventoryItem) //Removes each item or the object
        {
            RemoveItem(inventoryItem);
        }

        public void AddItem(ItemScript itemScript) //Create's a object with the item name and then adds the items to another list so that the count of the items increases
        {
            Item item = new Item();

            if (this.items.Find(x => x.ItemName == itemScript.GetItemName()) != null)
            {
                for (int i = 0; i < this.items.Count; i++)
                {
                    item = this.items[i];

                    if (item.ItemName.Equals(itemScript.GetItemName()))
                    {
                        if (!item.ItemsScripts.Contains(itemScript))
                        {
                            if (item.ItemsScripts.Count < maxCount)
                            {
                                item.ItemsScripts.Add(itemScript);
                                UpdateInventory(item);
                            }
                            else
                            {
                                LimitExceeded?.Invoke();
                            }
                        }
                    }
                }
            }
            else
            {
                item.ItemName = itemScript.GetItemName();
                item.ItemIcon = itemScript.GetIcon();
                item.ItemsScripts.Add(itemScript);
                this.items.Add(item);
                UpdateInventory(item);
            }
        }

        public void RemoveItem(InventoryItem inventoryItem) //Removes the item from the last index
        {
            Item item = new Item();

            for (int i = 0; i < this.items.Count; i++)
            {
                item = this.items[i];

                if(item.ItemName == inventoryItem.GetItemName())
                {
                    if (item.ItemsScripts.Count > 0)
                    {
                        ThrowItem(item.ItemsScripts[item.ItemsScripts.Count - 1]);
                        item.ItemsScripts.RemoveAt(item.ItemsScripts.Count - 1);
                        inventoryItem.SetData(item.ItemName, item.ItemIcon, item.ItemsScripts.Count);
                    }

                    if (item.ItemsScripts.Count == 0)
                    {
                        inventoryItem.SetData(null, null, 0);
                        this.items.Remove(item);
                    }
                }
            }
        }

        private void UpdateInventory (Item item) //Updates the inventory UI
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].GetItemName() == null)
                {
                    inventoryItems[i].SetData(item.ItemName, item.ItemIcon, item.ItemsScripts.Count);
                    break;
                }
                else
                {
                    if (inventoryItems[i].GetItemName() == item.ItemName)
                    {
                        inventoryItems[i].SetData(item.ItemName, item.ItemIcon, item.ItemsScripts.Count);
                        break;
                    }
                }
            }
        }

        private void ThrowItem(ItemScript itemScript)
        {
            if(itemScripts.Contains(itemScript))
            {
                int index = itemScripts.IndexOf(itemScript);

                itemScripts[index].transform.position = itemScript.GetPlayer().transform.position + new Vector3(0f, 1f, 5f);
                itemScripts[index].gameObject.SetActive(true);
            }
        }

        public void SetItemsList(ItemScript itemScript)
        {
            itemScripts.Add(itemScript);
        }

        public int GetMaxCount()
        {
            return maxCount;
        }
    }

    //This class represents a data for the items
    [System.Serializable]
    public class Item
    {
        public string ItemName;
        public Sprite ItemIcon;
        public List<ItemScript> ItemsScripts = new List<ItemScript>();
    }
}