using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuHandler : MonoBehaviour
{
    public Text bestScoreText;
    public InputField playerInputField;
   // private DataPersistManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        SetBestScoreText();
        if (DataPersistManager.Instance != null)
        {
            playerInputField.text = DataPersistManager.Instance.GetPlayerName();
        }
    }

    public void SetBestScoreText()
    {
        string playerName = null;
        int highScore = 0;

        if (DataPersistManager.Instance != null)
        {
            playerName = DataPersistManager.Instance.GetHighestScorePlayerName();
            highScore = DataPersistManager.Instance.GetHighestScore();
        }

        string outText = "Best Score : ";

        if (playerName != null)
        {
            outText += playerName;
        }

        outText += " : " + highScore;
        bestScoreText.text = outText;
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
    public void OnPlayerNameChanged()
    {
        //        InputField playerinputfield = GetComponent<InputField>();
        string playerName = playerInputField.text;

        if (DataPersistManager.Instance != null)
        {
            DataPersistManager.Instance.SetPlayer(playerName);
        }
    }
}
