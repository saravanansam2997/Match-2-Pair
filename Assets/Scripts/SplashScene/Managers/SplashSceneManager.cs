using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

    public class SplashSceneManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject loaderPanel;
        private bool hasAnimationEnded;
        private bool IsScenceStop=true;
        void Start()
        {
            hasAnimationEnded = false;
            Debug.Log("Start SplashSceneManager......");
            SetIsScenceStop(false);
        }
        public void SetIsScenceStop(bool tmpbol){
            IsScenceStop=tmpbol;
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
        public void OnAnimationEnd()
        {
            StartCoroutine(LoadSceneAsync("MainScene", LoadSceneMode.Single));
        }
        void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                Debug.Log("OnApplicationPause" + pauseStatus);
                // Handle pause logic
                Time.timeScale = 0f;
                AudioListener.pause = true;
                // Additional logic if necessary
            }
            else
            {
                Debug.Log("OnApplicationPause" + pauseStatus);
                // Handle resume logic
                Time.timeScale = 1f;
                AudioListener.pause = false;
               // Additional logic if necessary
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (loaderPanel.activeSelf)
            {
                if (loaderPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !loaderPanel.GetComponent<Animator>().GetComponent<Animator>().IsInTransition(0))
                {
                    if (!hasAnimationEnded&& !IsScenceStop)
                    {
                        OnAnimationEnd();
                        hasAnimationEnded = true;

                    }
                }
            }

        }

    }
