using System;
using System.Collections.Specialized;//�ֵ��б�
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MissionUI : MonoBehaviour
{

    public Button GoToNext;

    // Start is called before the first frame update
    void Start()
    {
        GoToNext.onClick.AddListener(gotoNext);
    }

    public void gotoNext()
    {
        SceneManager.LoadScene("winSelect");//ֱ�ӽ���ѡ�����档������
    }
}
