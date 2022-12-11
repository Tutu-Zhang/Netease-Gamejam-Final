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
            AudioManager.Instance.PlayBGM("beginBGM-Changes-fiftysounds");
        }

        DescriptionManager.Instance.CreatlNumToCardSkilDictionary();
        //RoleManager.Instance.CardTreasureInit();

        GoToNext.onClick.AddListener(gotoNext);
        GetCardUnlockSituration(); 
    }

    //��Ҫ�����ڿ���ѡ�������ʾ�������
    public void GetCardUnlockSituration()
    {
        bool ifPCardSkillUnlock = false;//ר��һ��λ���Ƿ����
        Debug.Log(RoleManager.Instance.GetProfession());

        int PCardSkillNum = 0;//����ְҵ���������һ��Ӧ����ʲô
        Description = GameObject.Find("Description");

        int pro = 0; ;
        switch (RoleManager.Instance.PlayerProfession)
        {
            case Professions.PALADIN:
                pro = 6;
                break;
            case Professions.MONK:
                pro = 7;
                break;
            case Professions.SAMURAI:
                pro = 8;
                break;
            default:
                break;
        }

        for (int i = 0; i < 8; i++)
        {
            string key = DescriptionManager.Instance.NumToPair[i];//����i��Ӧ�������
            for (int j = 0; j < pro; j++)
            {
                SkillLevel Clevel = DescriptionManager.Instance.NumToSkillLevel[j];//����j��Ӧ���ܵȼ�
                Debug.Log("�������Ϊ" + key);
                Debug.Log("���Ƶȼ�" + Clevel);

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
                        if (j>=5)//��ְҵ��������������ж�
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
                            
                            string imgPath = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillIcon");
                            Texture2D texture2 = Resources.Load<Texture2D>(imgPath);
                            Sprite sp2 = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f));
                            obj.GetComponent<Image>().sprite = sp2;
                            
                            Debug.Log(y);
                            obj.GetComponent<Button>().onClick.AddListener(() => {
                                RoleManager.Instance.SetCardLevel(key, Clevel);
                                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");
                                SetCIconSelected(5, 5, key);//���һ�й̶�Ϊ5
                            });
                        }
                        else//�������ְҵ����Ч��
                        {
                            x = i;
                            y = j;
                            obj.GetComponent<Button>().onClick.AddListener(() => {
                                RoleManager.Instance.SetCardLevel(key, Clevel);
                                Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");
                                SetCIconSelected(5, y, key);
                            });
                        }

                    }

                }
                else//δ������Ч��
                {                    
                    int num_presentProfession = j;
                    if (j >= 5)
                    {
                        num_presentProfession = 5;
                    }
                    string ProfessionNum = num_presentProfession.ToString();                    
                    obj = GameObject.Find(key + "-" + ProfessionNum);

                    Texture2D texture2 = Resources.Load<Texture2D>("TreasureIcon/��ͷ");
                    Sprite sp2 = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f));
                    obj.GetComponent<Image>().sprite = sp2;
                    
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
        SceneManager.LoadScene("selectTreasures");//ֱ�ӽ���ѡ�����档������
    }

    //�ڵ��һ����ť֮����ʾ��ѡ��Ч��������ͬ�������ġ���ѡ������
    public void SetCIconSelected(int groupCount,int whichToSelect,string groupName)//�����ж��ٸ�����GroupCount����������5�����Լ�Ҫ���õ�����һ��WhichToSelect����Ӧj��groupName����000��001����Ӧkey
    {
        //���������ڵ�������ѡ������
        for (int i = 1; i <= groupCount; i++)
        {

            //�ҵ��ض��ġ���ѡ��
            GameObject selectedText = GameObject.Find(groupName + "-" + i.ToString() + "selected");
            Debug.Log("����İ�ť�ǣ�" + groupName + "-" + i.ToString() + "selected");

            if (i == whichToSelect)//����������������ť����Ҫѡ��������ť
            {

                Debug.Log(selectedText);
                selectedText.GetComponent<Text>().text = "��ѡ��";
            }
            else//�������
            {                
                selectedText.GetComponent<Text>().text = " ";               
            }

        }
    }

}
