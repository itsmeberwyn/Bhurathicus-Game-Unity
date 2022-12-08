using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BTNclick : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void exitClick()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
