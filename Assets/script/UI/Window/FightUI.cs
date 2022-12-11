using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FightUI : UIBase
{
    private Text hpText;
    private Image hpImage;
    private Image hpHitImage;
    private Text defText;
    private GameObject playerHPBar;

    private GameObject playCardZone;
    private GameObject cardZone;
    private GameObject cardArea;
    private GameObject enemyCase;

    private List<CardItem> cardItemList; //����������
    private List<CardItem> PlayCardList; //����������
    private List<BuffItem> BuffList; //�ƾ���Buff���ϣ����ڲ�������ҶԴ����Ե���buff���Լ���buff��������
    private List<Button> UseBtnList;//���ư�ť�б�

    Button turnBtn, SklBtn, TBtn1, TBtn2;

    public bool SkillUsed = false;
    private void Awake()
    {
        cardItemList = new List<CardItem>();
        PlayCardList = new List<CardItem>();
        BuffList = new List<BuffItem>();
        UseBtnList = new List<Button>();
        BuffList.Clear();
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
        
        //��ȡ���ư�ť�б�
        DescriptionManager.Instance.CreatlNumToCardSkilDictionary();
        for (int i = 0; i < 8; i++)
        {
            Debug.Log("useBtn" + DescriptionManager.Instance.NumToPair[i]+" i��ֵΪ��"+i);
            UseBtnList.Add(GameObject.Find("useBtn" + DescriptionManager.Instance.NumToPair[i]).GetComponent<Button>());            
            UseBtnList[i].gameObject.SetActive(false);
            UseBtnList[i].onClick.AddListener(UseCard);
        }

        SklBtn = GameObject.Find("����").GetComponent<Button>();
        TBtn1 = GameObject.Find("����1").GetComponent<Button>();
        TBtn2 = GameObject.Find("����2").GetComponent<Button>();

        //��ȡ�غ��л���ť
        turnBtn.onClick.AddListener(onChangeTurnBtn);
        //��ȡ���ܰ�ť�����ί��
        SklBtn.onClick.AddListener(UseSkill);

        TBtn1.onClick.AddListener(UseTreasure1);
        TBtn2.onClick.AddListener(UseTreasure2);

        UpdateHP();
        UpdateDef();

        Debug.Log(RoleManager.Instance.GetProfession());

        if (RoleManager.Instance.GetTreasure(1) != null)
        {
            Debug.Log("���ñ���1Ϊ��Ч" + " " + RoleManager.Instance.GetTreasure(1).TPro + RoleManager.Instance.GetTreasure(1).TCategory);
            RoleManager.Instance.GetTreasure(1).SetTreasureAble();
        }
        if (RoleManager.Instance.GetTreasure(2) != null)
        {
            RoleManager.Instance.GetTreasure(2).SetTreasureAble();
        }

        SetCardSkillDes();//��ʾ��ǰ��������

        TreasureDes();//�滻����ͼ��
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

    private void UseTreasure1()
    {
        if (FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            if(RoleManager.Instance.GetTreasure(1) == null)
            {
                Debug.Log("����1������");
                return;
            }

            if (RoleManager.Instance.GetTreasure(1).IfReady() == true)
            {
                RoleManager.Instance.GetTreasure(1).UseTreasure();
            }
            else
            {
                Debug.Log("����1δ׼���û򲻴���");
            }

        }
    }

    private void UseTreasure2()
    {
        if (FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            if (RoleManager.Instance.GetTreasure(2) == null)
            {
                Debug.Log("����2������");
                return;
            }

            if (RoleManager.Instance.GetTreasure(2).IfReady() == true)
            {
                RoleManager.Instance.GetTreasure(2).UseTreasure();
            }
            else
            {
                Debug.Log("����2δ׼���û򲻴���");
            }

        }
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
            Debug.Log(cardId);

            if (GetSpecialSkillLevel(cardId) != SkillLevel.NONE)
            {
                CardEffects.MatchCard(cardId, GetSpecialSkillLevel(cardId)); //Matchcard˳���ִ�п���Ч��
                StartCoroutine(UseCardEffects(cardId));
                //BuffDescription.GetComponent<BuffDescription>().RefreshBuffText();

                //����ť����
                int cardPairNum = DescriptionManager.Instance.PairToNum[cardId];
                UseBtnList[cardPairNum].gameObject.SetActive(false);
                //�����ƷŴ���Ϊ��������
                GameObject card = GameObject.Find("Image" + cardId);
                card.transform.SetSiblingIndex(DescriptionManager.Instance.PairToNum[cardId]);
                Transform[] cardChild = card.transform.GetComponentsInChildren<Transform>(true);
                foreach (var child in cardChild)
                {
                    child.DOScale(1f, 0.2f);//��ԭ
                }

            }
            else
            {
                Debug.Log("������δ����");
            }

        }
    }

    //�ó�����
    private void UseSkill()
    {
        AudioManager.Instance.PlayEffect("��ť2");
        if (FightManager.Instance.fightUnit is FightPlayerTurn)
        {
            if(SkillUsed == true)
            {
                return;
            }

            if (RoleManager.Instance.GetProSkillLvl() != SkillLevel.NONE && RoleManager.Instance.GetProfession() != Professions.NONE)
            {
                PlayerSkill.MatchSkill(RoleManager.Instance.GetProfession(), RoleManager.Instance.GetProSkillLvl()); //Matchcard˳���ִ�п���Ч��
                //BuffDescription.GetComponent<BuffDescription>().RefreshBuffText();
                SkillUsed = true;
            }
            else
            {
                Debug.Log("�޼���");
            }

        }
    }

    public SkillLevel GetSpecialSkillLevel(string skill)
    {
        return RoleManager.Instance.GetCurrentCardLevel(skill);
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

    //��ʾ����Ч��, ���ڻ�û����ƥ��


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

    public BuffItem FindBuffWithLvl(string id, SkillLevel lvl)
    {
        if (BuffList.Count != 0)
        {
            for (int i = 0; i < BuffList.Count; i++)
            {
                if (BuffList[i].GetBuffId() == id && BuffList[i].GetBuffLevel() == lvl)
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

    //���³�����λ��&��ʾ���ư�ť
    public void UpdatePlayCardPos()
    {
        //Debug.Log("�����Ϸ�����λ��");
        for (int i = 0; i < PlayCardList.Count; i++)
        {
            Transform card = PlayCardList[i].transform;
            card.position = playCardZone.transform.GetChild(i).position;
            card.transform.localScale = new Vector2(1, 1);
        }

        if (PlayCardList.Count == 3)
        {
            //��ȡ��ǰ�Ŀ������
            string cardId = "";
            for (int i = 0; i < PlayCardList.Count; i++)
            {
                cardId = cardId + PlayCardList[i].GetCardNum().ToString();
            }
            //����ť����
            int cardPairNum = DescriptionManager.Instance.PairToNum[cardId];
            UseBtnList[cardPairNum].gameObject.SetActive(true);
            //�����ƷŴ���Ϊ��������
            GameObject card = GameObject.Find("Image" + cardId);
            card.transform.SetAsLastSibling();
            Transform[] cardChild =  card.transform.GetComponentsInChildren<Transform>(true);
            foreach (var child in cardChild)
            {
                child.DOScale(1.2f, 0.2f);//�Ŵ�
            }

        }
        else
        {
            Debug.Log("��δ��ɳ�������");
        }               
    }

    //����������������
    public bool MoveCardToPlayArea(CardItem card)
    {
        CardItem nowcard = card;
        //Debug.Log(nowcard);

        if (PlayCardList.Count < 3 && FightManager.Instance.fightUnit is FightPlayerTurn)
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

    public void TreasurePassTurn()
    {
        if (RoleManager.Instance.GetTreasure(1) != null)
        {
            RoleManager.Instance.GetTreasure(1).TPassTurn();
        }

        if (RoleManager.Instance.GetTreasure(2) != null)
        {
            RoleManager.Instance.GetTreasure(2).TPassTurn();
        }
    }

    public void refreshBuff()
    {
        //BuffDescription.GetComponent<BuffDescription>().RefreshBuffText(); ;
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


    public void SetCardSkillDes()
    {

        GameObject CardDes;
        GameObject CardIcon;
        DescriptionManager.Instance.CreatlNumToCardSkilDictionary();
        for (int i = 0; i < 8; i++)
        {
            CardDes = GameObject.Find("Name" + i.ToString());//��������

            SkillLevel skl = RoleManager.Instance.GetCurrentCardLevel(DescriptionManager.Instance.NumToPair[i]);//�洢��ǰ���Ƶȼ�

            int j = DescriptionManager.Instance.SkillLevelToNum[skl];//ȡ�ö�Ӧ������

            CardDes.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(i, j, "CardSkillDes");

            string imgPath = ExcelReader.Instance.GetProfessionDes(i, j, "CardSkillIcon");
            CardIcon = GameObject.Find("TreasureImg" + i.ToString());
            Texture2D texture = Resources.Load<Texture2D>(imgPath);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            CardIcon.GetComponent<Image>().sprite = sp;
        }
    }

    //���ñ���ͼ��
    public void TreasureDes()
    {
        DescriptionManager.Instance.CreatNumToTreasureDictionary();

        DescriptionManager.Instance.CreatNumToTreasureDictionary();

        TreasureItem treasure1 = RoleManager.Instance.GetTreasure(1);
        TreasureItem treasure2 = RoleManager.Instance.GetTreasure(2);

        //����1
        TreasureLevel treasureLevel1 = treasure1.Tlevel;
        TreasureCategory treasureCategory1 = treasure1.TCategory;
        string TImgPath1 = ExcelReader.Instance.GetProfessionDes(DescriptionManager.Instance.TLevelToNum[treasureLevel1], DescriptionManager.Instance.CategoryToNum[treasureCategory1], "TreasureIcon");
        Debug.Log("����1��ͼ��Ϊ" + TImgPath1);
        GameObject T1 = GameObject.Find("����1");
        Texture2D texture = Resources.Load<Texture2D>(TImgPath1);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        T1.GetComponent<Image>().sprite = sp;

        GameObject T1Text = GameObject.Find("TreasurText1");
        T1Text.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(DescriptionManager.Instance.TLevelToNum[treasureLevel1], DescriptionManager.Instance.CategoryToNum[treasureCategory1] ,"TreasureDes");

        //����2
        TreasurePro treasurePro = treasure2.TPro;
        TreasureCategory treasureCategory2 = treasure2.TCategory;

        Debug.Log("����2��ְҵ������Ϊ"+treasurePro + " " + treasureCategory2);
        string TImgPath2 = ExcelReader.Instance.GetProfessionDes(DescriptionManager.Instance.ProToNum[treasurePro] +2, DescriptionManager.Instance.CategoryToNum[treasureCategory2], "TreasureIcon");
        Debug.Log("����2��ͼ��Ϊ" + TImgPath2);
        GameObject T2 = GameObject.Find("����2");
        Texture2D texture2 = Resources.Load<Texture2D>(TImgPath2);
        Sprite sp2 = Sprite.Create(texture2, new Rect(0, 0, texture2.width, texture2.height), new Vector2(0.5f, 0.5f));
        T2.GetComponent<Image>().sprite = sp2;

        GameObject T2Text = GameObject.Find("TreasurText2");
        T2Text.GetComponent<Text>().text = ExcelReader.Instance.GetProfessionDes(DescriptionManager.Instance.ProToNum[treasurePro] + 2, DescriptionManager.Instance.CategoryToNum[treasureCategory2], "TreasureDes");

    }
}
