using Game.Player;
using System;
using UnityEngine;

namespace Game.Items
{
    public class ItemScript : MonoBehaviour
    {
        [SerializeField] private string ItemName;

        [SerializeField] private Sprite ItemIcon;

        [SerializeField] private PlayerScript player;

        [SerializeField] private float distance;

        [SerializeField] private MeshFilter meshFilter;

        public Action<bool, ItemScript> CloseToPlayer { get; set; }

        private bool isCloseToPlayer = false;

        private void OnEnable()
        {
            player = FindObjectOfType<PlayerScript>();
        }

        private void OnDisable()
        {
            if(player != null) 
                CloseToPlayer?.Invoke(false, this);
        }

        void Update()
        {
            //Checks the distance between the plaer and item 

            distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < 5f && !isCloseToPlayer)
            {
                CloseToPlayer?.Invoke(true, this);
                isCloseToPlayer = true;
            }
            else if (distance > 5f && isCloseToPlayer)
            {
                CloseToPlayer?.Invoke(false, this);
                isCloseToPlayer = false;
            }
        }

        public void SetItemName(string itemName)
        {
            this.ItemName = itemName;
        }

        public void SetMesh(Mesh mesh)
        {
            meshFilter.mesh = mesh;
        }

        public string GetItemName()
        {
            return ItemName;
        }

        public void SetIcon (Sprite icon)
        {
            ItemIcon = icon;
        }

        public Sprite GetIcon()
        {
            return ItemIcon;
        }

        public PlayerScript GetPlayer()
        {
            return player;
        }
    }
}