using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class ScoreController1 : MonoBehaviour
{
    private int score = 0;
    private TextMeshProUGUI textmeshpro;
    public void Updatescore()
    {
        score += 10;
        textmeshpro.text = "Score: " + score;
    }
    private void Awake()
    {
        textmeshpro = gameObject.GetComponent<TextMeshProUGUI>();
    }
}
