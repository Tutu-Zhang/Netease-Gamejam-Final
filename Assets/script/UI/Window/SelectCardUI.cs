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
        Debug.Log(RoleManager.Instance.GetProfession());

        int PCardSkillNum = 0;//����ְҵ���������һ��Ӧ����ʲô
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

                    //ȷ��excel�е�λ��
                    int x = 0;
                    int y = 0;
                    if (j > 0)
                    {
                        if (j>=5)//���һ�У�Ҫ�ж�ְҵ
                        {
                            switch (RoleManager.Instance.PlayerProfession)
                            {
                                case Professions.PALADIN:
                                    PCardSkillNum = 5;
                                    break;
                                case Professions.MONK:
                                    PCardSkillNum = 6;
                                    break;
                                case Professions.SAMURAI:
                                    PCardSkillNum = 7;
                                    break;
                            }
                            x = i;
                            y = PCardSkillNum;
                            Debug.Log(y);
                            obj.GetComponent<Button>().onClick.AddListener(() => {
                                RoleManager.Instance.SetCardLevel(key, Clevel);
                                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");
                            });
                        }
                        else
                        {
                            x = i;
                            y = j;
                            obj.GetComponent<Button>().onClick.AddListener(() => {
                                RoleManager.Instance.SetCardLevel(key, Clevel);
                                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");
                            });
                        }

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

                    int x = 0;
                    int y = 0;
                    if (j > 0)
                    {
                        if (j >= 5)//���һ�У�Ҫ�ж�ְҵ
                        {
                            switch (RoleManager.Instance.PlayerProfession)
                            {
                                case Professions.PALADIN:
                                    PCardSkillNum = 5;
                                    break;
                                case Professions.MONK:
                                    PCardSkillNum = 6;
                                    break;
                                case Professions.SAMURAI:
                                    PCardSkillNum = 7;
                                    break;
                            }
                            x = i;
                            y = PCardSkillNum;
                            //Debug.Log(y);
                            obj.GetComponent<Button>().onClick.AddListener(() => {

                                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");
                            });
                        }
                        else
                        {
                            x = i;
                            y = j;
                            obj.GetComponent<Button>().onClick.AddListener(() => {
                                RoleManager.Instance.SetCardLevel(key, Clevel);
                                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");
                            });
                        }
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
