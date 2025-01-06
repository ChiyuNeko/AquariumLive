using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class performReady : MonoBehaviour
{
    public Button back;
    public Button start;
    public Button teamEdit;
    public performStage performStage;

    private void OnEnable()
    {
        ListenerOn();
    }

    void goBack()
    {
        ListenerOff();
        performStage.stageListenerOn();
        gameObject.SetActive(false);
    }
    void gameStart()
    {
        SceneManager.LoadScene("start");
    }
    void goEdit()
    {
        SceneManager.LoadScene("teamEdit");
    }

    private void ListenerOn()
    {
        back.onClick.AddListener(goBack);
        start.onClick.AddListener(gameStart);
        teamEdit.onClick.AddListener(goEdit);
    }

    private void ListenerOff()
    {
        back.onClick.RemoveAllListeners();
        start.onClick.RemoveAllListeners();
        teamEdit.onClick.RemoveAllListeners();
    }
}
