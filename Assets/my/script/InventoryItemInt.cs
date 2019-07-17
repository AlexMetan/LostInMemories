using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemInt : MonoBehaviour
{
   [SerializeField] int inventoryIndex;
   public int InventoryItemIndex=>inventoryIndex;
   [SerializeField] Sprite itemImage;
   public Sprite ItemImage { get => itemImage; set => itemImage = value; }
   [SerializeField] Vector3 size;
   public Vector3 Size { get => size; set => size = value; }

}
