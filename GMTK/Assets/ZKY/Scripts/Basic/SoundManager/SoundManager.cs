using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct Sounds
{
    public string name;
    public AudioSource audio;
}

public class SoundManager : SinglatonForMono<SoundManager>
{
    [SerializeField] private List<Sounds> musics;
    [SerializeField] private List<Sounds> soundEffects;

    /**
    * @brief 播放指定名称的音频
    * @param name 音频名称
    */
    public void Play(string name)
    {
        foreach (var music in musics)
        {
            if (music.name.Equals(name))
            {
                music.audio.Play();
                return;
            }
        }

        foreach (var music in soundEffects)
        {
            if (music.name.Equals(name))
            {
                music.audio.Play();
                return;
            }
        }
    }

    /**
     * @brief 暂停指定名称的音频
     * @param name 音频名称
     */
    public void Pause(string name)
    {
        foreach (var music in musics)
        {
            if (music.name.Equals(name))
            {
                music.audio.Pause();
                return;
            }
        }

        foreach (var music in soundEffects)
        {
            if (music.name.Equals(name))
            {
                music.audio.Pause();
                return;
            }
        }
    }

    /**
     * @brief 停止指定名称的音频
     * @param name 音频名称
     */
    public void Stop(string name)
    {
        foreach (var music in musics)
        {
            if (music.name.Equals(name))
            {
                music.audio.Stop();
                return;
            }
        }

        foreach (var music in soundEffects)
        {
            if (music.name.Equals(name))
            {
                music.audio.Stop();
                return;
            }
        }
    }

    /**
     * @brief 改变音乐或音效的音量
     * @param isMusic 是否为音乐
     * @param volumn 音量大小
     */
    public void ChangeVolumn(bool isMusic, float volumn)
    {
        if (isMusic)
        {
            foreach (var music in musics)
            {
                music.audio.volume = volumn;
            }
        }
        else
        {
            foreach (var music in soundEffects)
            {
                music.audio.volume = volumn;
            }
        }
    }

    /**
     * @brief 改变指定名称的音频音量
     * @param name 音频名称
     * @param volumn 音量大小
     */
    public void ChangeVolumn(string name, float volumn)
    {
        foreach (var music in musics)
        {
            if (music.name.Equals(name))
            {
                music.audio.volume = volumn;
                return;
            }
        }

        foreach (var music in soundEffects)
        {
            if (music.name.Equals(name))
            {
                music.audio.volume = volumn;
                return;
            }
        }
    }

    /**
     * @brief 使用渐变效果调整音乐或音效的音量
     * @param isMusic 是否为音乐
     * @param volumn 目标音量
     * @param time 渐变时间
     */
    public void FadeVolumn(bool isMusic, float volumn, float time)
    {
        if (isMusic)
        {
            for (int i = 0; i < musics.Count; i++)
            {
                StartCoroutine(FadeVolumnCourtine(musics[i], volumn, time));
            }
        }
        else
        {
            for (int i = 0; i < soundEffects.Count; i++)
            {
                StartCoroutine(FadeVolumnCourtine(soundEffects[i], volumn, time));
            }
        }
    }

    /**
     * @brief 使用渐变效果调整指定名称的音频音量
     * @param name 音频名称
     * @param volumn 目标音量
     * @param time 渐变时间
     */
    public void FadeVolumn(string name, float volumn, float time)
    {
        foreach (var music in musics)
        {
            if (music.name.Equals(name))
            {
                StartCoroutine(FadeVolumnCourtine(music, volumn, time));
                return;
            }
        }

        foreach (var music in soundEffects)
        {
            if (music.name.Equals(name))
            {
                StartCoroutine(FadeVolumnCourtine(music, volumn, time));
                return;
            }
        }
    }

    /**
     * @brief 音量渐变协程
     * @param audio 音频结构体
     * @param volumn 目标音量
     * @param time 渐变时间
     * @returns IEnumerator 协程
     */
    private IEnumerator FadeVolumnCourtine(Sounds audio, float volumn, float time)
    {
        float start = audio.audio.volume;
        float timer = 0;
        while (timer <= time)
        {
            timer += Time.deltaTime;
            audio.audio.volume = Mathf.Lerp(start, volumn, timer / time);
            yield return null;
        }
    }
}
