using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmSource;//����bgm����Ƶ
    public bool isPlayingBeginBGM;

    

    private void Awake()
    {
        Instance = this;
        Debug.Log("���ֲ�����������");
    }

/*    public void Init()
    {       
       bgmSource = gameObject.GetComponent<AudioSource>();
       Debug.Log("�����������");
    }*/

    public void PlayBGM(string name,bool isLoop = true)
    {
        bgmSource = gameObject.GetComponent<AudioSource>();

        Debug.Log("����ǰ"+isPlayingBeginBGM);
        //����bgm��������
        AudioClip clip = Resources.Load<AudioClip>("Sound/BGM/" + name);

        bgmSource.clip = clip;

        bgmSource.loop = isLoop;
        
        bgmSource.volume = 0.2f;

        bgmSource.Play();

        if (name == "����BGM")
        {
            isPlayingBeginBGM = true;
        }
        else
        {
            isPlayingBeginBGM = false;
        }
        Debug.Log("���ú�"+isPlayingBeginBGM);

    }
    public void StopBGM()
    {
        bgmSource = gameObject.GetComponent<AudioSource>();
        bgmSource.volume = 0f;
    }
    //������Ч
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/Effect/" + name);

        AudioSource.PlayClipAtPoint(clip, new Vector2(0,0),5);//���ض�ʱ��㲥��
    }
}
