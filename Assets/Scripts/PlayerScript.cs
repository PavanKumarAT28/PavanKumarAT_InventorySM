using UnityEngine;
using Game.Inventory;
using Game.Items;

namespace Game.Player
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private InventoryScript inventory;

        [SerializeField] private ItemScript itemsScript;

        [SerializeField] private float playerSpeed;

        private bool isCloseToItem = false;

        void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                MovePlayer(Vector3.forward);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                MovePlayer(Vector3.back);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.eulerAngles += new Vector3(0, -0.25f, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.eulerAngles += new Vector3(0, 0.25f, 0);
            }

            if(Input.GetKeyDown(KeyCode.G)) //Picks up the item
            {
                if (isCloseToItem)
                {
                    inventory.AddItem(itemsScript);
                    itemsScript.gameObject.SetActive(false);
                    itemsScript = null;
                }
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private void MovePlayer(Vector3 direction) //Move the player on the inputs given
        {
            transform.Translate(direction * playerSpeed * Time.deltaTime);
        }

        public void SetItem(ItemScript itemsScript)
        {
            this.itemsScript = itemsScript;
        }

        public void SetIsCloseToPlayer(bool isCloseToItem)
        {
            this.isCloseToItem = isCloseToItem;
        }
    }
}