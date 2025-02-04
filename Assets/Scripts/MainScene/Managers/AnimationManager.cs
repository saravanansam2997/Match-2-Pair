using System.Linq.Expressions;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject ScoreAnimationPrefab;
    private GameObject Scores;
    private static AnimationManager _instance;
        public static AnimationManager Instance
        {

            get
            {
                if (_instance is null)
                {
                    Debug.Log("AnimationManager is NULL");
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
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("AnimationManager Start");
        DOTween.Init();
    }
    public void ScoreAnimation(bool IsAddCoins = false,string scorestr=null,GameObject Canvas=null, GameObject targetobj0 = null, GameObject targetobj1 = null, Action callback = null, float autotimerf = 0.8f){
     StartCoroutine(ScoreAnimationTimer(IsAddCoins,scorestr,Canvas,targetobj0,targetobj1,callback,autotimerf));
    }
     private IEnumerator ScoreAnimationTimer(bool IsAddCoins = false,string scorestr=null,GameObject Canvas=null, GameObject targetobj0 = null, GameObject targetobj1 = null, Action callback = null, float autotimerf = 1f)
    {
        yield return new WaitForSeconds(autotimerf);
        Scores = Instantiate(ScoreAnimationPrefab, Canvas.transform);
        Scores.GetComponent<TMP_Text>().text=scorestr;
        Scores.transform.localPosition = targetobj0.transform.localPosition;
        Scores.GetComponent<RectTransform>().DOAnchorPos(targetobj1.GetComponent<RectTransform>().position, 0.8f)
                   .SetEase(Ease.InBack)
                   .OnComplete(() =>
                   {
                       ScoresTweenCompleted( callback);
                   });
    }
    public void ScoresTweenCompleted(Action callback = null)
    {

            ScoresCompleted(callback);
    }
    public void ScoresCompleted(Action action)
    {
        if (Scores != null)
        {
            Destroy(Scores);
        }
        if (action != null)
        {
            action?.Invoke();
        }


    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
