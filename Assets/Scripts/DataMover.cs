using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataMover : MonoBehaviour
{
    public static DataMover Instance;
    public UIManager uIManager;
    public MainManager mainManager;

    public int savedHighScore;
    public int score;
    public string savedPlayerName;
    public string playerName;

    private bool isGameScene = false;

    [System.Serializable]
    class SaveData
    {
        public int savedHighScore;
        public string savedPlayerName;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
        //LoadPlayerName();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameScene)
        {
            score = mainManager.m_Points;

            if (score > savedHighScore)
            {
                savedHighScore = score;
                savedPlayerName = playerName;
            }
        }

        GetConnections();
    }

    private void GetConnections()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (uIManager == null)
            {
                uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
                isGameScene = false;

            }

            playerName = uIManager.playerName;
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (mainManager == null)
            {
                mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
                isGameScene = true;
            }

        }

    }
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.savedHighScore = savedHighScore;
        data.savedPlayerName = savedPlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            savedHighScore = data.savedHighScore;
            savedPlayerName = data.savedPlayerName;
        }
    }
    /*public void SavePlayerName()
    {
        SaveData data = new SaveData();


        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);


        }
    }*/
}
