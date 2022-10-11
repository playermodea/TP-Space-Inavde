using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;
public class AudioManager : MonoBehaviour
{
    // Singleton을 위한 변수 선언
    public static AudioManager instance;

    public AudioClip[] bgmList;     // 배경음악 저장용 list
    public AudioSource bgmSource;   // 배경음악 재생을 위한 audio source
    public AudioMixer mixer;        // 음량 조절을 구현하기 위한 audio mixer

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 다른 Scene으로 이동해도 계속 오브젝트 사용 가능
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);    // 이미 Singleton 오브젝트 존재하므로 자신을 파괴
        }
        /*
        else if (instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        */
    }

    private void OnSceneLoaded(Scene sc, LoadSceneMode scMode)
    {
        for (int i = 0; i < bgmList.Length; i++)
        {
            if(sc.name == bgmList[i].name)
                PlayBgm(bgmList[i]);
        }
    }

    public void PlaySfx(string _name, AudioClip _clip, float _vol)
    {
        GameObject gameObject = new GameObject(_name + "Sound");
        AudioSource a = gameObject.AddComponent<AudioSource>();
        a.outputAudioMixerGroup = mixer.FindMatchingGroups("Sfx")[0];
        //audioSource.spatialBlend = 1.0f;
        a.clip = _clip;
        a.volume = _vol;
        a.priority = 100;
        a.Play();

        Destroy(gameObject, _clip.length);
    }

    public void PlayBgm(AudioClip _clip)
    {
        bgmSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Bgm")[0];
        bgmSource.clip = _clip;
        bgmSource.loop = true;
        bgmSource.volume = 0.7f;
        bgmSource.Play();
    }

    public void SetBgmVolume(float val)
    {
        mixer.SetFloat("BgmVol", Mathf.Log10(val) * 20);
    }
    public void SetSfxVolume(float val)
    {
        mixer.SetFloat("SfxVol", Mathf.Log10(val) * 20);
    }
}
