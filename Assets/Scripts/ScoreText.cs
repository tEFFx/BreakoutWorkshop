using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    private Text m_Text;

    void Awake()
    {
        m_Text = GetComponent<Text>();
    }

    void Update()
    {
        m_Text.text = "SCORE: " + Ball.score;
    }
}
