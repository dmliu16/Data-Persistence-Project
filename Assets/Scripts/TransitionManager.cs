using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;
    public string userName;
    public int finalScore;

    public string highestScoreUserName;
    public int highestScore;

    private void Awake()
    {   
        // Check if Instance is already initiated to avoid duplicating MainManager
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    class SaveData
    {
        public string highestScoreUserName;
        public int highestScore;
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highestScoreUserName = data.highestScoreUserName;
            highestScore = data.highestScore;
        }
        Debug.Log(highestScoreUserName + ": " + highestScore);
    }

    public void SaveHighScore()
    {
        // if current user beat the highest score or there is no highest score
        if (finalScore > highestScore || (highestScoreUserName == "" && highestScore == 0))
        {
            highestScore = finalScore;
            highestScoreUserName = userName;
            SaveData data = new SaveData();
            data.highestScoreUserName = highestScoreUserName;
            data.highestScore = highestScore;
            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }
}
