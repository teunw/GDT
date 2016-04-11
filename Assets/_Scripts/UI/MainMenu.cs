using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button startButton;
    public Button exitButton;
    public string GameScene;

    public Text WinnerText, LoserText;

	// Use this for initialization
	void Start () {
	    startButton.onClick.AddListener(() =>
	    {
	        SceneManager.LoadScene(GameScene);
	    });
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
	    WinnerText.text = "Winner";
	    LoserText.text = "Loser";
	}

    void Update()
    {
        if (GameManager.getInstance() != null)
        {
            if (GameManager.getInstance().Winner != null)
            {
                Debug.Log("test2");
                WinnerText.color = GameManager.getInstance().Winner.unitColor;
            }
            if (GameManager.getInstance().Loser != null)
            {
                Debug.Log("test");
                LoserText.color = GameManager.getInstance().Loser.unitColor;
            }
        }
    }
}
