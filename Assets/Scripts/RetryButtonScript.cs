using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButtonScript : MonoBehaviour
{
    public void RetryGame()
    {
        GameManager.Instance.RetryGame(); 
    }

}
