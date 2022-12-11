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
    private int selectGTreasure = 0;
    private int selectPTreasure = 0;


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
                   

                    //添加点击事件,添加宝物到游戏，显示描述
                    int x = i;
                    int y = j;
                    
                    obj.GetComponent<Button>().onClick.AddListener(() => {
                        selectGTreasure = 1;//用以判断玩家是否选择了两个宝物
                        
                        //点击后，将该宝物绑定在宝物1的位置
                        RoleManager.Instance.SetTreasure(1, RoleManager.Instance.GetTreasureStatus(TreasurePro.GENERAL, TL, TC));
                        Description.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "TreasureDes");
                        Debug.Log(x + " " + y);
                        //显示“已选择”效果
                        SetTIconSelected(3, 3, x+1, y+1, "general");
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
        if (selectGTreasure + selectPTreasure == 2)
        {
            SceneManager.LoadScene("game1");
        }
        else
        {
            Description = GameObject.Find("Description");
            Description.GetComponent<Text>().text = "确定不选择宝物吗？携带两个宝物将使对战更加轻松";
        }
        
        
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
                string imgPath = ExcelReader.Instance.GetProfessionDes(i, DescriptionManager.Instance.CategoryToNum[TC], "TreasureIcon"); ;
                Texture2D texture = Resources.Load<Texture2D>(imgPath);
                Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                obj.GetComponent<Image>().sprite = sp;

                //添加点击事件,添加宝物到游戏，显示描述
                int x = i;//i始终等于4？
                int y = j;
                obj.GetComponent<Button>().onClick.AddListener(() => {
                    selectPTreasure = 1;
                    SetTIconSelected(1, 3, 1, y + 1, "profession");
                    //点击后，将该宝物绑定在宝物2的位置
                    RoleManager.Instance.SetTreasure(2, RoleManager.Instance.GetTreasureStatus(treasurePro, TreasureLevel.LEGEND, TC));
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

    //在点击一个按钮之后，显示已选择效果，并将同组其他的“已选择”隐藏
    public void SetTIconSelected(int groupCountX, int groupCountY, int whichToSelectX,int whichToSelectY, string groupName)//一共有多少组GroupCount；以及要设置的是哪一个WhichToSelect，对应j；groupName代表000，001，对应key
    {
        //遍历该组内的所“已选择”文字
        for (int i = 1; i <= groupCountX; i++)//groupCountX，非职业情况下是3，职业情况下是1
        {
            for (int j = 1; j <= groupCountY; j++)//groupCountY，始终为3
            {
                //找到特定的“已选择”
                GameObject selectedText = GameObject.Find(groupName + "-" + i.ToString()+ j.ToString());
                Debug.Log("点击的按钮是：" + groupName + "-" + whichToSelectX.ToString() + whichToSelectY.ToString());

                if (i == whichToSelectX && j == whichToSelectY)//如果遍历到的这个按钮就是要选择的这个按钮
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

}
