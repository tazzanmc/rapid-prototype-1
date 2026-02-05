using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SceneChange : MonoBehaviour
{
    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
