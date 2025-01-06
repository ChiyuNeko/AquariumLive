[System.Serializable]
public class CharacterData
{
    public string characterName;
    public string iconPath; // 圖標的路徑
    public float probability;
}

[System.Serializable]
public class CharacterPoolData
{
    public CharacterData[] characters;
}
