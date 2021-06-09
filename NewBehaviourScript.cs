﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{

    public Button button;
    public string newstring;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnButton);

    }

    void OnButton()
    {
        SceneManager.LoadScene(newstring);
    }
}
