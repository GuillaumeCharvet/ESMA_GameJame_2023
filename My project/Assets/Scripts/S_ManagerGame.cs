using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_ManagerGame : MonoBehaviour
{
    private void Start()
    {
       // Time.timeScale = 0f;
    }
    private void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {

    }

    public void ReloadGame()
    {
        Scene _scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_scene.name);
    }

}
