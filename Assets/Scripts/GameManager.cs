using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    //I try to do it in one scene because WebGL support is still iffy
    public int gameScore;
    public int fallenWords;
    public Canvas canvas; 
    public WordSpawner spawner;
    public TMPro.TextMeshProUGUI countdownText;
    private IEnumerator CountdownCoroutine;
    private IEnumerator ProgressCoroutine; 
    public static GameManager Instance;
    private bool gameIsRunning;
    public GameObject RetryButton;
    public GameObject StartButton;
    public GameObject MenuButton;
    public GameObject inputField;
    public TMPro.TextMeshProUGUI fallenText; 
    public TMPro.TextMeshProUGUI titleText;
    private bool currentFullScreen;
    private bool newFullScreen; 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        StartMenu();
        
        

    }
    public void StartMenu()
    {
        spawner.scoreText.text = "";
        fallenText.text = ""; 
        countdownText.text = ""; 
        StartButton.SetActive(true);
        titleText.text = "言葉かご"; 
        AnimationController.AnimatePopUp(StartButton.gameObject);
        AnimationController.AnimatePopUp(titleText.gameObject);
        RetryButton.SetActive(false);
        MenuButton.SetActive(false);
        inputField.SetActive(false);
        
    }
    public void AddScore()
    {
        gameScore++;    
    }
    public void AddFallenWord() 
    {
        fallenWords++;
        fallenText.text = "落ちた言葉 : " + GameManager.Instance.fallenWords;        
        CheckGameStatus(); 
    }
    private void CheckGameStatus()
    {
        if (fallenWords == 3)
        {
            Debug.Log("U lose");
            StopGame();
        }
    }

    public void StartGame() 
    {
        RetryButton.SetActive(false);
        MenuButton.SetActive(false); 
        StartButton.SetActive(false);
        fallenText.text = "落ちた言葉 : " + GameManager.Instance.fallenWords; 
        titleText.text = ""; 
        spawner.scoreText.text = "スコア：" + GameManager.Instance.gameScore;
        CountdownCoroutine = CountdownGame();
        StartCoroutine(CountdownCoroutine);
        gameIsRunning = true;

    }
    private IEnumerator GameProgress()
    {
        //progressive increase of spawning and falling speed, to make it harder
        while (gameIsRunning)
        {
            yield return new WaitForSeconds(8);
            spawner.SpawnDelay = spawner.SpawnDelay * 0.99f;            
            spawner.WordFallingSpeed = spawner.WordFallingSpeed * 1.01f;
        } 
    }
    private IEnumerator CountdownGame()
    {
        countdownText.fontSize = 36;
        countdownText.transform.position = new Vector3(0, 0, 0); 
        countdownText.text = "3";
        AnimationController.AnimatePopUp(countdownText.gameObject);
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        AnimationController.AnimatePopUp(countdownText.gameObject);
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        AnimationController.AnimatePopUp(countdownText.gameObject);
        yield return new WaitForSeconds(1);
        countdownText.text = "始め！";
        AnimationController.AnimatePopUp(countdownText.gameObject);
        yield return new WaitForSeconds(1);
        AnimationController.AnimatePopUpDisappear(countdownText.gameObject);
        yield return new WaitForSeconds(0.2f);
        countdownText.text = "";
        inputField.SetActive(true);
        AnimationController.AnimatePopUp(inputField.gameObject);
        spawner.StartSpawn();
        ProgressCoroutine = GameProgress();
        StartCoroutine(ProgressCoroutine); 
        StopCoroutine(CountdownCoroutine);
        
    }
    private void StopGame()
    {
        gameIsRunning = false;      
        StopCoroutine(ProgressCoroutine); 
        spawner.StopSpawn();
        countdownText.text = "ゲームオーバー! \n スコア: " + gameScore ;
        countdownText.transform.position = new Vector3(0, 1, 0);
        countdownText.fontSize = 24; 
        RetryButton.SetActive(true);
        MenuButton.SetActive(true);
        inputField.SetActive(false); 
        AnimationController.AnimatePopUp(countdownText.gameObject);
        AnimationController.AnimatePopUp(RetryButton.gameObject);
        AnimationController.AnimatePopUp(MenuButton.gameObject);
        AnimationController.AnimatePopUpDisappear(inputField.gameObject);
        countdownText.fontSize = 24;
        gameScore = 0;
        fallenWords = 0;        
        Debug.Log("Game has stopped"); 
    }
    public void RetryGame()
    {
        StartGame(); 
    }
    private void Update()
    {
        newFullScreen = Screen.fullScreen;
        if (currentFullScreen != newFullScreen)
        { //detects a change in fullscreen-ness
            currentFullScreen = newFullScreen;
           
            
            if (currentFullScreen == true)
            {
                //disable input field
                inputField.GetComponent<TMPro.TMP_InputField>().interactable = false;
                var placeholder = inputField.GetComponent<TMPro.TMP_InputField>().placeholder.GetComponent<TMPro.TextMeshProUGUI>();
                placeholder.text = "Fullscreenを無効にしてください";
                placeholder.color = new Color(255, 0, 0, 128);
                Debug.Log("Disabled input field");
            }
            else 
            {
                
                var placeholder = inputField.GetComponent<TMPro.TMP_InputField>().placeholder.GetComponent<TMPro.TextMeshProUGUI>();
                placeholder.text = "テクストをタイプ";
                placeholder.color = new Color(0, 0, 0, 128);
                inputField.GetComponent<TMPro.TMP_InputField>().interactable = true;
            }
            
        }
        
    }
}
