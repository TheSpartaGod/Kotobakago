using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordObject : MonoBehaviour
{
    public string WordText;
    public TMPro.TextMeshProUGUI textContainer;

   
    private void Start()
    {
        textContainer.text = WordText; 
    }
  
}
