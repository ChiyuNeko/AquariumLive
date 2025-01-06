using UnityEngine;
using UnityEngine.UI;

public class BackpackUI : MonoBehaviour
{
    public GameManager gameManager;
    public Transform backpackContainer;
    public GameObject backpackItemPrefab;

    // BackpackUI.cs
    public void RefreshBackpackUI()
    {
        foreach (Transform child in backpackContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in gameManager.backpack)
        {
            GameObject backpackItemObj = Instantiate(backpackItemPrefab, backpackContainer);
            backpackItemObj.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
            backpackItemObj.transform.Find("Quantity").GetComponent<Text>().text = $"x{item.quantity}";
            backpackItemObj.transform.Find("ItemImage").GetComponent<Image>().sprite = item.itemImage; // 設置圖片
        }

        Debug.Log("Backpack UI refreshed.");
    }

}
