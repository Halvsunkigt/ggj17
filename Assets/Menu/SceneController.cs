using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public bool splash = false;
    public float splashDelay = 0;

    public string levelToLoad;

    // Use this for initialization
    void Start()
    {
        if (splash)
            Invoke("LoadNextScene", splashDelay);

    }

    void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        //TODO: add delay and turn off animation.
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1") && levelToLoad != "")
        {
            if (levelToLoad == "Quit")
            {
                Quit();
                return;
            }
            LoadLevel(levelToLoad);
        }
    }

}
