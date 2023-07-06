using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    private int firstSceneIndex = 1;

    
    public void StartGame()
    {
        SceneManager.LoadScene(firstSceneIndex);
    }
    

}
