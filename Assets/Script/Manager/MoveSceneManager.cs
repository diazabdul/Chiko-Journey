using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneManager : MonoBehaviour
{
    public string LoadScene;

    public void ChangeSceneTo()
    {
        SceneManager.LoadScene(LoadScene);
    }
}
