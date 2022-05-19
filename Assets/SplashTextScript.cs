using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SplashTextScript : MonoBehaviour
{
    public Animator transition; 
    public TMPro.TextMeshProUGUI splashText;
    IEnumerator SplashScreenCoroutine; 
    void Awake()
    {
        SplashScreenCoroutine = SplashScreen();
        StartCoroutine(SplashScreenCoroutine); 
    }
    private IEnumerator SplashScreen()
    {
         
        yield return new WaitForSeconds(5);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);           
    }
}
