using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevelByName(string sceneName)
    {
        if(sceneName == "Exit")
        {
            Application.Quit();
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevelByID(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
