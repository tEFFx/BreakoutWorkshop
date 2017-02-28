using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounter : MonoBehaviour {
    public float spriteWidth;

    private Image m_LivesImage;
	
    void Awake()
    {
        m_LivesImage = GetComponent<Image>();
    }

	// Update is called once per frame
	void Update () {
        m_LivesImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, spriteWidth * Ball.lives);
	}
}
