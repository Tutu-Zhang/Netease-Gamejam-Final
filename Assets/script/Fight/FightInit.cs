using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ս����ʼ��
public class FightInit : FightUnit
{
    //�����ҳ��ʱ����
    public void Start()
    {

        //��ʼ�����ñ�
        GameConfigManager.Instance.Init();

        //��ʼ��ս����ֵ
        FightManager.Instance.Init();

        //��ʼ��ս����������
        FightCardManager.Instance.Init();

        //����ս��Ԫ��
        UIManager.Instance.ShowUI<FightUI>("fightBackground");

        int levelCount = LevelManager.Instance.level;
        Debug.Log(levelCount);

        //int levelCount = 0;

        //���عؿ���Դ
        switch (levelCount)
        {
            case -1:
                EnemyManager.Instance.loadRes("10000");
                break;

            case 0:
                EnemyManager.Instance.loadRes("10000");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("��ѧ�ؿ�");
                break;
            case 1:
                EnemyManager.Instance.loadRes("10001");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("С��");
                break;
            case 2:
                EnemyManager.Instance.loadRes("10002");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("ȭͷ��");
                break;
            case 3:
                EnemyManager.Instance.loadRes("10003");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("����");
                break;
            case 4:
                EnemyManager.Instance.loadRes("10004");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("���һ��");
                break;
        }


        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.Player);
    }



    public override void OnUpdate()
    {

    }
}
