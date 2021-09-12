using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{

    [SerializeField] float time;

    void Start()
    {
        StartCoroutine(SplashScreenTime());
    }

    IEnumerator SplashScreenTime()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(1);
    }

}
