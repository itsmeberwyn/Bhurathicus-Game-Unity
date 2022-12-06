using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource death;
    public AudioSource takeDamage;
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
        takeDamage.Play();
        lifebar -= damage;
        if(lifebar <= 0)
        {
            death.Play();
            Object.Destroy(this.gameObject);
            return 1;
        }
        return 0;
    }
}
