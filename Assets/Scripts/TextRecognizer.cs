using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRecognizer : MonoBehaviour
{
    public string text;
    public TMPro.TMP_InputField inputField;
    public WordSpawner spawner;


    public void RecognizeText() {
        List<string> wordCache = spawner.wordBank.WordCache;
        if (wordCache.Contains(inputField.text))
            Debug.Log("Word Matched!");     
            //removes text from input field and spawned words when inputted text is the same as spawned words. 
            //TODO: add some point system
        {
            for (int i = 0; i < wordCache.Count; i++)
            {
                if (wordCache[i] == inputField.text)
                {   
                    spawner.wordBank.WordCache.RemoveAt(i);
                    spawner.GenerateWordCatchEffect(spawner.spawnedWords[i].gameObject.transform.position);
                    Destroy(spawner.spawnedWords[i].gameObject);
                    spawner.spawnedWords.RemoveAt(i);
                    GameManager.Instance.AddScore(); 
                    spawner.scoreText.text = "スコア：" + GameManager.Instance.gameScore;

                    inputField.text = "";
                }
            }
        }
        
            
    }

}
