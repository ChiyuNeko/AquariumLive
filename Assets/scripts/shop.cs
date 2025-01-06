using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class shop: MonoBehaviour
{
    public Button home;
    public Button collcetion;
    public Button shopB;
    public Button gacha;
    public Button friend;

    public void Start()
    {
        home.onClick.AddListener(goMain);
        collcetion.onClick.AddListener(goCollect);
        shopB.onClick.AddListener(goShop);
        gacha.onClick.AddListener(goGacha);
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
