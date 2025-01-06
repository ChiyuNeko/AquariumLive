using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class result : MonoBehaviour
{
    public Button con;

    public void Start()
    {
        con.onClick.AddListener(next);
    }

    void next()
    {
        SceneManager.LoadScene("SampleScene");
    }
}