using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameBtnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitClick()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void retryClick()
    {
        SceneManager.LoadScene("Main");

    }
}
