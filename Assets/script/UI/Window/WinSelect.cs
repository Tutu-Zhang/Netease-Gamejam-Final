using System;
using System.Collections.Specialized;//字典列表
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
        //任务奖励

        //从效果池中选出3个效果，并显示
        SelectThreeCardSkills();
    }


    public void gotoNext()
    {
        SceneManager.LoadScene("selectCardSkills");//直接进入选宝界面。调试用
    }

    //设置3个公共效果
    public void SelectThreeCardSkills()
    {
        List<string> keyList = new List<string>();//存放数字组合
        List<SkillLevel> skillLevelList = new List<SkillLevel>();//存放等级组合

        //首先遍历所有效果，找出其中未被解锁的效果，将其加入list
        for (int i = 0; i < 8; i++)
        {
            string key = DescriptionManager.Instance.NumToPair[i];//数字i对应技能组合
            for (int j = 0; j < 5; j++)
            {
                SkillLevel Clevel = DescriptionManager.Instance.NumToSkillLevel[j];//数字j对应技能等级

                if (!RoleManager.Instance.GetCardStatus(key, Clevel))//如果未解锁
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
            //从Keylist中随机挑选一个
            //改变效果名
            int num = rnd.Next(keyList.Count);
            obj = GameObject.Find("效果名" + i.ToString());
            obj.GetComponent<Text>().text = num.ToString();//需要一个excel
            //改变效果图标
            obj = GameObject.Find("效果图标" + i.ToString());
            string imgPath = "UI-img/Treasure/宝物解锁";
            Texture2D texture = Resources.Load<Texture2D>(imgPath);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            obj.GetComponent<Image>().sprite = sp;
            //解锁效果
            obj = GameObject.Find("效果" + i.ToString());
            obj.GetComponent<Button>().onClick.AddListener(() => {
                RoleManager.Instance.SetCardLevel(keyList[i], skillLevelList[i]);             
            });

            //将已解锁的效果移除
            keyList.RemoveAt(num);

        }

    }
}
