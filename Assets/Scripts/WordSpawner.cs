using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    public WordObject wordObject;
    public Canvas canvas;
    public WordBank wordBank;
    public Camera cam;
    public List<WordObject> spawnedWords;
    public TMPro.TextMeshProUGUI scoreText; 
    public float SpawnDelay;
    public float WordFallingSpeed;
    
    private IEnumerator SpawnCoroutine;

    public void StartSpawn()
    {
        scoreText.text = "スコア：" + GameManager.Instance.gameScore;
        SpawnCoroutine = GenerateWordObject(); //set coroutine to object as reference so we can delete it later
        StartCoroutine(SpawnCoroutine);
        
    }
    public void StopSpawn()
    {
        StopCoroutine(SpawnCoroutine);
        for (int i = 0; i < spawnedWords.Count; i++) 
        {
            Destroy(spawnedWords[i].gameObject); 
        }
        spawnedWords.Clear(); 
    }
    private string GenerateWord() 
    {
        string word; 
        do
        {
           word = wordBank.WordListEasy[Random.Range(0, wordBank.WordListEasy.Count - 1)];
        } while (WordHasSpawned(word) == true);

        return word; 
    
    }
    private bool WordHasSpawned(string word) 
    {
        // prevents same words from being spawned twice in one cache
        if (wordBank.WordCache.Contains(word)) return true;
        else 
        {
            wordBank.AddSpawnedWord(word);
            return false;
        }
        

    }
    public void DeleteWordObject(WordObject wordObject) 
    {
        for (int i = 0; i < spawnedWords.Count; i++)
        {
            if (spawnedWords[i] == wordObject)
            {
                spawnedWords.RemoveAt(i);
                Debug.Log("Removed word object cache index: " + i + " with word: " + wordObject.WordText);
                break;
            }
        }
    }
     public void GenerateWordCatchEffect(Vector3 position) 
    {
        wordObject.WordText = "+1";
        WordObject effectObject = wordObject; 
        
        WordObject newEffect =  Instantiate(effectObject, position, Quaternion.identity, this.transform);
        AnimationController.AnimatePopUp(newEffect.gameObject); 
        //removes the collision and gravity from the object
        Destroy(newEffect.GetComponent<Rigidbody2D>());
        Destroy(newEffect.GetComponent<BoxCollider2D>());
        StartCoroutine(DeleteEffect(newEffect.gameObject)); 
        

    }
    private IEnumerator DeleteEffect(GameObject obj) {
        yield return new WaitForSeconds(1.5f);
        AnimationController.AnimatePopUpDisappear(obj);
        yield return new WaitForSeconds(0.2f); 
        Destroy(obj);
    }

    IEnumerator GenerateWordObject()
    {
        for (int i = 0; i < 100; i++)
        {   
            //determine spawning coordinates based on camera bounds
            float minX = cam.transform.position.x - (cam.orthographicSize * cam.aspect) / 2f;
            float maxX = cam.transform.position.x + (cam.orthographicSize * cam.aspect) / 2f;
            float xPos = Random.Range(minX, maxX);

            wordObject.WordText = GenerateWord();
            wordObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = WordFallingSpeed; 
            spawnedWords.Add(Instantiate(wordObject, new Vector3(xPos, this.transform.position.y, 0), Quaternion.identity, this.transform));
            AnimationController.AnimatePopUp(spawnedWords[spawnedWords.Count - 1].gameObject); 

            //wait a delay before spawning a new word
            yield return new WaitForSeconds(SpawnDelay);
        }
    } 
}
