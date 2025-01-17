using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using RhythmNamespace;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BlockManager : MonoBehaviour
{
    public GameObject[] blocks; // 16 個方塊
    public AudioClip beatSound; // 節拍音效
    public AudioSource audioSource; // 音效播放源

    public string rhythmJsonPath = "Assets/Rhythms.json"; // 節奏文件路徑
    public float delayStart;// 延遲開始時間（秒）
    public Text delayText;
    public float perfectWindow = 0.2f; // Perfect 判定的時間窗口（秒）
    public Text Score;
    public Text ResaultScore;
    public bool Click{get; set;}
    public Button button;
    public Animator animator;
    public UnityEvent OnFinish;

    private int currentBeat = 0;
    private float interval; // 每拍的時間間隔
    private List<RhythmData> rhythms = new List<RhythmData>();
    private int currentRhythmIndex = 0; // 當前播放的節奏索引
    private RhythmData currentRhythm;
    private bool isPlayerTurn = false; // 判斷是否為玩家回合
    private float[] playerInputTimings; // 玩家每次輸入的時間
    private int playerScore = 0; // 玩家得分
    void Start()
    {
        //rhythmJsonPath = Path.Combine(Application.streamingAssetsPath,"Rhythms.json");
        LoadRhythmJson();
        if (rhythms.Count > 0)
        {
            currentRhythmIndex = 0;
            LoadCurrentRhythm();
            //StartCoroutine(PlayGame());
        }
        else
        {
            Debug.LogError("No rhythms found in the JSON file.");
        }
    }
    void Update()
    {
        ResaultScore.text = Score.text;
        delayText.text = $"延遲：{delayStart}";
        if(Click)
        {
            Debug.Log("Click");
        }
        Score.text = $"得分：{playerScore}";
    }
    public void DelayPlus()
    {
        delayStart +=0.1f;
    }
    public void DelayMinus()
    {
        delayStart -=0.1f;
    }

    public void ReStart()
    {
        SceneManager.LoadScene("Start");
    }

    private void LoadRhythmJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Rhythms"); // 不要包含文件副檔名
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            RhythmFile rhythmFile = JsonUtility.FromJson<RhythmFile>(json);
            rhythms = rhythmFile.rhythms;
            Debug.Log("Rhythm JSON loaded from Resources.");
        }
        else
        {
            Debug.LogError("Rhythm JSON file not found in Resources.");
        }
    }

    private void LoadCurrentRhythm()
    {
        if (currentRhythmIndex >= 0 && currentRhythmIndex < rhythms.Count)
        {
            currentRhythm = rhythms[currentRhythmIndex];
            interval = 60f / currentRhythm.bpm / 4f; // 每 16 拍分 4 等份的間隔
            currentBeat = 0; // 重置拍子
            playerInputTimings = new float[16]; // 初始化玩家輸入時間
        }
        else
        {
            Debug.LogError("Invalid rhythm index.");
        }
    }

    public void _PlayGame()
    {
        StartCoroutine(PlayGame());
    }

    private IEnumerator PlayGame()
    {
        yield return new WaitForSeconds(delayStart); // 延遲開始

        while (currentRhythmIndex < rhythms.Count)
        {
            // 節奏示範階段
            isPlayerTurn = false;
            button.interactable = false;
            yield return PlayCurrentRhythm();

            // 玩家模仿階段
            isPlayerTurn = true;
            button.interactable = true;
            yield return WaitForPlayerInput();

            // 判定結果
            JudgePlayerInput();

            // 切換到下一個節奏
            currentRhythmIndex++;
            if (currentRhythmIndex < rhythms.Count)
            {
                LoadCurrentRhythm();
            }
            else
            {
                OnFinish?.Invoke();
                Debug.Log("All rhythms completed!");
                break;
            }
        }

        Debug.Log($"Game Over! Final Score: {playerScore}");
    }

    private IEnumerator PlayCurrentRhythm()
    {
        Debug.Log($"Playing Rhythm: {currentRhythm.name} (BPM: {currentRhythm.bpm})");

        while (currentBeat < currentRhythm.pattern.Length)
        {
            ResetBlocks();

            // 啟用當前節拍的方塊
            if (currentRhythm.pattern[currentBeat] == 1)
            {
                blocks[currentBeat].GetComponent<RawImage>().color = Color.yellow;

                // 播放音效
                if (audioSource && beatSound)
                {
                    audioSource.PlayOneShot(beatSound);
                    animator.Play("Character");
                }
            }

            currentBeat++;
            yield return new WaitForSeconds(interval);
        }

        currentBeat = 0; // 重置拍子
        ResetBlocks();
    }

    private IEnumerator WaitForPlayerInput()
    {
        Debug.Log("Player Turn: Mimic the rhythm!");

        float elapsedTime = 0f;
        currentBeat = 0;

        while (elapsedTime < interval * 16)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Click)
            {
                // 記錄玩家輸入的時間
                playerInputTimings[currentBeat] = elapsedTime;

                // 高亮玩家輸入的方塊
                if (currentBeat < blocks.Length)
                {
                    blocks[currentBeat].GetComponent<RawImage>().color = Color.green;
                }

                Debug.Log($"Player pressed at time {elapsedTime:F2}");
                Click = false;
            }

            elapsedTime += Time.deltaTime;
            yield return null;

            // 更新當前節拍
            currentBeat = Mathf.FloorToInt(elapsedTime / interval);
        }
    }

    private void JudgePlayerInput()
    {
        int scoreForPerfect = 100;

        for (int i = 0; i < currentRhythm.pattern.Length; i++)
        {
            if (currentRhythm.pattern[i] == 1)
            {
                float beatTime = i * interval; // 當前拍子的理論時間
                float playerTime = playerInputTimings[i]; // 玩家輸入的實際時間

                if (Mathf.Abs(playerTime - beatTime) <= perfectWindow)
                {
                    // Perfect 判定
                    playerScore += scoreForPerfect;
                    Debug.Log($"Beat {i + 1}: Perfect! Time Difference: {playerTime - beatTime:F2}");
                }
                else
                {
                    // Miss 判定
                    Debug.Log($"Beat {i + 1}: Miss. Time Difference: {playerTime - beatTime:F2}");
                }
            }
        }
    }

    private void ResetBlocks()
    {
        foreach (var block in blocks)
        {
            block.GetComponent<RawImage>().color = Color.white;
        }
    }

}
