using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject CardsParent;
    [SerializeField]
    private GameObject CardPrefab;
     [SerializeField]
    private List<CardHandler> CardsList=new List<CardHandler>();
    [SerializeField]
    private GamePlayViewHandler GamePlayViewHandlerCS;
    [SerializeField]
    private GameCompletedHandler GameCompletedHandlerCS;
    private List<CardHandler> CardClickList=new List<CardHandler>();
    [SerializeField]

    private bool mIscompleted=false;

    private int mCurrentLevel = 0;
    public int mCurrentScore = 0;
    private int totalMatchpairs=0;
    private int currentMatchpair=0;
    public int currentIncorrectCount=0;
    private int currentCorrectCount=0;
    private int CheckMatchNum=2;
       [SerializeField]
    private Sprite emptysp;
    private int mtotallevelNum=10;
    private int scorescale=10;    
    private string mPreviousInputstr="";
[SerializeField]
   private GameObject Canvas;
    

    // Start is called before the first frame update
    void Start()
    {
       LoadLevel();
    }
    public void RestartLevel()
    {
        CardsDestroy();
        currentCorrectCount=0;
        currentIncorrectCount=0;
        currentMatchpair=0;
        totalMatchpairs=0;
        mCurrentScore=0;
        mPreviousInputstr="";
        mIscompleted=false;
        CardClickList.Clear();
        CardsList.Clear();
        GamePlayViewHandlerCS.RestartTimer();
        LoadLevel();
        Debug.Log("LevelManager RestartLevel");
    }
    public int GetTotallevelNumber()
    {
     return mtotallevelNum;
    }
    public int GetCurrentLevelNumber(){
        return mCurrentLevel;
    }
    public void NextLevel()
    {
    mCurrentLevel++;
    GameManager.Instance.SetLeveLNumber(mCurrentLevel);
    RestartLevel();
    }
    public void CardsDestroy()
        {
             Debug.Log("CardsDestroy");
            for (int i = 0; i < CardsParent.transform.childCount; i++)
            {
                Destroy(CardsParent.transform.GetChild(i).gameObject);
            }
        }
    public void LoadLevel()
    {
     GameManager.Instance.IsBgMusicPlay=true;
     mCurrentLevel=GameManager.Instance.GetLeveLNumber();
     GenerateCards((int)GameManager.Instance.GetGridSizeData().RowGrid,(int)GameManager.Instance.GetGridSizeData().ColumnGrid,GenerateRandomCards(GenerateSprites(GameManager.Instance.GetGridSizeData(),mCurrentLevel)),GameManager.Instance.GetGridSizeData());
     SoundManager.Instance.BgMusicPlay();
     GamePlayViewHandlerCS.StartTimer();
     GamePlayViewHandlerCS.UIUpdate();
    }
    public List<Sprite> GenerateSprites(GridSizeData gridSize,int levelnum)
    {
        int totalcardsnum=(int)gridSize.RowGrid*(int)gridSize.ColumnGrid;
        totalMatchpairs=(int)Mathf.Floor(totalcardsnum/2);
        int totalmaxCardsnum=12;
        List<Sprite> tmpsp=new List<Sprite>();
        List<Sprite> tmpsp1=new List<Sprite>();
        tmpsp=GameManager.Instance.GetLevelSprites(totalmaxCardsnum,levelnum);
        for(int i=0;i<totalMatchpairs;i++){
         tmpsp1.Add(tmpsp[i]);
         tmpsp1.Add(tmpsp[i]);
        }
        if(totalcardsnum>tmpsp1.Count)
        {
            tmpsp1.Add(emptysp);
        }
        return tmpsp1;
    }
    public List<Sprite> GenerateRandomCards(List<Sprite> tmpcards){
        List<Sprite> TmpRandomcardssp=new List<Sprite>();
         List<Sprite> TmpRandomcardssp1=new List<Sprite>();
        var rand = new System.Random();
        int tmprand=0;
         TmpRandomcardssp1=tmpcards;
         List<int> listNumbers = new List<int>();
        for(var i=0;i<TmpRandomcardssp1.Count;i++){
        do {
      tmprand=rand.Next(0,TmpRandomcardssp1.Count);
  } while (listNumbers.Contains(tmprand));
         listNumbers.Add(tmprand);
        TmpRandomcardssp.Add(TmpRandomcardssp1[tmprand]);
        }
       return TmpRandomcardssp;
    }

    public void GenerateCards(int row,int col,List<Sprite> sprites=null,GridSizeData gridSize=null){
        Debug.Log(sprites.Count);
        Vector3 cardstartpost=gridSize.gridSize_Position;
        float tmpcardstartposty=0f;
        int Count=0;
         for (int i = 0; i < row; i++)
        {
            cardstartpost.x=gridSize.gridSize_Position.x;
            for (int j = 0; j < col; j++)
             {
                GameObject card = Instantiate(CardPrefab,CardsParent.transform);
                card.transform.parent = CardsParent.transform;
                card.transform.position =cardstartpost;
                card.transform.localScale = new Vector3(gridSize.gridSize_Scale.x, gridSize.gridSize_Scale.y,gridSize.gridSize_Scale.z);
                Vector3 calculatedPosition = new Vector3((gridSize.gridSize_Scale.x+gridSize.gridSize_offset.x), (gridSize.gridSize_Scale.y+gridSize.gridSize_offset.y),0f);
                if(j==0)
                {
                   calculatedPosition.x=0f;
                }
                if(i==0)
                {
                    calculatedPosition.y=0f;
                }
                card.transform.position -=  calculatedPosition;

                cardstartpost.x=card.transform.position.x;
                tmpcardstartposty=card.transform.position.y;
                Color _color=new Color();
                bool isShow=false;
                bool IsInputvaild=false;
               // Debug.Log("sprites[Count]"+sprites[Count].name);
               //  Debug.Log("emptysp"+emptysp.name);
                if(sprites[Count].name==emptysp.name){
                    _color=Color.black;
                    isShow=false;
                    IsInputvaild=false;
                }
                else{
                     _color=Color.white;
                     isShow=true;
                     IsInputvaild=true;
                }
                card.GetComponent<CardHandler>().Init(i,j,sprites[Count],_color,isShow,IsInputvaild,false,false,this);
                CardsList.Add(card.GetComponent<CardHandler>());
                Count++;
        }
              cardstartpost.y=tmpcardstartposty;
        }
    }
    public void InputValidCount(string tmpinputstr,CardHandler tmpcardhandler=null)
    {
        bool isrunMatched=false;
         bool iscompleted=false;
        if(CardClickList.Count>0){
           for(int i=0; i<CardClickList.Count;i++){
              mPreviousInputstr=CardClickList[i].GetSpriteName();
              }
        }
         CardClickList.Add(tmpcardhandler);
        if(tmpinputstr==mPreviousInputstr){
          isrunMatched=true;
          
           mPreviousInputstr="";
          }
        else{
              isrunMatched=false;
             
              
        }
         
              if(CardClickList.Count>=CheckMatchNum){
                  foreach(var tmpcard in CardClickList){
              tmpcard.CardClose(isrunMatched);
              }
              CardClickList.Clear();

             
              if(!isrunMatched){
              currentIncorrectCount++;
              SoundManager.Instance.CardTonePlay(false);
              }
              else
              {
               currentMatchpair++;
               currentCorrectCount++;
               SoundManager.Instance.CardTonePlay(true);
              }
               if(currentMatchpair>=totalMatchpairs){
               iscompleted=true;
               mIscompleted=iscompleted;
              }
              GamePlayViewHandlerCS.UIStatusUpdate(isrunMatched); 
              ScoreUpdate(isrunMatched,iscompleted,tmpcardhandler);
              }
        
       
    }
    public void ScoreUpdate(bool ismatch,bool iscompleted=false,CardHandler tmpcardhandler=null){
    int tmpscorescale=scorescale;
    if(iscompleted){
int extrachanceclickCount=(int)Mathf.Floor(totalMatchpairs/2);
    extrachanceclickCount=extrachanceclickCount-1;
    if(extrachanceclickCount>currentIncorrectCount){
       tmpscorescale=(totalMatchpairs-currentIncorrectCount)*(tmpscorescale/2);
    }
      
    }
      
     // UIUpdate();
     string addstr=tmpscorescale.ToString();
     if(ismatch){
    addstr="+"+addstr;
    GameManager.Instance.SetScore(GameManager.Instance.GetScore()+tmpscorescale);
     }
     else{
        tmpscorescale=3;
        addstr=tmpscorescale.ToString();
       addstr="-"+addstr; 
       GameManager.Instance.SetScore(GameManager.Instance.GetScore()-tmpscorescale);
     }
      UIUpdate();
     //AnimationManager.Instance.ScoreAnimation(ismatch,addstr,Canvas,tmpcardhandler.gameObject,GamePlayViewHandlerCS.GetScoreText(),() => { UIUpdate();});
    }
    public void UIUpdate(){
     GamePlayViewHandlerCS.UIUpdate();
     if(mIscompleted)
     {
     SoundManager.Instance.BgMusicStop();
     GameManager.Instance.IsBgMusicPlay=false;
     GamePlayViewHandlerCS.StopTimer();
     StartCoroutine(GameCompletedShowUITimer());
   
     }
    }
    private IEnumerator GameCompletedShowUITimer(){
       yield return new WaitForSeconds(1.5f);
        GameCompletedHandlerCS.ShowUI();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
