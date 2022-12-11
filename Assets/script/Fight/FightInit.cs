using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

            case 1:
                EnemyManager.Instance.loadRes("10000");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("ǳ��ս��Infinite Horizons - FiftySounds");
                break;
            case 2:
                EnemyManager.Instance.loadRes("10001");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("ǳ��ս��Infinite Horizons - FiftySounds");
                break;
            case 3:
                EnemyManager.Instance.loadRes("10002");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("ǳ��ս��Infinite Horizons - FiftySounds");
                break;
            case 4:
                EnemyManager.Instance.loadRes("10003");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("Boss2����֮��");
                break;
            case 5:
                EnemyManager.Instance.loadRes("10004");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("�к�ս��Unnatural");
                break;
            case 6:
                EnemyManager.Instance.loadRes("10005");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("�к�ս��Unnatural");
                break;
            case 7:
                EnemyManager.Instance.loadRes("10006");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("�к�ս��Unnatural");
                break;
            case 8:
                EnemyManager.Instance.loadRes("10007");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("Boss2����֮��");
                break;
            case 9:
                EnemyManager.Instance.loadRes("10008");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("�ս��Morpheus");
                break;
            case 10:
                EnemyManager.Instance.loadRes("10009");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("�ս��Morpheus");
                break;
            case 11:
                EnemyManager.Instance.loadRes("100010");
                //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayBGM("�ս��Morpheus");
                break;
            case 12:
                switch (RoleManager.Instance.PlayerProfession)
                {
                    case Professions.PALADIN:
                        EnemyManager.Instance.loadRes("100011");
                        //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                        if (AudioManager.Instance != null)
                            AudioManager.Instance.PlayBGM("Boss2����֮��");
                        break;
                    case Professions.MONK:
                        EnemyManager.Instance.loadRes("100012");
                        //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                        if (AudioManager.Instance != null)
                            AudioManager.Instance.PlayBGM("Boss2����֮��");
                        break;  
                    case Professions.SAMURAI:
                        EnemyManager.Instance.loadRes("100013");
                        //����ս��bgm������ֻ��Ҫ����bgm�����־Ϳ���
                        if (AudioManager.Instance != null)
                            AudioManager.Instance.PlayBGM("Boss2����֮��");
                        break;
                }
                break;
        }


        //�л�����һغ�
        FightManager.Instance.ChangeType(FightType.Player);
    }





    public override void OnUpdate()
    {

    }
}
