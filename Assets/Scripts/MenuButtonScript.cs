using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{
    public void RetryGame()
    {
        GameManager.Instance.RetryGame();
    }
    public void StartGame()
    {
        GameManager.Instance.StartGame(); 
    }
    public void StartMenu()
    {
        GameManager.Instance.StartMenu();
    }

}
