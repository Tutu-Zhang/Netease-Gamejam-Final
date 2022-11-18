using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DG插件
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Transform canvasTF;//画布变换组件

    private List<UIBase> uiList;//存储加载过的界面的列表

    private void Awake()
    {
        Instance = this;
        //Debug.Log("UIMawake");
        canvasTF = GameObject.Find("Canvas").transform;//寻找世界中的画布
        uiList = new List<UIBase>(); // 将列表初始化

    }

    public UIBase ShowUI<T>(string uiName) where T:UIBase
    {
        UIBase ui = Find(uiName);
        if (ui == null) 
        {
            //如果集合中没有 需要从Resources/UI中加载
            GameObject obj = Instantiate(Resources.Load("UI/" + uiName), canvasTF) as GameObject;
            //改名字
            obj.name = uiName;
            //添加需要的脚本
            ui = obj.AddComponent<T>();
            //添加到集合uiList
            uiList.Add(ui);
            //调整显示层级为最底层
            obj.transform.SetAsFirstSibling();

            //显示当前的UIlist，用于调试
            System.Console.WriteLine(uiList);

        }
        else
        {
            //将UI显示
            ui.Show();
        }

        return ui;
    }
    
    public void HideUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            ui.Hide();
        }
    }

    //关闭所有界面
    public void CloseAllUI()
    {
        for (int i = uiList.Count - 1; i >= 0; i--)
        {
            Destroy(uiList[i].gameObject);

        }
        uiList.Clear();
    }

    public void CloseUI(string uiName)
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            uiList.Remove(ui);
            Destroy(ui.gameObject);//将ui销毁
        }
    }

    public UIBase Find(string uiName)//根据给定的界面名，从集合中将这个界面返回
    {
        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name == uiName)
            {
                return uiList[i];
            }
       
        }
        return null;
    }

    public T GetUI<T> (string uiName) where T : UIBase
    {
        UIBase ui = Find(uiName);
        if (ui != null)
        {
            return ui.GetComponent<T>();
        }
        return null;    }

    /// <summary>
    /// 创建敌人头部的行动图标
    /// </summary>
    /// <returns></returns>
    public GameObject CreateActionIcon()
    {
        GameObject obj = Instantiate(Resources.Load("UI-img/figth-elements/action"), canvasTF.Find("fightBackground")) as GameObject;//加载UI文件夹中敌人行动的UI
        obj.transform.SetAsLastSibling();//设置在父级的最后一位
        Debug.Log("加载敌人行动图标");
        return obj;
    }

    //创建敌人底部血量物体
    public GameObject CreateEnemyHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/EnemyHPBar"), canvasTF.Find("fightBackground")) as GameObject;//加载UI文件夹中血量的UI
        obj.transform.SetAsLastSibling();//设置在父级的最后一位
        Debug.Log("加载敌人血量条");
        return obj;
        
    }

    //创建玩家底部血量物体
    public GameObject CreatePlayerHpItem()
    {
        GameObject obj = Instantiate(Resources.Load("UI/playerHPBar"), canvasTF.Find("fightBackground")) as GameObject;//加载UI文件夹中血量的UI
        obj.transform.SetAsLastSibling();//设置在父级的最后一位
        Debug.Log("加载玩家血量条");
        return obj;

    }

    //提示界面（回合切换等等
    public void ShowTip(string msg,Color color,System.Action callBack = null)
    {
        GameObject obj = Instantiate(Resources.Load("UI/turnChangeUI"), canvasTF) as GameObject;//加载UI文件夹中提示UI
        obj.transform.SetAsLastSibling();//置于最上层
        Text text = obj.transform.Find("turnChangeText").GetComponent<Text>();
        text.color = color;
        text.text = msg;
        Tween scale1 = obj.transform.Find("fight-background").DOScaleY(1, 1f);//将回合切换UI藏在背景后面，当需要切换的时候，把他移到前面
        Tween scale2 = obj.transform.Find("fight-background").DOScaleY(0, 1f);

        //定义上述两个动画的播放队列，先播放1，在播放2
        Sequence seq = DOTween.Sequence();
        seq.Append(scale1);
        seq.AppendInterval(0.5f);
        seq.Append(scale2);
        seq.AppendCallback(delegate () //这个是干什么的
        {
            if (callBack != null)
            {
                callBack();
            }
        });

        //延迟2秒销毁obj
        MonoBehaviour.Destroy(obj, 2);
    }
}
