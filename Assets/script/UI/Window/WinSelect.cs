using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinSelect : MonoBehaviour
{
    public Button GoToNext;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void gotoNext()
    {

        SceneManager.LoadScene("selectCardSkills");//ֱ�ӽ���ѡ�����档������

    }
}
