using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class welcome : MonoBehaviour
{
    public Button w;

    public void Start()
    {
        w.onClick.AddListener(gameStart);
    }

    void gameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
