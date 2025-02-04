using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    public AudioClip bgsound, UI_CliCk, Wrong_Tone, Correct_Tone;
    [SerializeField]
    public AudioSource mGameAudioSource, mMusicAudioSource;
    public bool mIsSoundbol = true, mIsMusicbol = true;
   
     
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.Log("SoundManager is NULL");
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
    public bool GetIsSound()
    {
        return mIsSoundbol;
    }
    public bool GetIsMusic()
    {
        return mIsMusicbol;
    }
    public void SetIsSound(bool tmpbol)
    {
        mIsSoundbol = tmpbol;
        GameManager.Instance.SaveData();
    }
    public void SetIsMusic(bool tmpbol)
    {
        mIsMusicbol = tmpbol;
        GameManager.Instance.SaveData();
        if (GameManager.Instance.IsBgMusicPlay)
        {
            BgMusicPlay();
        }
    }
    public void ButtonOnClick()
    {
        if (mIsSoundbol)
        {
            mMusicAudioSource.clip = UI_CliCk;
            mMusicAudioSource.loop = false;
            mMusicAudioSource.Play();
        }
    }

    public void CardTonePlay(bool isCard)
    {
        if (mIsSoundbol)
        {
            if (isCard)
            {
                mMusicAudioSource.clip = Correct_Tone;
            }
            else
            {
                mMusicAudioSource.clip = Wrong_Tone;
            }
            mMusicAudioSource.loop = false;
            mMusicAudioSource.Play();
        }
    }
    public void BgMusicPlay()
    {
        if (mIsMusicbol)
        {
            // IsBgMusicPlay=true;
            mGameAudioSource.clip = bgsound;
            mGameAudioSource.loop = true;
            mGameAudioSource.Play();
        }
        else
        {
            //   IsBgMusicPlay=false;
            mGameAudioSource.Stop();
        }
    }
    public void BgMusicStop()
    {
        //IsBgMusicPlay=false;
        mGameAudioSource.Stop();
    }

    // Update is called once per frame
     void Update()
    {
       if (SceneManager.GetActiveScene().name == "MainScene")
            {
          //IsBgMusicPlay=false;
          if(mGameAudioSource.isPlaying){
          BgMusicStop();
          }
            }
    }
}
