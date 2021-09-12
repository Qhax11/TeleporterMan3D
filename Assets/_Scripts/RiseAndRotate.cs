using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseAndRotate : MonoBehaviour
{
    bool up=true, down=false;
    private float playerY;
    public float riseValue;
    public float rotationValue;
    private void Start()
    {
        playerY = transform.position.y;
    }
    void Update()
    {
        if (transform.position.y > playerY + riseValue)
        {
            up = false;
            down = true;
        }
        else if (transform.position.y < playerY )
        {
            up = true;
            down = false;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, -rotationValue * Time.deltaTime, 0);
        if (up)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * riseValue, transform.position.z);
        }
        if (down)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * riseValue, transform.position.z);
        }
    }
}
