using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    private CharacterPoolData characterPool;
    public int playerCurrency = 100;    // 預設貨幣
    public Transform resultDisplay;    // 結果展示區
    public GameObject iconPrefab;      // 用於展示角色 Icon 的預製件
    public GameObject Video;
    public VideoPlayer videoPlayer;    // VideoPlayer 組件
    public GameObject GotchaUI;
    public Text MoneyText;
    private List<CharacterData> multiDrawResults; // 暫存十連抽結果

    private void Awake()
    {
        LoadCharacterPool();
    }

    void  Update()
    {

        MoneyText.text = "" + playerCurrency;
    }
    private void LoadCharacterPool()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("CharacterPool");
        if (jsonFile == null)
        {
            Debug.LogError("CharacterPool.json not found in Resources!");
            return;
        }

        characterPool = JsonUtility.FromJson<CharacterPoolData>(jsonFile.text);
        Debug.Log($"Loaded {characterPool.characters.Length} characters.");
    }
    public void SingleDraw()
    {
        if (playerCurrency >= 5)
        {
            GotchaUI.SetActive(true);
            playerCurrency -= 5;
            PlayGachaVideo(() =>
            {
                ClearPreviousResults(); // 確保結果清理在顯示前執行
                CharacterData character = DrawCharacter();
                DisplayResult(character);
            });
        }
        else
        {
            Debug.Log("Not enough currency for a single draw.");
        }
    }

    public void MultiDraw()
    {
        if (playerCurrency >= 50)
        {
            GotchaUI.SetActive(true);
            playerCurrency -= 50;
            PlayGachaVideo(() =>
            {
                ClearPreviousResults(); // 確保結果清理在顯示前執行
                multiDrawResults = new List<CharacterData>();
                for (int i = 0; i < 10; i++)
                {
                    multiDrawResults.Add(DrawCharacter());
                }

                foreach (var character in multiDrawResults)
                {
                    DisplayResult(character);
                }
            });
        }
        else
        {
            Debug.Log("Not enough currency for a multi draw.");
        }
    }

     private CharacterData DrawCharacter()
    {
        if (characterPool == null || characterPool.characters == null || characterPool.characters.Length == 0)
        {
            Debug.LogError("Character pool is empty or not loaded.");
            return null;
        }

        float totalProbability = 0;
        foreach (var character in characterPool.characters)
        {
            totalProbability += character.probability;
        }

        float randomValue = Random.Range(0, totalProbability);
        float cumulativeProbability = 0;

        foreach (var character in characterPool.characters)
        {
            cumulativeProbability += character.probability;
            if (randomValue <= cumulativeProbability)
            {
                return character;
            }
        }

        Debug.LogError("Failed to draw a character. Check probabilities.");
        return null;
    }



    private void DisplayResult(CharacterData character)
{
    if (character == null)
    {
        Debug.LogError("Character data is null.");
        return;
    }

    Sprite icon = Resources.Load<Sprite>(character.iconPath);
    if (icon == null)
    {
        Debug.LogError($"Icon not found at path: {character.iconPath}");
        return;
    }

    GameObject iconObject = Instantiate(iconPrefab, resultDisplay);
    iconObject.GetComponent<UnityEngine.UI.Image>().sprite = icon;
    Debug.Log($"You got: {character.characterName}");
}


    private void ClearPreviousResults()
    {
        if (resultDisplay == null)
        {
            Debug.LogError("Result display container is not assigned!");
            return;
        }

        foreach (Transform child in resultDisplay)
        {
            Destroy(child.gameObject);
        }
    }

    // private void PlayGachaVideo(System.Action onVideoEnd)
    // {
    //     Video.SetActive(true);
    //     videoPlayer.Play();
    //     videoPlayer.loopPointReached += (vp) =>
    //     {
    //         vp.Stop();
    //         onVideoEnd?.Invoke();
    //         Video.SetActive(false);
    //     };
    // }
    
    private void PlayGachaVideo(System.Action onVideoEnd)
    {
        if (videoPlayer == null)
        {
            Debug.LogError("Video player is not assigned.");
            onVideoEnd?.Invoke(); // 確保後續邏輯不被阻斷
            return;
        }

        if (videoPlayer.clip == null)
        {
            Debug.LogError("Video player clip is not assigned.");
            onVideoEnd?.Invoke();
            return;
        }
        Video.SetActive(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += (vp) =>
        {
            vp.Stop();
            if (onVideoEnd != null)
            {
                onVideoEnd.Invoke();
            }
            else
            {
                Debug.LogWarning("onVideoEnd callback is null.");
            }
            Video.SetActive(false);
        };
    }


}
