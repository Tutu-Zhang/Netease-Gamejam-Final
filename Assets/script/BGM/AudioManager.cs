using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmSource;//播放bgm的音频
    public bool isPlayingBeginBGM;

    

    private void Awake()
    {
        Instance = this;
        Debug.Log("音乐播放器已启动");
    }

/*    public void Init()
    {       
       bgmSource = gameObject.GetComponent<AudioSource>();
       Debug.Log("添加音乐物体");
    }*/

    public void PlayBGM(string name,bool isLoop = true)
    {
        bgmSource = gameObject.GetComponent<AudioSource>();

        Debug.Log("设置前"+isPlayingBeginBGM);
        //加载bgm声音剪辑
        AudioClip clip = Resources.Load<AudioClip>("Sound/BGM/" + name);

        bgmSource.clip = clip;

        bgmSource.loop = isLoop;
        
        bgmSource.volume = 0.2f;

        bgmSource.Play();

        if (name == "开场BGM")
        {
            isPlayingBeginBGM = true;
        }
        else
        {
            isPlayingBeginBGM = false;
        }
        Debug.Log("设置后"+isPlayingBeginBGM);

    }
    public void StopBGM()
    {
        bgmSource = gameObject.GetComponent<AudioSource>();
        bgmSource.volume = 0f;
    }
    //播放音效
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/Effect/" + name);

        AudioSource.PlayClipAtPoint(clip, new Vector2(0,0),5);//在特定时间点播放
    }
}
