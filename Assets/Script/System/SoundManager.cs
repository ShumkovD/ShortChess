using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンド管理クラス
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
    }

    /// <summary>
    /// 音源管理クラス
    /// </summary>
    #region SoundData
    [System.Serializable]
    public class SoundData
    {
        public string name;
        public AudioClip clip;
        public float playedTime;
    }
    #endregion


    List<AudioSource> audioList = new List<AudioSource>();
    [SerializeField] SoundData[] soundDatas;
    Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();
    [SerializeField] float playableDist;

    private void Start()
    {
        foreach(var soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
            audioList.Add(gameObject.AddComponent<AudioSource>());
        }
    }

    AudioSource GetUnActiveSource()
    {
        for (int i = 0; i < audioList.Count; ++i)
        {
            if(!audioList[i].isPlaying)
            {
                return audioList[i];
            }
        }
        //未使用のソースなし
        return null;
    }

    public void Play(AudioClip clip)
    {
        var source = GetUnActiveSource();
        if(source == null) { return; }
        source.clip = clip;
        source.Play();
    }

    public void Play(string name)
    {
        if(soundDictionary.TryGetValue(name,out var soundData))
        {
            if(Time.realtimeSinceStartup - soundData.playedTime < playableDist) { return; }
            soundData.playedTime = Time.realtimeSinceStartup;
            Play(soundData.clip);
        }
        else
        {
            Debug.LogWarning($"音源が未登録です:{name}");
        }
    }
}
