using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttoncontroller : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    private void Awake()
    {
        button1.onClick.AddListener(Startgame);
        button2.onClick.AddListener(Endgame);
        button3.onClick.AddListener(Currentgame);
    }
   void Startgame()
    {
        SceneManager.LoadScene("Level1");
    }

    void Endgame()
    {
        Application.Quit();
    }

    void Currentgame()
    {
        SceneManager.LoadScene(Currentscene.currentscene);
    }
}
