using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

//����ϡ�ж�
public enum TreasureLevel {RARE, EPIC, LEGEND }

//���������
public enum TreasureCategory {BUFF, ROUND, PERGAME}

//�����ְҵ������ͨ�õı���ְҵ����ΪGENERAL
public enum TreasurePro {GENERAL, PALADIN, MONK, SAMURAI }

public class TreasureItem
{
    public TreasureLevel Tlevel;
    public TreasureCategory TCategory;
    public TreasurePro TPro;

    //�����������Զ�Ӧ����ϡ�еȼ����������࣬�����ְҵ��ͨ�����������Կ���Ψһȷ��һ�����������ְҵ����
    //��Ϊgeneralʱ�������ϡ�ж�û�����壬��ʱͨ��ְҵ������Ϳ�ȷ��һ������

    public bool Ifready = true;//����������Ƿ����
    public bool IfUnlock = false;//��������Ƿ����
    private int PrepareTime = 0;//��������м��غϿ���


    public TreasureItem(TreasureCategory category, TreasureLevel level, TreasurePro pro)
    {
        Tlevel = level;
        TCategory = category;
        TPro = pro;
    }

    public void TPassTurn()
    {
        if(PrepareTime > 0)
            PrepareTime -= 1;
        if (PrepareTime == 0)
            Ifready = true;
    }

    //ÿ��ʹ�ñ��ﶼҪ����һ�δ˺���
    public void UseTreasure()
    {
        if (!Ifready)
        {
            Debug.Log("���ﲻ����");
            return;
        }
            

        if(TCategory == TreasureCategory.ROUND)
        {
            Ifready = false;
            PrepareTime = 3;
        }
        else 
        {
            Ifready = false;
            PrepareTime = 999;
        }

        TreasureEffects.MatchTreasure(TPro, TCategory, Tlevel);
    }

    //ÿ����Ϸ��ʼǰ����Ҫ�����б���ִ��һ�δ˺����Ա�֤�����»غϿ���
    public void SetTreasureAble()
    {
        Ifready = true;
        PrepareTime = 0;
    }

    public void SetPreTime(int i)
    {
        PrepareTime = i;
    }

    public int GetPreTime()
    {
        return PrepareTime;
    }



}
