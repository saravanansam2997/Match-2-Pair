using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCompletedHandler : MonoBehaviour
{
   [SerializeField]
    private GameObject mTotalScoreTxt;
    [SerializeField]
    private GameObject mCurrentTimeTxt;
     [SerializeField]
    private GameObject mCurrentInCorrectTxt;
    [SerializeField]
    private GameObject Replaybtn;
    [SerializeField]
    private GameObject Nextbtn;
    [SerializeField]
    private GameObject Homebtn;
      [SerializeField]
    private GamePlayViewHandler GamePlayViewHandlerCS;
     [SerializeField]
    private LevelManager LevelManagerCS;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }
    public void ShowUI()
    {
          this.gameObject.SetActive(true);
          UIButtonAddEvent();
          UIUpdate();
          Debug.Log("GameCompleted ShowUI");
    }
    
    public void UIUpdate()
    {
     mTotalScoreTxt.GetComponent<TMP_Text>().text="Total Score : "+GameManager.Instance.GetScore().ToString();
      float minutes = Mathf.Floor(+GamePlayViewHandlerCS.mcurrentTime / 60); 
      // Returns the remainder
      float seconds = Mathf.Floor(+GamePlayViewHandlerCS.mcurrentTime % 60);
     mCurrentTimeTxt.GetComponent<TMP_Text>().text=String.Format("Time : {0:00}:{1:00}",minutes, seconds).ToString();    
     mCurrentInCorrectTxt.GetComponent<TMP_Text>().text="Wrong Count : "+LevelManagerCS.currentIncorrectCount.ToString();
    }
    public void UIButtonAddEvent()
    {
      UIButtonRemoveEvent();
      Replaybtn.GetComponent<Button>().onClick.AddListener(() => GameCompletedButtonClick(Replaybtn.name));
      if(LevelManagerCS.GetTotallevelNumber()>LevelManagerCS.GetCurrentLevelNumber())
      {
      Nextbtn.GetComponent<Button>().onClick.AddListener(() => GameCompletedButtonClick(Nextbtn.name));
      }
      else{
        Nextbtn.SetActive(false);
      }
      Homebtn.GetComponent<Button>().onClick.AddListener(() => GameCompletedButtonClick(Homebtn.name));
     // Debug.Log("GameCompleted UIButtonAddEvent");
    }
    public void UIButtonRemoveEvent()
    {
     // Debug.Log("GameCompleted UIButtonRemoveEvent");
      Replaybtn.GetComponent<Button>().onClick.RemoveAllListeners();
      Nextbtn.GetComponent<Button>().onClick.RemoveAllListeners();
      Homebtn.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    void GameCompletedButtonClick(string tmpbuttonstr)
    {
        if (tmpbuttonstr == "Replaybtn")
      {
        LevelManagerCS.RestartLevel();
        HideUI();
        Debug.Log("Replaybtn");
      }
       if (tmpbuttonstr == "Nextbtn")
      {
        UIButtonRemoveEvent();
        LevelManagerCS.NextLevel();
        HideUI();
        Debug.Log("Nextbtn");
      }
      if (tmpbuttonstr == "Homebtn")
      {
        GameManager.Instance.LoadSceneasync("MainScene");
        Debug.Log("Homebtn");
      }
    }
    public void HideUI()
    {
      Debug.Log("GameCompleted HideUI");
      UIButtonRemoveEvent();
      this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
