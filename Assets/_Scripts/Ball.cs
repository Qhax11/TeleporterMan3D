using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Ball : MonoBehaviour
{
    Vector3 v3 = new Vector3(0,0,1);
    void FixedUpdate()
    {
        if (PlayerController.Instance.ballTrigger)
        {
            transform.position -= v3 * Time.deltaTime * 5;
            transform.Rotate(-100 * Time.deltaTime, 0, 0);
        }
   
    }
}
