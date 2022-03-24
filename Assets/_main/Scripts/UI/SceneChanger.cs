using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testSceneReload()
    {
        SceneManager.LoadScene("Dungeon2");
    }

    public void ApplicationQuitButton()
    {
        Debug.Log("does this get called");
        Application.Quit();
    }

    public void LoadTheScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
