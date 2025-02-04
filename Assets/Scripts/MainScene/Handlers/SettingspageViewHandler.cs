using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingspageViewHandler : MonoBehaviour
{
  [SerializeField]
  private GameObject Musicbtn;
  [SerializeField]
  private GameObject Soundbtn; 
  [SerializeField]
  private GameObject Homebtn; 
  [SerializeField]
   private GameObject Closebtn; 
    // Start is called before the first frame update
    // void Start()
    // {
    //   //  UIButtonAddEvent();
    // }
    public void ShowUI(){
this.gameObject.SetActive(true);
 UIUpdated();
      UIButtonAddEvent();
      GameManager.Instance.isPopUpWindows=true;
    }
    public void UIButtonAddEvent()
    {
     
      UIButtonRemoveEvent();
      Musicbtn.GetComponent<Button>().onClick.AddListener(() => SettingPageButtonClick(Musicbtn.name));
      Soundbtn.GetComponent<Button>().onClick.AddListener(() => SettingPageButtonClick(Soundbtn.name));
      Homebtn.GetComponent<Button>().onClick.AddListener(() => SettingPageButtonClick(Homebtn.name));
      Closebtn.GetComponent<Button>().onClick.AddListener(() => SettingPageButtonClick(Closebtn.name));
      Debug.Log("SettingPage UIButtonAddEvent");
    }
    public void UIButtonRemoveEvent()
    {
      Debug.Log("SettingPage UIButtonRemoveEvent");
      Musicbtn.GetComponent<Button>().onClick.RemoveAllListeners();
      Soundbtn.GetComponent<Button>().onClick.RemoveAllListeners();
      Homebtn.GetComponent<Button>().onClick.RemoveAllListeners();
      Closebtn.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    public void SettingPageButtonClick(string tmpbuttonstr)
    {
        SoundManager.Instance.ButtonOnClick();
        if (tmpbuttonstr == "Soundbtn")
        {
            SoundManager.Instance.SetIsSound(!SoundManager.Instance.GetIsSound());
            if (SoundManager.Instance.GetIsSound() == true)
            {
                Soundbtn.transform.Find("Symbol").GetChild(0).gameObject.SetActive(false);
            }
            else if (SoundManager.Instance.GetIsSound() == false)
            {
                Soundbtn.transform.Find("Symbol").GetChild(0).gameObject.SetActive(true);
            }
            Debug.Log("Soundbtn");
        }
        if (tmpbuttonstr == "Musicbtn")
        {
            SoundManager.Instance.SetIsMusic(!SoundManager.Instance.GetIsMusic());
            if (SoundManager.Instance.GetIsMusic() == true)
            {
                Musicbtn.transform.Find("Symbol").GetChild(0).gameObject.SetActive(false);
            }
            else if (SoundManager.Instance.GetIsMusic() == false)
            {
                Musicbtn.transform.Find("Symbol").GetChild(0).gameObject.SetActive(true);
            }
            Debug.Log("Musicbtn");
        }
      if (tmpbuttonstr == "Homebtn")
      {
         GameManager.Instance.isPopUpWindows=false;
        GameManager.Instance.LoadSceneasync("MainScene");
        Debug.Log("Homebtn");
      }
      if (tmpbuttonstr == "Closebtn")
      {
         GameManager.Instance.isPopUpWindows=false;
           HideUI();
           Debug.Log("Closebtn");
      }
    }
    public void UIUpdated()
    {
      if (SceneManager.GetActiveScene().name == "MainScene")
            {
              Homebtn.SetActive(false);
            }
        if (SoundManager.Instance.GetIsSound() == true)
        {
            Soundbtn.transform.Find("Symbol").GetChild(0).gameObject.SetActive(false);
        }
        else if (SoundManager.Instance.GetIsSound() == false)
        {
            Soundbtn.transform.Find("Symbol").GetChild(0).gameObject.SetActive(true);
        }
        if (SoundManager.Instance.GetIsMusic() == true)
        {
            Musicbtn.transform.Find("Symbol").GetChild(0).gameObject.SetActive(false);
        }
        else if (SoundManager.Instance.GetIsMusic() == false)
        {
            Musicbtn.transform.Find("Symbol").GetChild(0).gameObject.SetActive(true);
        }
    }
        public void HideUI()
    {
      GameManager.Instance.isPopUpWindows=false;
      Debug.Log("StartPage HideUI");
      UIButtonRemoveEvent();
      this.gameObject.SetActive(false);   
    }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
