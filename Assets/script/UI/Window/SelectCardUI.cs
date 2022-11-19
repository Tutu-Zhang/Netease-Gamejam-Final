using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCardUI : MonoBehaviour
{
    public Button GoToNext;

    private GameObject obj;
    private GameObject Description;

    // Start is called before the first frame update
    void Start()
    {
        if (!AudioManager.Instance.isPlayingBeginBGM)
        {
            Debug.Log("SelectUI����BGM");
            AudioManager.Instance.PlayBGM("����BGM");
        }

        DescriptionManager.Instance.CreatlNumToCardSkilDictionary();
        //RoleManager.Instance.CardTreasureInit();

        GoToNext.onClick.AddListener(gotoNext);
        GetCardUnlockSituration(); 
    }

    //��Ҫ�����ڿ���ѡ�������ʾ�������
    public void GetCardUnlockSituration()
    {
        Description = GameObject.Find("Description");
        for (int i = 0; i < 8; i++)
        {
            string key = DescriptionManager.Instance.NumToPair[i];//����i��Ӧ�������
            for (int j = 0; j < 8; j++)
            {
                SkillLevel Clevel = DescriptionManager.Instance.NumToSkillLevel[j];//����j��Ӧ���ܵȼ�
                //Debug.Log("�������Ϊ" + key);
                //Debug.Log("���Ƶȼ�" + Clevel);

                //�ж�ĳ��ϡ�ж��Ƿ�������������������ϡ�ж�����ʾ��������Ϊ����ӵ���¼�
                if (RoleManager.Instance.GetCardStatus(key,Clevel))
                {
                    //������Ч��
                    //��Ϊ��ѡЧ���Ľ����ϣ�����ְҵЧ���ĺ�׺����5
                    int num_presentProfession = j;
                    if (j>=5)
                    {
                        num_presentProfession = 5;
                    }
                    string ProfessionNum = num_presentProfession.ToString();

                    obj = GameObject.Find(key + "-" + ProfessionNum);


                    if (j > 0)
                    {
                        int x = i;
                        int y = j;
                        obj.GetComponent<Button>().onClick.AddListener(() => {
                            RoleManager.Instance.SetCardLevel(key, Clevel);
                            Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");//�������
                            //Debug.Log(x + "," + y);
                        });
                    }

                }
                else
                {
                    //δ������Ч��
                    int num_presentProfession = j;
                    if (j >= 5)
                    {
                        num_presentProfession = 5;
                    }
                    string ProfessionNum = num_presentProfession.ToString();
                    
                    obj = GameObject.Find(key + "-" + ProfessionNum);
                    ColorBlock newColor = new ColorBlock();
                    newColor.normalColor = new Color(0, 0, 0); 
                    obj.gameObject.GetComponent<Image>().color = newColor.normalColor;

                    if (j > 0)
                    {
                        int x = i;
                        int y = j;
                        obj.GetComponent<Button>().onClick.AddListener(() => {
                            //RoleManager.Instance.SetCardLevel(key, Clevel);
                            Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");//�������
                            //Debug.Log(x + "," + y);
                        });
                    }

                    //Debug.Log("δ�������������Ϊ" + key + "-" + ProfessionNum);                  
                }
            }
        }
    }


    //��һҳ
    public void gotoNext()
    {
/*        if (LevelManager.Instance.level >= 5)//�Ƚ��뱦��ѡ����棬�ڽ�������ѡ�����
        {
            SceneManager.LoadScene("selectTreasures");
        }
        else if (LevelManager.Instance.level == 4)//�����������
        {
            SceneManager.LoadScene("MissionUI");
        }
        else//����ս������
        {
            SceneManager.LoadScene("game1");
        }*/

        SceneManager.LoadScene("selectTreasures");//ֱ�ӽ���ѡ�����档������

    }
}
