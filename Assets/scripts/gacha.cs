using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gacha: MonoBehaviour
{
    public Button home;
    public Button collcetion;
    public Button shop;
    public Button gachaB;
    public Button friend;

    public Button roll1;
    public Button roll10;

    public void Start()
    {
        home.onClick.AddListener(goMain);
        collcetion.onClick.AddListener(goCollect);
        shop.onClick.AddListener(goShop);
        gachaB.onClick.AddListener(goGacha);
        friend.onClick.AddListener(goFriend);
    }

    void goMain()
    {
        SceneManager.LoadScene("main");
    }
    void goCollect()
    {
        SceneManager.LoadScene("collection");
    }
    void goShop()
    {
        SceneManager.LoadScene("shop");
    }
    void goGacha()
    {
        SceneManager.LoadScene("gacha");
    }
    void goFriend()
    {
        SceneManager.LoadScene("friends");
    }
}
