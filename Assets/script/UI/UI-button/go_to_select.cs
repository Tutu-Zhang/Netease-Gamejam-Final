using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class go_to_select : MonoBehaviour
{
    public Button button;

    private void Start()//��ʼʱִ��
    {
        button.onClick.AddListener(GotoNew);//����Ϊbutton1��button�����ʱ��ִ��Gotonew����,button1��Ҫ��unity�и�ֵ
    }
    private void GotoNew()
    {
        AudioManager.Instance.PlayEffect("��ť");
        SceneManager.LoadScene("selectScene");
    }
}
