using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//��Ϸ���
public class GameStart : MonoBehaviour
{ 
    //��ʼ���������չʾ��ʼUI������BGM
    void Start()
    {
        //����BGM
        //AudioManager.Instance.PlayBGM("beginBGM");//�ڴ������ʼBGM

        //��LoginUI���ؽ�UI�б�չʾ
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");


    }

}
