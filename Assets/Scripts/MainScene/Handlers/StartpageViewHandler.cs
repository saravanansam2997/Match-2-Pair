using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartpageViewHandler : MonoBehaviour
{
   [SerializeField]
    private GameObject Settingsbtn;
    [SerializeField]
    private GameObject Playbtn;
     [SerializeField]
    private GameObject TitleTxt;
[SerializeField]
    private LevelspageViewHandler LevelspageViewHandlerCS; 
    [SerializeField]
    private SettingspageViewHandler SettingspageViewHandlerCS; 
    // Start is called before the first frame update
    void Start()
    {
        UIButtonAddEvent();
    }
    public void UIButtonAddEvent()
    {
      UIButtonRemoveEvent();
      Playbtn.GetComponent<Button>().onClick.AddListener(() => StartPageButtonClick(Playbtn.name));
      Settingsbtn.GetComponent<Button>().onClick.AddListener(() => StartPageButtonClick(Settingsbtn.name));
      Debug.Log("StartPage UIButtonAddEvent");
    }
    public void UIButtonRemoveEvent()
    {
      Debug.Log("StartPage UIButtonRemoveEvent");
      Playbtn.GetComponent<Button>().onClick.RemoveAllListeners();
      Settingsbtn.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    public void StartPageButtonClick(string tmpbuttonstr)
    {
      SoundManager.Instance.ButtonOnClick();
        if (tmpbuttonstr == "Playbtn")
      {
        //GameManager.Instance.LoadGameScene();

        LevelspageViewHandlerCS.ShowUI();
        Debug.Log("Playbtn");
      }
        if (tmpbuttonstr == "Settingsbtn")
      {
        //GameManager.Instance.LoadGameScene();

        SettingspageViewHandlerCS.ShowUI();
        Debug.Log("Settingsbtn");
      }
    }
        public void HideUI()
    {
      Debug.Log("StartPage HideUI");
      UIButtonRemoveEvent();
      this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
