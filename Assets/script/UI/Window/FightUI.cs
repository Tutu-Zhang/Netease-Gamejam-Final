using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FightUI : UIBase
{
    //private Text cardCountText;//��������
    //private Text noCardCountText;//���ƶ�����
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
    private String skillText = "��������";

    private List<CardItem> cardItemList; //����������
    private List<CardItem> PlayCardList; //����������
    private List<BuffItem> BuffList; //�ƾ���Buff���ϣ����ڲ�������ҶԴ����Ե���buff���Լ���buff��������

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
        //Debug.Log("�ҵ�Ѫ��");
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

        //��ȡ�غ��л���ť
        turnBtn.onClick.AddListener(onChangeTurnBtn);
        //��ȡ���ư�ť
        UseBtn.onClick.AddListener(UseCard);

        UpdateHP();
        UpdateDef();


        //UpdateCardCount();
        //UpdateUsedCardCount();
        //UpdatePower();
    }

    //��һغϽ������л������˻غ�
    private void onChangeTurnBtn()
    {
        AudioManager.Instance.PlayEffect("��ť2");
        //ֻ����Ҳ����л�
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
        Debug.Log("�غ��л�");
    }

    //�����
    private void UseCard()
    {
        AudioManager.Instance.PlayEffect("��ť2");
        if (FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            string cardId = "";
            for (int i = 0; i < PlayCardList.Count; i++)
            {
                cardId = cardId + PlayCardList[i].GetCardNum().ToString();
            }
            //Debug.Log(cardId);


            CardEffects.MatchCard(cardId, GetSpecialSkillLevel(cardId)); //Matchcard˳���ִ�п���Ч��
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

    //��ʾ����Ч��
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

        skillText = "��������";

    }

    //���ؿ���Ч��
    private void HideSkillText()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("SkillDes");

        obj.GetComponent<Text>().text = skillText;
    }

    //Ѱ��buff�б����Ƿ���buffidΪ������buff
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


    //������������
    public void CreatCardItem(int count)
    {
        for (int i = 0; i < count ; i++)
        {
            
            //Id	Name	Script	Type	Des	BgIcon	Icon	Expend	Arg0	Effects
            //Ψһ�ı�ʶ�������ظ���	����	������ӵĽű�	�������͵�Id	����	���Ƶı���ͼ��Դ·��	ͼ����Դ��·��	���ĵķ���	����ֵ	��Ч
            //1000	��ͨ����	AttackCardItem	10001	�Ե������˽���{0}����˺�	Icon/BlueCard	Icon/sword_03e	1	3	Effects/GreenBloodExplosion

            GameObject obj = Instantiate(Resources.Load("UI-img/card/cardBackground"), transform.Find("CardArea") ,false) as GameObject;//���ؿ���UI�������ݸ���������
            obj.GetComponent<Transform>().position = new Vector2(-3.1f, -3.2f);
            //var Item = obj.AddComponent<CardItem>();
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            //Debug.Log("��ǰ������������Ϊ" + System.Type.GetType(data["Script"]));
            
            CardItem Item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            Item.Init(data,i);
            cardItemList.Add(Item);
            //Debug.Log("������" + cardItemList.Count + "����");
        }
    }


    //���������addBuff������bufflist���buff����������ڽ��ܵĲ�����ͬ
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
            //Debug.Log("Ŀǰ��buff:" + BuffList[i]);
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
            //Debug.Log("Ŀǰ��buff:" + BuffList[i].GetBuffId());
        }
    }



    //��������λ��
    public void UpdateCardItemPos()
    {
        //Debug.Log("��������λ��");
        for(int i = 0; i < cardItemList.Count; i++)
        {
            Transform card = cardItemList[i].transform;
            card.position = cardZone.transform.GetChild(i).position;
            card.transform.localScale = new Vector2(1,1);
        }
     
    }

    //���³�����λ��
    public void UpdatePlayCardPos()
    {
        //Debug.Log("�����Ϸ�����λ��");
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

    //����������������
    public bool MoveCardToPlayArea(CardItem card)
    {
        CardItem nowcard = card;
        //Debug.Log(nowcard);

        if (PlayCardList.Count < 4 && FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            PlayCardList.Add(nowcard);
            UpdatePlayCardPos();
            //Debug.Log("�Ѿ��ƶ�������������");
            cardItemList.Remove(nowcard);
            UpdateCardItemPos();
            //Debug.Log("�����������ͳ���������" + cardItemList.Count + " " + PlayCardList.Count + "����");
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
        //Debug.Log("�Ѿ��ƶ�����������");
        PlayCardList.Remove(nowcard);
        UpdatePlayCardPos();
        Debug.Log("�����������ͳ���������" + cardItemList.Count + " " + PlayCardList.Count + "����");
    }

    public int GetCardNum()
    {
        return cardItemList.Count;
    }

    public int GetPlayCardNum()
    {
        return PlayCardList.Count;
    }

    //ɾ����������
    public void RemoveCard(CardItem item, bool ifInPlayArea)
    {
        //AudioManager.Instance.PlayEffect("");//ɾ����Ч���ļ���·��

        item.enabled = false;//���ÿ����߼�

        //�Ӽ�����ɾ��
        if (ifInPlayArea)
            PlayCardList.Remove(item);
        else 
            cardItemList.Remove(item);

        //ˢ�¿���λ��
        UpdateCardItemPos();
        UpdatePlayCardPos();

        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject,1);

    }

    //���һ����list�е�buff
    public void RemoveBuff(BuffItem item)
    {
        item.enabled = false;
        BuffList.Remove(item);

        Destroy(item.gameObject);
    }

    //�����е�buffʣ���ִμ�һ
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

    //������п���
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
