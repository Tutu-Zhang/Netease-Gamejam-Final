using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DG���
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTF;//�����任���

    private List<UIBase> uiList;//�洢���ع��Ľ�����б�

    private void Awake()
    {
        Instance = this;
        //Debug.Log("UIMawake");
        canvasTF = GameObject.Find("Canvas").transform;//Ѱ�������еĻ���
        uiList = new List<UIBase>(); // ���б��ʼ��

    }

    public UIBase ShowUI<T>(string uiName) where T:UIBase
    {
        UIBase ui = Find(uiName);
        if (ui == null) 
        {
            //���������û�� ��Ҫ��Resources/UI�м���
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName), canvasTF) as GameObject;
            //������
            obj.name = uiName;
            //�����Ҫ�Ľű�
            ui = obj.AddComponent<T>();
            //��ӵ�����uiList
            uiList.Add(ui);
            //������ʾ�㼶Ϊ��ײ�
            obj.transform.SetAsFirstSibling();

            //��ʾ��ǰ��UIlist�����ڵ���
            System.Console.WriteLine(uiList);

        }
        else
        {
            //��UI��ʾ
            ui.Show();
        }

        return ui;
    }
    
    public void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            ui.Hide();
        }
    }

    //�ر����н���
    public void CloseAllUI()
    {
        for (int i = uiList.Count - 1; i >= 0; i--)
        {
            Destroy(uiList[i].gameObject);

        }
        uiList.Clear();
    }

    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            uiList.Remove(ui);
            Destroy(ui.gameObject);//��ui����
        }
    }

    public UIBase Find(string uiName)//���ݸ����Ľ��������Ӽ����н�������淵��
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name == uiName)
            {
                return uiList[i];
            }
       
        }
        return null;
    }

    public T GetUI<T> (string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            return ui.GetComponent<T>();
        }
        return null;    }

    /// <summary>
    /// ��������ͷ�����ж�ͼ��
    /// </summary>
    /// <returns></returns>
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI-img/figth-elements/action"), canvasTF.Find("fightBackground")) as GameObject;//����UI�ļ����е����ж���UI
        obj.transform.SetAsLastSibling();//�����ڸ��������һλ
        Debug.Log("���ص����ж�ͼ��");
        return obj;
    }

    //�������˵ײ�Ѫ������
    public GameObject CreateEnemyHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/EnemyHPBar"), canvasTF.Find("fightBackground")) as GameObject;//����UI�ļ�����Ѫ����UI
        obj.transform.SetAsLastSibling();//�����ڸ��������һλ
        Debug.Log("���ص���Ѫ����");
        return obj;
        
    }

    //������ҵײ�Ѫ������
    public GameObject CreatePlayerHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/playerHPBar"), canvasTF.Find("fightBackground")) as GameObject;//����UI�ļ�����Ѫ����UI
        obj.transform.SetAsLastSibling();//�����ڸ��������һλ
        Debug.Log("�������Ѫ����");
        return obj;

    }

    //��ʾ���棨�غ��л��ȵ�
    public void ShowTip(string msg,Color color,System.Action callBack = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/turnChangeUI"), canvasTF) as GameObject;//����UI�ļ�������ʾUI
        obj.transform.SetAsLastSibling();//�������ϲ�
        Text text = obj.transform.Find("turnChangeText").GetComponent<Text>();
        text.color = color;
        text.text = msg;
        Tween scale1 = obj.transform.Find("fight-background").DOScaleY(1, 1f);//���غ��л�UI���ڱ������棬����Ҫ�л���ʱ�򣬰����Ƶ�ǰ��
        Tween scale2 = obj.transform.Find("fight-background").DOScaleY(0, 1f);

        //�����������������Ĳ��Ŷ��У��Ȳ���1���ڲ���2
        Sequence seq = DOTween.Sequence();
        seq.Append(scale1);
        seq.AppendInterval(0.5f);
        seq.Append(scale2);
        seq.AppendCallback(delegate () //����Ǹ�ʲô��
        {
            if (callBack != null)
            {
                callBack();
            }
        });

        //�ӳ�2������obj
        MonoBehaviour.Destroy(obj, 2);
    }
}
