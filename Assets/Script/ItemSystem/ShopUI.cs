// ShopUI.cs
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public GameManager gameManager;
    public Transform shopItemContainer;
    public GameObject shopItemPrefab;

    void Start()
    {
        foreach (var item in gameManager.shopItems)
        {
            GameObject shopItemObj = Instantiate(shopItemPrefab, shopItemContainer);
            shopItemObj.transform.Find("ItemName").GetComponent<Text>().text = item.itemName;
            shopItemObj.transform.Find("Price").GetComponent<Text>().text = $"${item.price}";
            shopItemObj.transform.Find("ItemImage").GetComponent<Image>().sprite = item.itemImage; // 設置圖片
            shopItemObj.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() => gameManager.BuyItem(item.itemName));
        }
    }
}
