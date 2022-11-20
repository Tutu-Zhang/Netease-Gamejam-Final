using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManager 
{
    public static FightCardManager Instance = new FightCardManager();
    //以下两个的数值为抽到0的概率, 名称代表抽到1的概率的大小
    private static float CARD_PROBABILITY_LOW = 0.6f;
    private static float CARD_PROBABILITY_HIGH = 0.4f;
    public float card_probability;

    public List<string> cardList;//牌堆里的牌，也就是不在玩家手里的牌

    //public List<string> usedCardList;//弃牌堆



    public void Init()
    {
        card_probability = CARD_PROBABILITY_LOW;
        cardList = new List<string>();

        //usedCardList = new List<string>();

        System.Random random = new System.Random();
        
        while (cardList.Count<6)
        {
            string num = "0";
            double temp = random.NextDouble();
            if (temp >= CARD_PROBABILITY_LOW)
            {
                num = "1";
            }

            cardList.Add(num);//相当于手牌列表
        }

       // Debug.Log(cardList.Count);//输出手牌堆数量
    }

    public void PrintCard()
    {
        System.Random random = new System.Random();


        while (cardList.Count < 3)
        {
            string num = "0";
            double temp = random.NextDouble();
            if (temp >= card_probability)
            {
                num = "1";
            }
            cardList.Add(num);//牌库，现在是没牌印3张牌，因为unity自带的random并非真随机，如果印一张抽一张很容易出现全1或者全0
            //Debug.Log("本次印牌为" + num + ",抽卡概率为" + card_probability);
        }

       
    }


    public void SetPro_Low()
    {
        card_probability = CARD_PROBABILITY_LOW;
    }

    public void SetPro_High()
    {
        card_probability = CARD_PROBABILITY_HIGH;
    }

    //是否有卡
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //抽卡
    public string DrawCard()
    {
        if(!HasCard())
            PrintCard();

        string id = cardList[cardList.Count - 1];//牌库？
        cardList.RemoveAt(cardList.Count - 1);//将对应卡牌从牌库中移除
        return id;
    }


}
