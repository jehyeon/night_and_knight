using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] mAudioSources = new AudioSource[(int)Define.Sound.MaxCount];
    private Dictionary<string, AudioClip> mAudioClipDic = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                mAudioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            mAudioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    /**
     * @param path위치의 음원 재생
     */
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }
    
    /**
     * @param audioClip 재생
     * @param 재생 타입은 type, 재생 속도는 pitch로 설정
     */
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = mAudioSources[(int)type];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = mAudioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    /**
     * @param path위치의 음원파일을 로드 후 리턴
     */
    private AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sound/") == false)
            path = $"Sound/{path}";

        AudioClip audioClip;

        if (type == Define.Sound.Bgm)
        {
            audioClip = GameManager.ResourceMng.Load<AudioClip>(path);
        }
        else
        {
            if (mAudioClipDic.TryGetValue(path, out audioClip) == false)
            {
                audioClip = GameManager.ResourceMng.Load<AudioClip>(path);
                mAudioClipDic.Add(path, audioClip);
            }
        }
        
        if (audioClip == null)
            Debug.Log($"Failed to load AudioClip : {path}");

        return audioClip;
    }
    
    public void Clear()
    {
        foreach (AudioSource audioSource in mAudioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        
        mAudioClipDic.Clear();
    }
}
