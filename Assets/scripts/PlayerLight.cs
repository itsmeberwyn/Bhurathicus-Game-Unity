using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public Transform lookAt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = lookAt.position.x;
        float deltaY = lookAt.position.y;

        transform.position = new Vector3(deltaX, deltaY, 0);
    }
}
