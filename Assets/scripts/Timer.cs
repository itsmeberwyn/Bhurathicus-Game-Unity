using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public int Duration;
    private int remainingDuration;

    // Start is called before the first frame update
    void Start()
    {
        Being(Duration);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Being(int Second)
    { 
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(remainingDuration >= 0)
        {
            timerText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
            remainingDuration++;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd()
    {

    }
}
