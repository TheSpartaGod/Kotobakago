using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTextCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public WordSpawner spawner; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Text has gotten outside of reach");
        spawner.wordBank.RemoveSpawnedWord(collision.gameObject.GetComponent<WordObject>().WordText);
        spawner.DeleteWordObject(collision.gameObject.GetComponent<WordObject>()); 
        Destroy(collision.gameObject);
        GameManager.Instance.AddFallenWord(); 

    }
}
