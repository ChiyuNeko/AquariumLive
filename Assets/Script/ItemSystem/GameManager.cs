// GameManager.cs
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int playerCurrency = 1000;
    public List<ShopItem> shopItems = new List<ShopItem>();
    public List<BackpackItem> backpack = new List<BackpackItem>();
    public BackpackUI backpackUI;
    public void BuyItem(string itemName)
    {
        ShopItem item = shopItems.Find(i => i.itemName == itemName);

        if (item != null && playerCurrency >= item.price)
        {
            playerCurrency -= item.price;

            BackpackItem backpackItem = backpack.Find(i => i.itemName == item.itemName);

            if (backpackItem != null)
            {
                backpackItem.quantity++;
            }
            else
            {
                backpack.Add(new BackpackItem { itemName = item.itemName, quantity = 1, itemImage = item.itemImage });
            }

            Debug.Log($"Purchased {item.itemName}. Remaining currency: {playerCurrency}");

            // 通知 Backpack UI 更新
            backpackUI.RefreshBackpackUI();
        }
        else
        {
            Debug.Log($"Not enough currency to purchase {itemName} or item not found.");
        }
    }
}
