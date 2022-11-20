using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCardManager 
{
    public static FightCardManager Instance = new FightCardManager();
    //������������ֵΪ�鵽0�ĸ���, ���ƴ���鵽1�ĸ��ʵĴ�С
    private static float CARD_PROBABILITY_LOW = 0.6f;
    private static float CARD_PROBABILITY_HIGH = 0.4f;
    public float card_probability;

    public List<string> cardList;//�ƶ�����ƣ�Ҳ���ǲ�������������

    //public List<string> usedCardList;//���ƶ�



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

            cardList.Add(num);//�൱�������б�
        }

       // Debug.Log(cardList.Count);//������ƶ�����
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
            cardList.Add(num);//�ƿ⣬������û��ӡ3���ƣ���Ϊunity�Դ���random��������������ӡһ�ų�һ�ź����׳���ȫ1����ȫ0
            //Debug.Log("����ӡ��Ϊ" + num + ",�鿨����Ϊ" + card_probability);
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

    //�Ƿ��п�
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //�鿨
    public string DrawCard()
    {
        if(!HasCard())
            PrintCard();

        string id = cardList[cardList.Count - 1];//�ƿ⣿
        cardList.RemoveAt(cardList.Count - 1);//����Ӧ���ƴ��ƿ����Ƴ�
        return id;
    }


}
