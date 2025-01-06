using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class collection : MonoBehaviour
{
    public Button home;
    public Button collcetion;
    public Button shop;
    public Button gacha;
    public Button friend;

    public Button teamEdit;
    public Button clct_info;
    public GameObject charaInfo;
    

    public void Start()
    {
        charaInfo.SetActive(false);
        listenerOn();
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
    void goCharaInfo()
    {
        listenerOff();
        charaInfo.SetActive(true);
    }

    void goEdit()
    {
        SceneManager.LoadScene("teamEdit");
    }

    public void listenerOn()
    {
        home.onClick.AddListener(goMain);
        collcetion.onClick.AddListener(goCollect);
        shop.onClick.AddListener(goShop);
        gacha.onClick.AddListener(goGacha);
        friend.onClick.AddListener(goFriend);

        teamEdit.onClick.AddListener(goEdit);
        clct_info.onClick.AddListener(goCharaInfo);
    }

    public void listenerOff()
    {
        home.onClick.RemoveAllListeners();
        collcetion.onClick.RemoveAllListeners();
        shop.onClick.RemoveAllListeners();
        gacha.onClick.RemoveAllListeners();
        friend.onClick.RemoveAllListeners();

        teamEdit.onClick.RemoveAllListeners();
        clct_info.onClick.RemoveAllListeners();
    }
}
