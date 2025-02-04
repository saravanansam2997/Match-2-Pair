using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayViewHandler : MonoBehaviour
{
    //UI Elements
    [SerializeField]
    private GameObject StatusTxt;
    [SerializeField]
    private GameObject ScoreTxt;
    [SerializeField]
    private GameObject TimeTxt;
    [SerializeField]
    private GameObject Settingsbtn;
      [SerializeField]
    private GameObject LevelTxt;
    private int mCoinNum;
    private int mLevelNum;
    private float mLevelProgress;
    public float totalTime=0f;
    public float mcurrentTime=0f;
    public bool IsStartTimer;
     [SerializeField]
    private LevelManager LevelManagerCS;
    [SerializeField]
    private SettingspageViewHandler SettingspageViewHandlerCS;
    // Start is called before the first frame update
     void Start()
    {
        StatusTxt.GetComponent<TMP_Text>().text="";
        UIButtonAddEvent();
        UIUpdate();
    }
    public void UIUpdate()
    {
      LevelTxt.GetComponent<TMP_Text>().text="Level "+GameManager.Instance.GetLeveLNumber().ToString();
      UIScoreUpdate();
      Debug.Log("GamePlayPage UIUpdate");
    }
    public void UIScoreUpdate(){
      ScoreTxt.GetComponent<TMP_Text>().text="Score : "+GameManager.Instance.GetScore().ToString();
    }
    public void UIStatusUpdate(bool ismatch=false){
      string statuspair="";
      if(ismatch){
       statuspair="You Found Match Pair";
      }
      else{
        statuspair="Not Match Pair";
      }
      StartCoroutine(UIStatusUpdateTimer(statuspair));
    }
    private IEnumerator UIStatusUpdateTimer(string statuspair=""){
       StatusTxt.GetComponent<TMP_Text>().text=statuspair;
        yield return new WaitForSeconds(1.5f);
       StatusTxt.GetComponent<TMP_Text>().text="";
    }
public void UIButtonAddEvent()
    {
      UIButtonRemoveEvent();
      Settingsbtn.GetComponent<Button>().onClick.AddListener(() => GamePlayPageButtonClick(Settingsbtn.name));
      Debug.Log("GamePlayPage UIButtonAddEvent");
    }
     public void UIButtonRemoveEvent()
    {
      Debug.Log("GamePlayPage UIButtonRemoveEvent");
      //Resetbtn.GetComponent<Button>().onClick.RemoveAllListeners();
    }
     public void GamePlayPageButtonClick(string tmpbuttonstr)
    {
        SoundManager.Instance.ButtonOnClick();
        if (tmpbuttonstr == "Settingsbtn")
      {
        SettingspageViewHandlerCS.ShowUI();
       // LevelManagerCS.ResetLevel();
        Debug.Log("Settingsbtn");
      }
    }
        public void HideUI()
    {
      Debug.Log("GamePlayPage HideUI");
      UIButtonRemoveEvent();
      this.gameObject.SetActive(false);
    }
    public GameObject GetScoreText(){
      return ScoreTxt;
    }
    public void StartTimer()
    {
      IsStartTimer=true;
    }
    public void StopTimer()
    {
       IsStartTimer=false;
      mcurrentTime=totalTime;
    }
    public void RestartTimer(){
      IsStartTimer=false;
      totalTime=0f;
      mcurrentTime=totalTime;
    }

    // // Update is called once per frame
     void Update()
     {
       if (IsStartTimer)
    {
      // Subtract elapsed time every frame
      totalTime += Time.deltaTime;

      // Divide the time by 60
      float minutes = Mathf.Floor(totalTime / 60); 
      
      // Returns the remainder
      float seconds = Mathf.Floor(totalTime % 60);

      // Set the text string
      TimeTxt.GetComponent<TMP_Text>().text = String.Format("{0:00}:{1:00}",minutes, seconds);    
      }
    
     }
}
