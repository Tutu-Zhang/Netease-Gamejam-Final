using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SelectTreasureUI : MonoBehaviour
{
    public Button GoToNext;
    public GameObject Description;
    private Professions Player_Pro;


    // Start is called before the first frame update
    void Start()
    {
/*        if (!AudioManager.Instance.isPlayingBeginBGM)
        {
            Debug.Log("SelectUI播放BGM");
            AudioManager.Instance.PlayBGM("开场BGM");
        }*/
        DescriptionManager.Instance.CreatNumToTreasureDictionary();

        GoToNext.onClick.AddListener(gotoNext);
        GetTreasureUnlockSituration();

    }
    //获得宝物的解锁情况
    public void GetTreasureUnlockSituration()
    {
        Description = GameObject.Find("Description");
        Player_Pro = RoleManager.Instance.PlayerProfession;//获取当前玩家职业

        GameObject obj = new GameObject();

        //先处理非职业的情况
        for (int i = 0; i < 3; i++)
        {
            TreasureLevel TL = DescriptionManager.Instance.NumToTLevel[i];//i对应纵坐标，宝物等级
            for (int j = 0; j < 3; j++)
            {
                TreasureCategory TC = DescriptionManager.Instance.NumToCategory[j];//j对应横坐标，宝物类型
                
                if (RoleManager.Instance.GetTreasureStatus(TreasurePro.GENERAL, TL, TC).IfUnlock)//宝物解锁
                {
                    obj = GameObject.Find("Treasure" + (i + 1).ToString() + (j + 1).ToString());
                    
                    Debug.Log("Treasure" + (i + 1).ToString() + (j + 1).ToString());
                    
                    //改变宝物图标
                    string imgPath = "UI-img/Treasure/宝物解锁";
                    Texture2D texture = Resources.Load<Texture2D>(imgPath);
                    Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    obj.GetComponent<Image>().sprite = sp;

                    //添加点击事件,添加宝物到游戏，显示描述
                    int x = i;
                    int y = j;
                    obj.GetComponent<Button>().onClick.AddListener(() => {
                        //点击后，将该宝物绑定在宝物1的位置
                        RoleManager.Instance.SetTreasure(1, RoleManager.Instance.GetTreasureStatus(TreasurePro.GENERAL, TL, TC));
                        Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "TreasureDes");
                        //查看是否成功绑定到1位置
                        Debug.Log(RoleManager.Instance.GetTreasure(1).TCategory);
                    });
                    
                }
                else//宝物未解锁
                {
                    Debug.Log("Treasure" + (i + 1).ToString() + (j + 1).ToString());
                    obj = GameObject.Find("Treasure" + (i + 1).ToString() + (j + 1).ToString());
                    //添加点击事件,添加宝物到游戏，显示描述
                    int x = i;
                    int y = j;
                    obj.GetComponent<Button>().onClick.AddListener(() => {
                        Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "TreasureDes");
                    });
                }
                


            }
        }

        Debug.Log(Player_Pro);
        //再处理职业情况。当职业定下来了，实际上只需要横坐标也就是宝物类型，就可以确定一个宝物
        switch (Player_Pro)
        {
            case Professions.PALADIN:
                PTreasureLinkAndDes(obj, TreasurePro.PALADIN, 3);
                break;
            case Professions.MONK:
                PTreasureLinkAndDes(obj, TreasurePro.MONK, 4);
                break;
            case Professions.SAMURAI:
                PTreasureLinkAndDes(obj, TreasurePro.SAMURAI, 5);
                break;
        }   
            
    }


    public void gotoNext()
    {
        SceneManager.LoadScene("game1");
    }

    public void PTreasureLinkAndDes(GameObject obj, TreasurePro treasurePro, int i)//i由职业确定，代表在excel中是第几行
    {
        for (int j = 0; j < 3; j++)
        {
            TreasureCategory TC = DescriptionManager.Instance.NumToCategory[j];//j对应横坐标，宝物类型
            if (RoleManager.Instance.GetTreasureStatus(treasurePro, TreasureLevel.LEGEND, TC).IfUnlock)//宝物解锁
            {
                obj = GameObject.Find("PTreasure" + "1" + (j + 1).ToString());

                Debug.Log("PTreasure" + "1" + (j + 1).ToString());

                //改变宝物图标
                string imgPath = "UI-img/Treasure/宝物解锁";
                Texture2D texture = Resources.Load<Texture2D>(imgPath);
                Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                obj.GetComponent<Image>().sprite = sp;

                //添加点击事件,添加宝物到游戏，显示描述
                int x = i;
                int y = j;
                obj.GetComponent<Button>().onClick.AddListener(() => {
                    //点击后，将该宝物绑定在宝物2的位置
                    RoleManager.Instance.SetTreasure(2, RoleManager.Instance.GetTreasureStatus(TreasurePro.GENERAL, TreasureLevel.LEGEND, TC));
                    Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "TreasureDes");
                    Debug.Log("点击了宝物");
                });

            }
            else//宝物未解锁
            {
                Debug.Log("PTreasure" + "1" + (j + 1).ToString());
                obj = GameObject.Find("PTreasure" + "1" + (j + 1).ToString());
                //添加点击事件,添加宝物到游戏，显示描述
                int x = i;
                int y = j;
                obj.GetComponent<Button>().onClick.AddListener(() => {
                    Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "TreasureDes");
                    Debug.Log("点击了宝物");
                });
            }
        }
    }

}
