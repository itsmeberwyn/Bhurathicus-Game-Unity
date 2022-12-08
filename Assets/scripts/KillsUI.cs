using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillsUI : MonoBehaviour
{
    public Text kills;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        kills.text = "Kills: " + FindObjectOfType<PlayerAttack>()?.kill;
    }
}
