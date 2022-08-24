using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// サウンド管理クラス
/// </summary>

//継承は仮！
public class SoundManager : MonoSingleton<SoundManager>
{
    private void Awake()
    {
        //if (this != instance)
        //{
        //    Destroy(this);
        //    return;
        //}

        foreach (var soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
            Debug.Log(soundData.name + "を登録");
        }
    }

    /// <summary>
    /// ボリューム
    /// </summary>
    [System.Serializable]
    public class SoundSettings
    {
        public float defVol;
        public bool mute = false;

        public void Reset()
        {
            defVol = 1.0f;
            mute = false;
        }
    }

    /// <summary>
    /// 音源のデータ
    /// </summary>
    #region SoundData
    [System.Serializable]
    public class SoundData
    {
        public string name;
        public AudioClip clip;
        public float vol = 1.0f;
        public bool loop = false;
    }
    #endregion

    public SoundSettings settings = new SoundSettings();

    //AudioSource
    const int audioChannel = 20;
    AudioSource[] audioList = new AudioSource[audioChannel];

    //Data
    [SerializeField] SoundData[] soundDatas;
    Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    //キュー
    Queue<SoundData> soundQueue = new Queue<SoundData>();

    private void Start()
    {
        for (int i = 0; i < audioChannel; ++i)
        {
            audioList[i] = gameObject.AddComponent<AudioSource>();
            audioList[i].volume = settings.defVol;
        }

        SwitchMute();
    }

    private void Update()
    {
        //キューに音源があるとき再生
        int count = soundQueue.Count;
        if (count != 0)
        {
            SoundData data = soundQueue.Dequeue();
            Play(data.clip, data.vol, data.loop);
        }
    }

    //未使用のソースを取得
    AudioSource GetUnActiveSource()
    {
        for (int i = 0; i < audioList.Length; ++i)
        {
            if (!audioList[i].isPlaying)
            {
                return audioList[i];
            }
        }
        //なし
        return null;
    }

    //再生処理
    public void Play(AudioClip clip, float vol, bool loop)
    {
        var source = GetUnActiveSource();
        if (source == null) { return; }
        source.clip = clip;
        source.volume = vol;
        source.loop = loop;
        source.Play();
    }

    public void Play(string name)
    {
        //stringをもとにdataを検索
        if (soundDictionary.TryGetValue(name, out var soundData))
        {
            //キューに追加
            if (!soundQueue.Contains(soundData)) { soundQueue.Enqueue(soundData); }
        }
        else
        {
            Debug.LogWarning($"音源が未登録です:{name}");
        }
    }

    //ミュートの切り替え
    public void SwitchMute()
    {
        foreach (var src in audioList)
        {
            src.mute = settings.mute;
        }

        if (settings.mute)
        {
            Debug.LogWarning("ミュートON");
        }
        else
        {
            Debug.LogWarning("ミュートOFF");
        }
    }

    //再生停止
    public void StopAudio()
    {
        soundQueue.Clear();
        foreach (var src in audioList)
        {
            src.Stop();
            src.clip = null;
        }
    }
}