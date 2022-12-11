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
            AudioManager.Instance.PlayBGM("beginBGM-Changes-fiftysounds");
        }

        DescriptionManager.Instance.CreatlNumToCardSkilDictionary();
        //RoleManager.Instance.CardTreasureInit();

        GoToNext.onClick.AddListener(gotoNext);
        GetCardUnlockSituration(); 
    }

    //主要用于在卡牌选择界面显示解锁情况
    public void GetCardUnlockSituration()
    {
        bool ifPCardSkillUnlock = false;//专判一个位置是否解锁
        Debug.Log(RoleManager.Instance.GetProfession());

        int PCardSkillNum = 0;//根据职业，决定最后一行应该是什么
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
            string key = DescriptionManager.Instance.NumToPair[i];//数字i对应技能组合
            for (int j = 0; j < pro; j++)
            {
                SkillLevel Clevel = DescriptionManager.Instance.NumToSkillLevel[j];//数字j对应技能等级
                Debug.Log("数字组合为" + key);
                Debug.Log("卡牌等级" + Clevel);

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
                        if (j>=5)//对职业的情况进行特殊判断
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
                                SetCIconSelected(5, 5, key);//最后一行固定为5
                            });
                        }
                        else//如果不是职业卡牌效果
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
                else//未解锁该效果
                {                    
                    int num_presentProfession = j;
                    if (j >= 5)
                    {
                        num_presentProfession = 5;
                    }
                    string ProfessionNum = num_presentProfession.ToString();                    
                    obj = GameObject.Find(key + "-" + ProfessionNum);

                    Texture2D texture2 = Resources.Load<Texture2D>("TreasureIcon/锁头");
                    Sprite sp2 = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f));
                    obj.GetComponent<Image>().sprite = sp2;
                    
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
        SceneManager.LoadScene("selectTreasures");//直接进入选宝界面。调试用
    }

    //在点击一个按钮之后，显示已选择效果，并将同组其他的“已选择”隐藏
    public void SetCIconSelected(int groupCount,int whichToSelect,string groupName)//组内有多少个物体GroupCount，在这里有5个；以及要设置的是哪一个WhichToSelect，对应j；groupName代表000，001，对应key
    {
        //遍历该组内的所“已选择”文字
        for (int i = 1; i <= groupCount; i++)
        {

            //找到特定的“已选择”
            GameObject selectedText = GameObject.Find(groupName + "-" + i.ToString() + "selected");
            Debug.Log("点击的按钮是：" + groupName + "-" + i.ToString() + "selected");

            if (i == whichToSelect)//如果遍历到的这个按钮就是要选择的这个按钮
            {

                Debug.Log(selectedText);
                selectedText.GetComponent<Text>().text = "已选择";
            }
            else//如果不是
            {                
                selectedText.GetComponent<Text>().text = " ";               
            }

        }
    }

}
