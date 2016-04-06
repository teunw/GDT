using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button startButton;
    public Button exitButton;
    public string GameScene;

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
	}
}
