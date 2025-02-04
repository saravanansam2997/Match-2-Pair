using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelspageViewHandler : MonoBehaviour
{
     [SerializeField]
    private GameObject GridSizeViewUI;
     [SerializeField]
    private GameObject LevelsViewUI;
    [SerializeField]
    private GameObject Backbtn;
    [SerializeField]
    private GameObject Settingsbtn;
    [SerializeField]
    private List<Button> GridSizeButtonList;
       [SerializeField]
    private List<Button> LevelsButtonList;
     [SerializeField]
    private SettingspageViewHandler SettingspageViewHandlerCS;
    // Start is called before the first frame update
    // void Start()
    // {
    //    // UIButtonAddEvent();
    // }
    public void ShowUI(){
      this.gameObject.SetActive(true);
      GridSizeViewUI.SetActive(true);
      LevelsViewUI.SetActive(false);
      UIButtonAddEvent();
    }
    public void UIButtonAddEvent()
    {
      UIButtonRemoveEvent();
      for ( int i=0;i<GridSizeButtonList.Count;i++)
      {
        string tmpgridstr=GridSizeButtonList[i].gameObject.name;
         GridSizeButtonList[i].onClick.AddListener(() => LevelsPageButtonClick(tmpgridstr));
      } 
      for ( int i=0;i<LevelsButtonList.Count;i++)
      {
        string tmplevelstr=LevelsButtonList[i].gameObject.name;
         LevelsButtonList[i].onClick.AddListener(() => LevelsPageButtonClick(tmplevelstr));
      } 
      Backbtn.GetComponent<Button>().onClick.AddListener(() => LevelsPageButtonClick(Backbtn.name));
      Settingsbtn.GetComponent<Button>().onClick.AddListener(() => LevelsPageButtonClick(Settingsbtn.name));
      Debug.Log("LevelPage UIButtonAddEvent");
    }
    public void UIButtonRemoveEvent()
    {
      Debug.Log("LevelPage UIButtonRemoveEvent");
       for ( int i=0;i<GridSizeButtonList.Count;i++)
      {
         GridSizeButtonList[i].onClick.RemoveAllListeners();
      } 
      for ( int i=0;i<LevelsButtonList.Count;i++)
      {
         LevelsButtonList[i].onClick.RemoveAllListeners();
      } 
      Backbtn.GetComponent<Button>().onClick.RemoveAllListeners();
      Settingsbtn.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    public void LevelsPageButtonClick(string tmpbuttonstr)
    {
      SoundManager.Instance.ButtonOnClick();
      Debug.Log(tmpbuttonstr);
        if (tmpbuttonstr == "Backbtn")
      {
        if(GridSizeViewUI.activeSelf==true)
        {
        HideUI();
        }
        else{
          GridSizeViewUI.SetActive(true);
          LevelsViewUI.SetActive(false);
        }
        Debug.Log("Backbtn");
      }
      if (tmpbuttonstr == "Settingsbtn")
      {
        SettingspageViewHandlerCS.ShowUI();
        Debug.Log("Settingsbtn");
      }
      if (tmpbuttonstr.Contains("X"))
      {
        int tmpintp0=0;
        int tmpintp1=0;
        string tmpgridstr= tmpbuttonstr[0].ToString();
        string tmpgrid1str= tmpbuttonstr[2].ToString();
        int.TryParse(tmpgridstr, out tmpintp0);
        int.TryParse(tmpgrid1str, out tmpintp1);
        GameManager.Instance.SetGridSizeData(tmpintp0,tmpintp1);
        GridSizeViewUI.SetActive(false);
        LevelsViewUI.SetActive(true);
        Debug.Log("Grid Szie"+tmpbuttonstr);
      }
      if (tmpbuttonstr.Contains("Level"))
      {
        int tmpintp0=0;
        string tmplevelstr=tmpbuttonstr.Substring(5);
        int.TryParse(tmplevelstr, out tmpintp0);
         Debug.Log("Level ---"+tmpintp0);
        GameManager.Instance.SetLeveLNumber(tmpintp0);
        GameManager.Instance.LoadGameScene();
        Debug.Log("Level ---"+tmplevelstr);
      }
    }
        public void HideUI()
    {
      Debug.Log("LevelsPage HideUI");
      UIButtonRemoveEvent();
      this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
