using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    private int mLevelNum=1;
    private int mScore=0;
    private GridSizeData mCurrentLevelGridSizeData;
    private Difficulty mCurrentLevelDifficulty;
      public bool IsBgMusicPlay=false;
    private GameInfoData GameInfoDataCS;
    public bool isPopUpWindows=false;
    [SerializeField]
    private LevelsManagerScriptableObject LevelsManagerScriptableObjectCS;
    private static GameManager _instance;
        public static GameManager Instance
        {

            get
            {
                if (_instance is null)
                {
                    Debug.Log("GameManager is NULL");
                }
                return _instance;
            }
        }
        private void Awake()
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
              //  LoadData();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }
    public void SetLeveLNumber(int level=1)
    {
      //  Debug.Log("mLevelNum"+level);
        mLevelNum = level;
    }
    public int GetLeveLNumber()
    {
        return mLevelNum;
    }
     public void SetScore(int tmpscore)
    {
        if(tmpscore>=0)
        {
        mScore = tmpscore;
        SaveData();
        }
    }
    public int GetScore()
    {
        return mScore;
    }
    public void SetGridSizeData(int row,int col)
    {
        GridSizeData tmpgridSizeData=null;
        for(int i=0;i<LevelsManagerScriptableObjectCS.levelsGridSize.Count;i++){
           if((int)LevelsManagerScriptableObjectCS.levelsGridSize[i].RowGrid==row&&
           (int)LevelsManagerScriptableObjectCS.levelsGridSize[i].ColumnGrid==col){
           tmpgridSizeData=LevelsManagerScriptableObjectCS.levelsGridSize[i];
           }
        }
      //  Debug.Log("row----"+row);
       // Debug.Log("col----"+col);
       // Debug.Log("tmpgridSizeData----"+tmpgridSizeData);
        mCurrentLevelGridSizeData = tmpgridSizeData;
    }
    public GridSizeData GetGridSizeData()
    {
        return mCurrentLevelGridSizeData;
    }
    public List<Sprite> GetLevelSprites(int tmptotalmaxCardsnum,int tmplevelnum)
    { 
        List<Sprite> tmplevelsSprites=new List<Sprite>();
        int startindex=(tmplevelnum-1)* tmptotalmaxCardsnum;
        int endindex=(tmplevelnum)* tmptotalmaxCardsnum;
        Debug.Log("startindex"+startindex);
        Debug.Log("endindex"+endindex);
        for(var i=startindex;i<endindex;i++)
        {
            Debug.Log("i-------ssssss--"+i);
            tmplevelsSprites.Add(LevelsManagerScriptableObjectCS.levelsSprites[i]);
        }
         Debug.Log("i-------tmplevelsSprites--"+tmplevelsSprites.Count);
        return tmplevelsSprites;
    }
    public void LoadGameScene(){
          StartCoroutine(LoadSceneAsync("GameScene", LoadSceneMode.Single));
    }
    public void LoadSceneasync(string levelName,LoadSceneMode loadSceneMode=LoadSceneMode.Single){
         StartCoroutine(LoadSceneAsync(levelName, loadSceneMode));
    }
    private IEnumerator LoadSceneAsync(string levelName, LoadSceneMode loadSceneMode)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(levelName, loadSceneMode);
            while (!op.isDone)
            {
                float progress = Mathf.Clamp01(op.progress / .9f);
                Debug.Log(op.progress);
                yield return null;
            }
        }
        public void LoadData()
        {
            string path = Application.persistentDataPath + "/savefile.dat";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(path, FileMode.Open);
                GameInfoData GameInfoDataCS = (GameInfoData)formatter.Deserialize(file);

                if (GameInfoDataCS?.mIsSoundbol != null)
                {
                    SoundManager.Instance.mIsSoundbol = GameInfoDataCS.mIsSoundbol;
                }
                if (GameInfoDataCS?.mIsMusicbol != null)
                {
                    SoundManager.Instance.mIsMusicbol = GameInfoDataCS.mIsMusicbol;
                }
                if (GameInfoDataCS?.mScore != null)
                {
                    mScore = GameInfoDataCS.mScore;
                }

                file.Close();
            }
        }
        public void SaveData()
        {
            GameInfoData tmpGameInfoDataCS = new GameInfoData();
            if (tmpGameInfoDataCS?.mIsSoundbol != null)
            {
                tmpGameInfoDataCS.mIsSoundbol = SoundManager.Instance.mIsSoundbol;
            }
            if (tmpGameInfoDataCS?.mIsMusicbol != null)
            {
                tmpGameInfoDataCS.mIsMusicbol = SoundManager.Instance.mIsMusicbol;
            }
             if (tmpGameInfoDataCS?.mScore != null)
            {
                tmpGameInfoDataCS.mScore = mScore;
            }
            

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/savefile.dat");
            formatter.Serialize(file, tmpGameInfoDataCS);
            GameInfoDataCS = tmpGameInfoDataCS;
            file.Close();
            LoadData();
        }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           Application.Quit();
        }
    }
}
