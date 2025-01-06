using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class performStage : MonoBehaviour
{
    public Button home;
    public Button collcetion;
    public Button shop;
    public Button gacha;
    public Button friend;
    public Button stage1_1;
    public Button stage1_2;
    public Button stage1_3;
    public Button stage1_4;
    public Button stage1_5;
    public Button stage1_6;

    public GameObject readyScr;

    public void Start()
    {
        readyScr.SetActive(false);
        stageListenerOn();
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
    void showReady()
    {
        stageListenerOff();
        readyScr.SetActive(true);
    }

    public void stageListenerOn()
    {
        home.onClick.AddListener(goMain);
        collcetion.onClick.AddListener(goCollect);
        shop.onClick.AddListener(goShop);
        gacha.onClick.AddListener(goGacha);
        friend.onClick.AddListener(goFriend);

        stage1_1.onClick.AddListener(showReady);
        stage1_2.onClick.AddListener(showReady);
        stage1_3.onClick.AddListener(showReady);
        stage1_4.onClick.AddListener(showReady);
        stage1_5.onClick.AddListener(showReady);
        stage1_6.onClick.AddListener(showReady);
    }

    void stageListenerOff()
    {
        home.onClick.RemoveAllListeners();
        collcetion.onClick.RemoveAllListeners();
        shop.onClick.RemoveAllListeners();
        gacha.onClick.RemoveAllListeners();
        friend.onClick.RemoveAllListeners();

        stage1_1.onClick.RemoveAllListeners();
        stage1_2.onClick.RemoveAllListeners();
        stage1_3.onClick.RemoveAllListeners();
        stage1_4.onClick.RemoveAllListeners();
        stage1_5.onClick.RemoveAllListeners();
        stage1_6.onClick.RemoveAllListeners();
    }
}
