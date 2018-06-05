
/// <summary>
@file   SetVolume.cs
@brief  音量の設定
@author 齊藤未来
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolumes : MonoBehaviour {

    [SerializeField]
    AudioMixer mixer;　// AudioMixerを取得


    public float masterVolume
    {
        set{ mixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, value));}
    }
    public float bgmVolume
    {
        set { mixer.SetFloat("BGMVolume", Mathf.Lerp(-80, 0, value)); }
    }
    public float systemVolume
    {
        set { mixer.SetFloat("SystemVolume", Mathf.Lerp(-80, 0, value)); }
    }
    public float voiceVolume
    {
        set { mixer.SetFloat("VoiceVolume", Mathf.Lerp(-80, 0, value)); }
    }
}
