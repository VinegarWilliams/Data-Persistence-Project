using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI highScoreText;
    public TMP_InputField nameInput;
    public Button startButton;
    public Button quitButton;

    public DataMover dataMover;

    public int savedHighScore;
    public string savedPlayerName;

    public string playerName;

    // Start is called before the first frame update
    void Start()
    {
        dataMover = GameObject.Find("DataMover").GetComponent<DataMover>();
        savedHighScore = dataMover.savedHighScore;
        savedPlayerName = dataMover.savedPlayerName;
        highScoreText.SetText(@$"Highest Score: {savedPlayerName} {savedHighScore}");

    }

    // Update is called once per frame
    void Update()
    {
        playerName = nameInput.text;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    public void OnApplicationQuit()
    {
        dataMover.SaveHighScore();
        //dataMover.SavePlayerName();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
