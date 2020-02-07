using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMgr : MonoBehaviour
{
    public static SoundMgr instance = null;

    public AudioMixerGroup _BGMMixer;
    public AudioMixerGroup _EffMixer;
    public BGMClip _BGM;
    public EffectClip _Effect;

    private List<AudioSource> m_BGMSources = new List<AudioSource>(m_BGMSourceCount);
    private List<AudioSource> m_EffSources = new List<AudioSource>(m_EffectSourceCount);
    private static int m_BGMSourceCount = 3;
    private static int m_EffectSourceCount = 5;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
        for (int i = 0; i < m_BGMSourceCount; i++)
        {
            InitBGMSource(i);
        }
        for (int i = 0; i < m_EffectSourceCount; i++)
        {
            InitEffSource(i);
        }
        Instantiate(_BGM, transform);
        Instantiate(_Effect, transform);
    }

    #region BGM
    public void PlayBGM(AudioClip clip, int sourceIndex = -1)
    {
        if (sourceIndex == -1)
        {
            for (int i = 0; i < m_BGMSources.Count; i++)
            {
                if (!m_BGMSources[i].isPlaying)
                {
                    m_BGMSources[i].clip = clip;
                    m_BGMSources[i].Play();
                    break;
                }
                else if (i == m_BGMSources.Count - 1)
                {
                    InitBGMSource(++i);
                    m_BGMSources[i].clip = clip;
                    m_BGMSources[i].Play();
                }
            }
        }
        else
        {
            m_BGMSources[sourceIndex].Stop();
            m_BGMSources[sourceIndex].clip = clip;
            m_BGMSources[sourceIndex].Play();
        }
    }

    public void PauseBGM(int sourceIndex)
    {
        if (m_BGMSources[sourceIndex].isPlaying)
            m_BGMSources[sourceIndex].Pause();
    }
    public void UnPauseBGM(int sourceIndex)
    {
        if (!m_BGMSources[sourceIndex].isPlaying)
            m_BGMSources[sourceIndex].UnPause();
    }
    public void StopBGM(int sourceIndex)
    {

        if (m_BGMSources[sourceIndex].isPlaying)
            m_BGMSources[sourceIndex].Stop();
    }
    #endregion

    #region Effect
    /// <summary>
    /// 
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="sourceIndex">0~1:Character; 2~3:Skills; 4:Common;</param>
    public void PlayEff(AudioClip clip, int sourceIndex = -1)
    {
        if (sourceIndex == -1)
        {
            for (int i = 0; i < m_EffSources.Count; i++)
            {
                if (!m_EffSources[i].isPlaying)
                {
                    m_EffSources[i].clip = clip;
                    m_EffSources[i].Play();
                    break;
                }
                else if (i == m_EffSources.Count - 1)
                {
                    InitEffSource(++i);
                    m_EffSources[i].clip = clip;
                    m_EffSources[i].Play();
                }
            }
        }
        else
        {
            m_EffSources[sourceIndex].Stop();
            m_EffSources[sourceIndex].clip = clip;
            m_EffSources[sourceIndex].Play();
        }
    }

    public void PauseEff(int sourceIndex)
    {
        if (m_EffSources[sourceIndex].isPlaying)
            m_EffSources[sourceIndex].Pause();
    }
    public void UnPauseEff(int sourceIndex)
    {
        if (!m_EffSources[sourceIndex].isPlaying)
            m_EffSources[sourceIndex].UnPause();
    }
    public void StopEff(int sourceIndex)
    {

        if (m_EffSources[sourceIndex].isPlaying)
            m_EffSources[sourceIndex].Stop();
    }
    #endregion

    #region Common
    /// <summary>
    /// 当前AudioSource是否在播放音频
    /// </summary>
    /// <param name="sourceType">0:BGM; 1:Effect;</param>
    /// <param name="sourceIndex"></param>
    /// <returns></returns>
    public bool IsPlaying(int sourceType, int sourceIndex)
    {
        if (sourceType == 0)
        {
            return m_BGMSources[sourceIndex].isPlaying;
        }
        else if (sourceType == 1)
        {
            return m_EffSources[sourceIndex].isPlaying;
        }
        else
        {
            throw new UnityException(string.Format("Fatal sourceType {0}!!!", sourceType));
        }
    }
    public void SetBGMVolume(float volume)
    {
        if (volume > 1 || volume < 0) return;

        for (int i = 0; i < m_BGMSources.Count; i++)
            m_BGMSources[i].volume = volume;
    }
    public void SetBGMMute(bool value)
    {
        for (int i = 0; i < m_BGMSources.Count; i++)
            m_BGMSources[i].mute = value;
    }
    public int GetBGMVolume()
    { return (int)(m_BGMSources[0].volume * 100); }

    public void SetFXVolume(float volume)
    {
        if (volume > 1 || volume < 0) return;

        for (int i = 0; i < m_EffSources.Count; i++)
            m_EffSources[i].volume = volume;
    }
    public void SetFXMute(bool value)
    {
        for (int i = 0; i < m_EffSources.Count; i++)
            m_EffSources[i].mute = value;
    }
    public int GetFXVolume()
    { return (int)(m_EffSources[0].volume * 100); }
    #endregion

    private void InitBGMSource(int i)
    {
        m_BGMSources.Add(gameObject.AddComponent<AudioSource>());
        m_BGMSources[i].outputAudioMixerGroup = _BGMMixer;
        m_BGMSources[i].loop = true;
    }

    private void InitEffSource(int i)
    {
        m_EffSources.Add(gameObject.AddComponent<AudioSource>());
        m_EffSources[i].outputAudioMixerGroup = _EffMixer;
    }
}
