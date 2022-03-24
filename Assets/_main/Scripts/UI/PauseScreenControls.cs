using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions;

public class PauseScreenControls : MonoBehaviour
{
    public GameObject PauseScreen;
    private bool requestPause { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        requestPause = false;

        Assert.IsNotNull(PauseScreen, "Object needs a reference to the pause screen");
        PauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(requestPause)
        {
            TogglePause();
        }

        requestPause = false;
    }

    void OnPause(InputValue value)
    {
        requestPause = value.Get<float>() != 0;
    }

    void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            //Unpause game
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
        }
        else
        {
            //Pause game
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
        }
    }
}
