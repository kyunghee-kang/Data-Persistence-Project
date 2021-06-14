using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DataPersistManager : MonoBehaviour
{
    public static DataPersistManager Instance;

    public string playerName = null;

    public string highestScorePlayerName = null;
    public int highestScore = 0;
   

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScoreData();
    }

    public void SetPlayer(string name)
    {
        // lookup dictionary and find high score
        playerName = name;
    }

    public void RegisterScore(int score)
    {
        if (highestScore < score)
        {
            Debug.Log("replace " + highestScorePlayerName + "(" + highestScore + ") -> " + playerName + "(" + score + " )");

            highestScore = score;
            highestScorePlayerName = playerName;
        }

        SaveScoreData();
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public string GetHighestScorePlayerName()
    {
        return highestScorePlayerName;
    }

    public int GetHighestScore()
    {
        return highestScore;
    }

    [System.Serializable]
    class SaveData
    {
        public string lastPlayerName = null;
        public string highestScorePlayerName = null;
        public int highestScore = 0;
    }

    public void SaveScoreData()
    {
        SaveData data = new SaveData();
        data.lastPlayerName = playerName;
        data.highestScorePlayerName = highestScorePlayerName;
        data.highestScore = highestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            this.playerName = data.lastPlayerName;
            this.highestScorePlayerName = data.highestScorePlayerName;
            this.highestScore = data.highestScore;
        }
    }
}
