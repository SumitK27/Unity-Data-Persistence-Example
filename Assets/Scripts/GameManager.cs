using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public int highScore;
    public string playerName;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static string playerName;
    public TextMeshProUGUI inputPlayerName;
    public static SaveData saveData;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadHighScore();
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (saveData != null)
        {
            highScoreText.text = $"Best Score : {saveData.playerName} ({saveData.highScore})";
        }
        else
        {
            highScoreText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        playerName = inputPlayerName.text;
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveHighScore(int score)
    {
        if (saveData != null && score <= saveData.highScore)
        {
            return;
        }

        SaveData data = new SaveData();
        data.highScore = score;
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highScore.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highScore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
    }
}
