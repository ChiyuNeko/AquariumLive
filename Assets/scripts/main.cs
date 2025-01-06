using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    public Button home;
    public Button collcetion;
    public Button shop;
    public Button gacha;
    public Button friend;
    public Button build;
    public Button book;
    public Button perform;

    public void Start()
    {
        home.onClick.AddListener(goMain);
        collcetion.onClick.AddListener(goCollect);
        shop.onClick.AddListener(goShop);
        gacha.onClick.AddListener(goGacha);
        friend.onClick.AddListener(goFriend);
        build.onClick.AddListener(goBuild);
        book.onClick.AddListener(goBook);
        perform.onClick.AddListener(goPerform);
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
    void goBuild()
    {
        SceneManager.LoadScene("build");
    }
    void goBook()
    {
        SceneManager.LoadScene("book");
    }
    void goPerform()
    {
        SceneManager.LoadScene("perform");
    }

}
