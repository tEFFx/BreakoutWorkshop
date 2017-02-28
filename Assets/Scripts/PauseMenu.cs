using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public static bool isOpen;
    public GameObject menuParent;

    public Button resume;
    public Button restart;
    public Button exit;

    public Ball ball;
    public BrickSpawner spawner;

    void Start()
    {
        SetMenu(false);
        resume.onClick.AddListener(OnResumePressed);
        restart.onClick.AddListener(OnRestartPressed);
        exit.onClick.AddListener(OnExitPressed);
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetMenu(!menuParent.activeSelf);
        }
	}

    void SetMenu(bool active)
    {
        menuParent.SetActive(active);
        Time.timeScale = active ? 0 : 1;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = active;
        isOpen = active;
    }

    void OnResumePressed()
    {
        SetMenu(false);
    }

    void OnRestartPressed()
    {
        ball.Reset();
        spawner.Reset();
        SetMenu(false);
    }

    void OnExitPressed()
    {
        Debug.Log("This will exit the game when we build!");
        Application.Quit();
    }
}
