using System;
using System.Collections.Specialized;//�ֵ��б�
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
        DescriptionManager.Instance.CreatlNumToCardSkilDictionary();

        GoToNext.onClick.AddListener(gotoNext);
        //������

        //��Ч������ѡ��3��Ч��������ʾ
        SelectThreeCardSkills();
    }


    public void gotoNext()
    {
        SceneManager.LoadScene("selectCardSkills");//ֱ�ӽ���ѡ�����档������
    }

    //����3������Ч��
    public void SelectThreeCardSkills()
    {
        List<string> keyList = new List<string>();//����������
        List<SkillLevel> skillLevelList = new List<SkillLevel>();//��ŵȼ����

        //���ȱ�������Ч�����ҳ�����δ��������Ч�����������list
        for (int i = 0; i < 8; i++)
        {
            string key = DescriptionManager.Instance.NumToPair[i];//����i��Ӧ�������
            for (int j = 0; j < 5; j++)
            {
                SkillLevel Clevel = DescriptionManager.Instance.NumToSkillLevel[j];//����j��Ӧ���ܵȼ�

                if (!RoleManager.Instance.GetCardStatus(key, Clevel))//���δ����
                {
                    keyList.Add(key);
                    skillLevelList.Add(Clevel);
                }
            }
        }


        GameObject obj;
        System.Random rnd = new System.Random();
        for (int i = 1; i <= 3; i++)
        {
            //��Keylist�������ѡһ��
            //�ı�Ч����
            int num = rnd.Next(keyList.Count);
            obj = GameObject.Find("Ч����" + i.ToString());
            obj.GetComponent<Text>().text = num.ToString();//��Ҫһ��excel
            //�ı�Ч��ͼ��
            obj = GameObject.Find("Ч��ͼ��" + i.ToString());
            string imgPath = "UI-img/Treasure/�������";
            Texture2D texture = Resources.Load<Texture2D>(imgPath);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            obj.GetComponent<Image>().sprite = sp;
            //����Ч��
            obj = GameObject.Find("Ч��" + i.ToString());
            obj.GetComponent<Button>().onClick.AddListener(() => {
                RoleManager.Instance.SetCardLevel(keyList[i], skillLevelList[i]);             
            });

            //���ѽ�����Ч���Ƴ�
            keyList.RemoveAt(num);

        }

    }
}
