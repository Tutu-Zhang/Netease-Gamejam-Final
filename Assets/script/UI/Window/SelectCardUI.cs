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
            Debug.Log("SelectUI播放BGM");
            AudioManager.Instance.PlayBGM("开场BGM");
        }

        DescriptionManager.Instance.CreatlNumToCardSkilDictionary();
        //RoleManager.Instance.CardTreasureInit();

        GoToNext.onClick.AddListener(gotoNext);
        GetCardUnlockSituration(); 
    }

    //主要用于在卡牌选择界面显示解锁情况
    public void GetCardUnlockSituration()
    {
        Debug.Log(RoleManager.Instance.GetProfession());

        int PCardSkillNum = 0;//根据职业，决定最后一行应该是什么
        Description = GameObject.Find("Description");
        for (int i = 0; i < 8; i++)
        {
            string key = DescriptionManager.Instance.NumToPair[i];//数字i对应技能组合
            for (int j = 0; j < 8; j++)
            {
                SkillLevel Clevel = DescriptionManager.Instance.NumToSkillLevel[j];//数字j对应技能等级
                //Debug.Log("数字组合为" + key);
                //Debug.Log("卡牌等级" + Clevel);

                //判断某个稀有度是否解锁。如果解锁，将该稀有度牌显示解锁，并为其添加点击事件
                if (RoleManager.Instance.GetCardStatus(key,Clevel))
                {
                    //解锁该效果
                    //因为在选效果的界面上，所有职业效果的后缀都是5
                    int num_presentProfession = j;
                    if (j>=5)
                    {
                        num_presentProfession = 5;
                        
                    }
                    string ProfessionNum = num_presentProfession.ToString();

                    obj = GameObject.Find(key + "-" + ProfessionNum);

                    //确定excel中的位置
                    int x = 0;
                    int y = 0;
                    if (j > 0)
                    {
                        if (j>=5)//最后一行，要判断职业
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
                    //未解锁该效果
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
                        if (j >= 5)//最后一行，要判断职业
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

                    //Debug.Log("未解锁的数字组合为" + key + "-" + ProfessionNum);                  
                }
            }
        }
    }


    //下一页
    public void gotoNext()
    {
/*        if (LevelManager.Instance.level >= 5)//先进入宝物选择界面，在进入任务选择界面
        {
            SceneManager.LoadScene("selectTreasures");
        }
        else if (LevelManager.Instance.level == 4)//进入任务界面
        {
            SceneManager.LoadScene("MissionUI");
        }
        else//进入战斗界面
        {
            SceneManager.LoadScene("game1");
        }*/

        SceneManager.LoadScene("selectTreasures");//直接进入选宝界面。调试用

    }
}
