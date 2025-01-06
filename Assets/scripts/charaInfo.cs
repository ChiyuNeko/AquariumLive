using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charaInfo : MonoBehaviour
{
    public GameObject charainfo;
    public Button goBack;
    public collection collection;

    private void OnEnable()
    {
        goBack.onClick.AddListener(GoBack);
        charainfo.SetActive(true);
    }

    void GoBack()
    {
        collection.listenerOn();
        charainfo.SetActive(false);
    }
}
