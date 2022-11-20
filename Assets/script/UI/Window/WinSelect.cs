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
    //用来记录最后选择的技能映射
    public string FinalKey;
    public SkillLevel FinalSkillLevel;
    //最后记录最后的职业技能映射
    public int PWhichWasSelect = 0;//记录玩家最终选择了哪一项 
    public string FinalPkey;
    public SkillLevel FinalPSkillLevel;

    // Start is called before the first frame update
    void Start()
    {
        DescriptionManager.Instance.CreatlNumToCardSkilDictionary();

        GoToNext.onClick.AddListener(gotoNext);
        //任务奖励

        //从卡牌效果池中选出3个效果，并显示
        SelectThreeCardSkills();

        //为职业效果按钮添加点击事件
        AddListenerToSetPCardSkillLevel();
        //为升级职业技能添加点击事件
        AddListenerToPlayerSkillLevelUp();
        //为提升血量添加点击事件
        AddListenerToMaxHpUp();

        
            Debug.Log(RoleManager.Instance.GetProfession());
    }

    //敲定玩家最后的选择
    public void gotoNext()
    {
        RoleManager.Instance.UnlockCardLevel(FinalKey, FinalSkillLevel);
        switch (PWhichWasSelect)    
        {
            case 1://解锁一个职业效果
                RoleManager.Instance.UnlockCardLevel(FinalPkey, FinalPSkillLevel);
                break;
            case 2://选择提升技能等级
                PlayerSkillLevelUp();
                break;
            case 3:
                MaxHpUp();
                break;
        }
        SceneManager.LoadScene("selectCardSkills");

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
            int num = rnd.Next(keyList.Count-1);
            //int num = 20;   
            obj = GameObject.Find("效果名" + i.ToString());
            int x = DescriptionManager.Instance.PairToNum[keyList[num]];//000对应的列数0
            int y = DescriptionManager.Instance.SkillLevelToNum[skillLevelList[num]];//rare对应2
            obj.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");//需要一个excel
            //改变效果图标
            obj = GameObject.Find("效果图标" + i.ToString());
            string imgPath = "UI-img/Treasure/宝物解锁";
            Texture2D texture = Resources.Load<Texture2D>(imgPath);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            obj.GetComponent<Image>().sprite = sp;
            //解锁效果
            obj = GameObject.Find("效果" + i.ToString());
            //提前将组合取出
            string TempFinalKey = keyList[num];
            SkillLevel TempFinalSkillLevel = skillLevelList[num];
            obj.GetComponent<Button>().onClick.AddListener(() => {
                //RoleManager.Instance.SetCardLevel(keyList[i], skillLevelList[i]);     不能在这里直接解锁
                //在点击下一页时才解锁这个技能
                FinalKey = TempFinalKey;
                FinalSkillLevel = TempFinalSkillLevel;
            });
            Debug.Log(num);
            Debug.Log("等级为" + skillLevelList[num]);
            //将已解锁的效果移除
            keyList.RemoveAt(num);

        }

    }

    //为职业效果按钮添加点击事件 PWhichWasSelect=1
    public void AddListenerToSetPCardSkillLevel()
    {
        //针对不同职业添加不同事件
        switch (RoleManager.Instance.PlayerProfession)
        {
            case Professions.PALADIN:
                SelectAndGetPCardSkillLEvel(SkillLevel.PALADIN);
                break;
            case Professions.MONK:
                SelectAndGetPCardSkillLEvel(SkillLevel.MONK);
                break;
            case Professions.SAMURAI:
                SelectAndGetPCardSkillLEvel(SkillLevel.SAMURAI);
                break;
        }
    }
    public void SelectAndGetPCardSkillLEvel(SkillLevel pro)
    {
        List<string> keyList = new List<string>();//存放数字组合。需要额外处理等于0的情况
        List<SkillLevel> skillLevelList = new List<SkillLevel>();//存放等级组合
        for (int i = 0; i < 8; i++)
        {
            string key = DescriptionManager.Instance.NumToPair[i];//数字i对应技能组合
            for (int j = 0; j < 8; j++)
            {
                SkillLevel Clevel = DescriptionManager.Instance.NumToSkillLevel[j];//数字j对应技能等级

                if (!RoleManager.Instance.GetCardStatus(key, Clevel) && Clevel == pro && RoleManager.Instance.GetProCardStatus(key, Clevel))//如果未解锁且属于特定职业
                {
                    keyList.Add(key);
                    skillLevelList.Add(Clevel);
                }
            }
        }


        if (keyList.Count == 0)//所有职业卡牌技能全部解锁
        {
            GameObject obj = GameObject.Find("职业效果");//将职业效果隐藏
            obj.SetActive(false);
        }
        else//还有卡牌技能尚未解锁
        {
            GameObject obj;
            System.Random rnd = new System.Random();
            int num = rnd.Next(keyList.Count-1);
            obj = GameObject.Find("职业效果描述");
            int x = DescriptionManager.Instance.PairToNum[keyList[num]];//000对应的列数0
            int y = DescriptionManager.Instance.SkillLevelToNum[skillLevelList[num]];//rare对应2
            obj.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(x, y, "CardSkillDes");//需要一个excel
            //改变效果图标
            obj = GameObject.Find("职业效果图标");
            string imgPath = "UI-img/Treasure/宝物解锁";
            Texture2D texture = Resources.Load<Texture2D>(imgPath);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            obj.GetComponent<Image>().sprite = sp;
            //解锁效果
            obj = GameObject.Find("职业效果");
            string TempFinalKey = keyList[num];
            SkillLevel TempFinalSkillLevel = skillLevelList[num];
            obj.GetComponent<Button>().onClick.AddListener(() => {
                //RoleManager.Instance.SetCardLevel(keyList[i], skillLevelList[i]);     不能在这里直接解锁
                //在点击下一页时才解锁这个技能
                FinalPkey = TempFinalKey;
                FinalPSkillLevel = TempFinalSkillLevel;
                //玩家选择了第一项
                PWhichWasSelect = 1;
            });
        }
    }

    //为升级技能按钮添加点击事件
    public void AddListenerToPlayerSkillLevelUp()
    {
        //获取升级技能按钮
        GameObject obj;
        obj = GameObject.Find("升级技能");
        obj.GetComponent<Button>().onClick.AddListener(() => {
            PWhichWasSelect = 2;
        });
        //如果职业技能已经达到传说级别，那么就将这个选项隐藏
        if (RoleManager.Instance.ProfessionSkillLevel == SkillLevel.LEGENDARY)
        {
            obj.SetActive(false);
        }
    }
    public void PlayerSkillLevelUp()
    {
        GameObject obj;
        obj = GameObject.Find("升级技能");
        switch (RoleManager.Instance.ProfessionSkillLevel)
        {
            case SkillLevel.NORMAL:

                    RoleManager.Instance.ProfessionSkillLevel = SkillLevel.RARE;

                break;
            case SkillLevel.RARE:

                    RoleManager.Instance.ProfessionSkillLevel = SkillLevel.EPIC;

                break;
            case SkillLevel.EPIC:

                    RoleManager.Instance.ProfessionSkillLevel = SkillLevel.LEGENDARY;

                break;
            case SkillLevel.LEGENDARY:
                break;
        }
    }

    //为提升血量上限添加点击事件
    public void AddListenerToMaxHpUp()
    {
        GameObject obj;
        obj = GameObject.Find("提升血量");
        obj.GetComponent<Button>().onClick.AddListener(() => {
            PWhichWasSelect = 3;
        });
    }
    public void MaxHpUp()//将提升血量的次数存在LevelManager里面
    {
        LevelManager.Instance.MaxHpFix += 1;
    }

}
