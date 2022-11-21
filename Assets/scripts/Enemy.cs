using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lifebar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(lifebar);
        lifebar -= damage;
        if(lifebar <= 0)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
