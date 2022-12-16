using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadRecord : MonoBehaviour
{
    public TextMeshProUGUI record;
    // Start is called before the first frame update
    void Start()
    {
        string record = PlayerPrefs.GetString("record");
        this.record.text = $"Record: {record}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
