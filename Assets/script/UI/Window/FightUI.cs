using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FightUI : UIBase
{
    //private Text cardCountText;//卡牌数量
    //private Text noCardCountText;//弃牌堆数量
    //private Text powerText;
    private Text hpText;
    private Image hpImage;
    private Image hpHitImage;
    private Text defText;
    private GameObject playerHPBar;

    private GameObject playCardZone;
    private GameObject cardZone;
    private GameObject cardArea;
    private GameObject enemyCase;
    private GameObject BuffDescription;
    private String skillText = "技能描述";

    private List<CardItem> cardItemList; //手牌区集合
    private List<CardItem> PlayCardList; //出牌区集合
    private List<BuffItem> BuffList; //牌局中Buff集合，由于不存在玩家对打，所以敌人buff和自己的buff都在里面

    Button turnBtn, UseBtn;
    private void Awake()
    {
        cardItemList = new List<CardItem>();
        PlayCardList = new List<CardItem>();
        BuffList = new List<BuffItem>();
        BuffList.Clear();

        BuffDescription = transform.Find("BuffDescription").gameObject;
        BuffDescription.AddComponent<BuffDescription>();
    }

    private void Start()
    {

        playerHPBar = UIManager.Instance.CreatePlayerHpItem();
        //Debug.Log("找到血条");
        hpText = playerHPBar.transform.Find("PlayerHPText").GetComponent<Text>();
        hpImage = playerHPBar.transform.Find("PlayerHPFill").GetComponent<Image>();
        hpHitImage = playerHPBar.transform.Find("PlayerHitHPFill").GetComponent<Image>();
        defText = playerHPBar.transform.Find("PlayerDefText").GetComponent<Text>();

        enemyCase = transform.Find("EnemyCase").gameObject;
        playCardZone = transform.Find("PlayCardZone").gameObject;
        cardZone = transform.Find("CardZone").gameObject;
        cardArea = transform.Find("CardArea").gameObject;
        turnBtn = GameObject.Find("turnBtn").GetComponent<Button>();
        turnBtn.gameObject.SetActive(false);
        UseBtn = GameObject.Find("UseBtn").GetComponent<Button>();
        UseBtn.gameObject.SetActive(false);

        //获取回合切换按钮
        turnBtn.onClick.AddListener(onChangeTurnBtn);
        //获取出牌按钮
        UseBtn.onClick.AddListener(UseCard);

        UpdateHP();
        UpdateDef();


        //UpdateCardCount();
        //UpdateUsedCardCount();
        //UpdatePower();
    }

    //玩家回合结束，切换到敌人回合
    private void onChangeTurnBtn()
    {
        AudioManager.Instance.PlayEffect("按钮2");
        //只有玩家才能切换
        if (FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            //RemoveAllCards(true);
            for(int i = PlayCardList.Count - 1; i >= 0; i--)
            {
                PlayCardList[i].SetPlayArea(false);
                MoveCardToHandArea(PlayCardList[i]);
            }

            FightManager.Instance.ChangeType(FightType.Enemy);
        }
        Debug.Log("回合切换");
    }

    //打出卡
    private void UseCard()
    {
        AudioManager.Instance.PlayEffect("按钮2");
        if (FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            string cardId = "";
            for (int i = 0; i < PlayCardList.Count; i++)
            {
                cardId = cardId + PlayCardList[i].GetCardNum().ToString();
            }
            //Debug.Log(cardId);


            CardEffects.MatchCard(cardId, GetSpecialSkillLevel(cardId)); //Matchcard顺便就执行卡的效果
           StartCoroutine(UseCardEffects(cardId));
            BuffDescription.GetComponent<BuffDescription>().RefreshBuffText();
        }
    }

    public SkillLevel GetSpecialSkillLevel(string skill)
    {
        return RoleManager.Instance.GetCurrentSkillLevel(skill);
    }

    IEnumerator UseCardEffects(string id)
    {
        if (id[0] == '1')
        {
            for (int i = 0; i < PlayCardList.Count; i++)
            {
                PlayCardList[i].transform.DOMove(enemyCase.transform.position, 1f);
            }
        }
        else
        {
            for (int i = 0; i < PlayCardList.Count; i++)
            {
                PlayCardList[i].transform.DOMove(playerHPBar.transform.position, 1f);
            }
        }
        //yield return new WaitForSeconds(1f);
        yield return null;
        RemoveAllCards(true);
    }

    //显示卡牌效果
    private void ShowSkillText()
    {
        string cardId = "";
        for (int i = 0; i < PlayCardList.Count; i++)
        {
            cardId = cardId + PlayCardList[i].GetCardNum().ToString();
        }

        
        Dictionary<string, string> skillData = GameConfigManager.Instance.GetPlayerSkillsById("00001");

        GameObject obj = GameObject.FindGameObjectWithTag("SkillDes");

        skillText = skillData[cardId];

        obj.GetComponent<Text>().text = cardId + ":"+ skillText;

        skillText = "技能描述";

    }

    //隐藏卡牌效果
    private void HideSkillText()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("SkillDes");

        obj.GetComponent<Text>().text = skillText;
    }

    //寻找buff列表中是否有buffid为参数的buff
    public BuffItem FindBuff(string id)
    {
        if (BuffList.Count != 0)
        {
            for (int i = 0; i < BuffList.Count; i++)
            {
                if (BuffList[i].GetBuffId() == id)
                {
                    return BuffList[i];
                }
            }
            return null;
        }
        else
            return null;
    }


    public void UpdateHP()
    {
        hpText.text = FightManager.Instance.CurHP + "/" + FightManager.Instance.MaxHP;
        hpImage.fillAmount = (float)FightManager.Instance.CurHP / (float)FightManager.Instance.MaxHP;
        
        StartCoroutine(ChangeHitFill());
    }

    public IEnumerator ChangeHitFill()
    {
        while(hpHitImage.fillAmount > hpImage.fillAmount)
        {
            hpHitImage.fillAmount -= 0.005f;
            yield return new WaitForSeconds(0.04f);
        }

        hpHitImage.fillAmount = hpImage.fillAmount;
        yield break;
    }



    public void UpdateDef()
    {
        defText.text = FightManager.Instance.DefCount.ToString();
    }


    //创建卡牌物体
    public void CreatCardItem(int count)
    {
        for (int i = 0; i < count ; i++)
        {
            
            //Id	Name	Script	Type	Des	BgIcon	Icon	Expend	Arg0	Effects
            //唯一的标识（不能重复）	名称	卡牌添加的脚本	卡牌类型的Id	描述	卡牌的背景图资源路径	图标资源的路径	消耗的费用	属性值	特效
            //1000	普通攻击	AttackCardItem	10001	对单个敌人进行{0}点的伤害	Icon/BlueCard	Icon/sword_03e	1	3	Effects/GreenBloodExplosion

            GameObject obj = Instantiate(Resources.Load("UI-img/card/cardBackground"), transform.Find("CardArea") ,false) as GameObject;//加载卡牌UI，并根据父对象设置
            obj.GetComponent<Transform>().position = new Vector2(-3.1f, -3.2f);
            //var Item = obj.AddComponent<CardItem>();
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            //Debug.Log("当前创建的牌类型为" + System.Type.GetType(data["Script"]));
            
            CardItem Item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            Item.Init(data,i);
            cardItemList.Add(Item);
            //Debug.Log("现在有" + cardItemList.Count + "张牌");
        }
    }


    //下面的两个addBuff都是向bufflist添加buff，区别仅在于接受的参数不同
    public void addBuff(BuffItem item)
    {
        if (FindBuff(item.GetBuffId()))
        {
            FindBuff(item.GetBuffId()).AddLeftTime(item.GetLeftTime());
        }
        else
            BuffList.Add(item);

        for(int i = 0;i < BuffList.Count; i++)
        {
            //Debug.Log("目前有buff:" + BuffList[i]);
        }
    }

    public void addBuff(string id, SkillLevel level, int left_time)
    {

        //buff.Init("0101", 3);

        if (FindBuff(id))
        {
            FindBuff(id).AddLeftTime(left_time);
        }
        else
        {
            GameObject obj = new GameObject("buff" + id);
            BuffItem buff = obj.AddComponent<BuffItem>();
            buff.Init(id, level, left_time);
            BuffList.Add(buff);
        }


        for (int i = 0; i < BuffList.Count; i++)
        {
            //Debug.Log("目前有buff:" + BuffList[i].GetBuffId());
        }
    }



    //更新手牌位置
    public void UpdateCardItemPos()
    {
        //Debug.Log("更新手牌位置");
        for(int i = 0; i < cardItemList.Count; i++)
        {
            Transform card = cardItemList[i].transform;
            card.position = cardZone.transform.GetChild(i).position;
            card.transform.localScale = new Vector2(1,1);
        }
     
    }

    //更新出牌区位置
    public void UpdatePlayCardPos()
    {
        //Debug.Log("更新上方牌区位置");
        for (int i = 0; i < PlayCardList.Count; i++)
        {
            Transform card = PlayCardList[i].transform;
            card.position = playCardZone.transform.GetChild(i).position;
            card.transform.localScale = new Vector2(1, 1);
        }

        if (PlayCardList.Count == 4)
        {
            ShowSkillText();
            UseBtn.gameObject.SetActive(true);
        }
        else
        {
            HideSkillText();
            UseBtn.gameObject.SetActive(false);
        }               
    }

    //以下两个函数如名
    public bool MoveCardToPlayArea(CardItem card)
    {
        CardItem nowcard = card;
        //Debug.Log(nowcard);

        if (PlayCardList.Count < 4 && FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            PlayCardList.Add(nowcard);
            UpdatePlayCardPos();
            //Debug.Log("已经移动卡牌至出牌区");
            cardItemList.Remove(nowcard);
            UpdateCardItemPos();
            //Debug.Log("现在手牌区和出牌区各有" + cardItemList.Count + " " + PlayCardList.Count + "张牌");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveCardToHandArea(CardItem card)
    {
        CardItem nowcard = card;
        Debug.Log(nowcard);
        cardItemList.Add(nowcard);
        UpdateCardItemPos();
        //Debug.Log("已经移动卡牌至手牌");
        PlayCardList.Remove(nowcard);
        UpdatePlayCardPos();
        Debug.Log("现在手牌区和出牌区各有" + cardItemList.Count + " " + PlayCardList.Count + "张牌");
    }

    public int GetCardNum()
    {
        return cardItemList.Count;
    }

    public int GetPlayCardNum()
    {
        return PlayCardList.Count;
    }

    //删除卡牌物体
    public void RemoveCard(CardItem item, bool ifInPlayArea)
    {
        //AudioManager.Instance.PlayEffect("");//删除音效（文件夹路径

        item.enabled = false;//禁用卡牌逻辑

        //从集合中删除
        if (ifInPlayArea)
            PlayCardList.Remove(item);
        else 
            cardItemList.Remove(item);

        //刷新卡牌位置
        UpdateCardItemPos();
        UpdatePlayCardPos();

        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject,1);

    }

    //清除一个在list中的buff
    public void RemoveBuff(BuffItem item)
    {
        item.enabled = false;
        BuffList.Remove(item);

        Destroy(item.gameObject);
    }

    //让所有的buff剩余轮次减一
    public void BuffPassTurn()
    {
        for(int i = BuffList.Count - 1; i >= 0; i--)
        {
            BuffList[i].PassTurn();
        }

        refreshBuff();
    }

    public void refreshBuff()
    {
        BuffDescription.GetComponent<BuffDescription>().RefreshBuffText(); ;
    }

    public List<BuffItem> returnBuffList()
    {
        return BuffList;
    } 

    //清空所有卡牌
    public void RemoveAllCards(bool ifInPlayArea)
    {
        if (!ifInPlayArea)
        {
            for (int i = cardItemList.Count - 1; i >= 0; i--)
            {
                RemoveCard(cardItemList[i], ifInPlayArea);
            }
        }
        else
        {
            for (int i = PlayCardList.Count - 1; i >= 0; i--)
            {
                RemoveCard(PlayCardList[i], ifInPlayArea);
            }
        }
    }
}
