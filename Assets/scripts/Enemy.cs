using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    public int TakeDamage(int damage)
    {
        lifebar -= damage;
        if(lifebar <= 0)
        {
            Object.Destroy(this.gameObject);
            return 1;
        }
        return 0;
    }
}
