using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    private List<TaskItem> Tasks;

    //用来记录一局游戏中各个任务的完成的变量
    //通用宝物部分
    private int MaxArmor_1 = 0;
    private bool ifHasArmorAtEnd_2 = false;
    private int ArmorGot_3 = 0;

    private bool ifHealthLowerHalf_11 = false;
    private int HealthRecovered_12 = 0;
    private bool IfHealthAtEndLower30p_13 = false;

    private bool IfHealthAtEndLarger80p_21 = false;
    private int DamageCaused_22 = 0; // 表格里给了个X，我自己给了100
    private int MaxDamage_23 = 0; //给了20


    //职业宝物部分
    private int ArmorGot_Skill_4 = 0;
    private bool ifArmorGThealth_14 = false;
    private bool ifArmorLT0_24 = false;

    private bool ifHealthLT50_ATFirst5Round_5 = false;
    private int HealthLT30_Round_15 = 0;
    private bool ifHealthAtEndIs1_25 = false;

    private bool ifDefendGT10_6 = false;
    private int MaxDamageGT25_16 = 0;
    private bool IfEndIn5Round_26 = false;

    public void Start()
    {
        Instance = this;
        Instance.TaskInit();
    }

    //重置任务变量
    public void ReInitiateTaskVariable()
    {
        MaxArmor_1 = 0;
        ifHasArmorAtEnd_2 = false;
        ArmorGot_3 = 0;

        ifHealthLowerHalf_11 = false;
        HealthRecovered_12 = 0;
        IfHealthAtEndLower30p_13 = false;

        IfHealthAtEndLarger80p_21 = false;
        DamageCaused_22 = 0; // 表格里给了个X，我自己给了100
        MaxDamage_23 = 0; //给了20

        ArmorGot_Skill_4 = 0;
        ifArmorGThealth_14 = false;
        ifArmorLT0_24 = false;

        ifHealthLT50_ATFirst5Round_5 = false;
        HealthLT30_Round_15 = 0;
        ifHealthAtEndIs1_25 = false;

        ifDefendGT10_6 = false;
        MaxDamageGT25_16 = 0;
        IfEndIn5Round_26 = false;
}

    //根据宝物的属性获取对应的任务id，当宝物已解锁时会获得id=0，此时表明此宝物已解锁对应任务已无法获取
    public TaskItem GetTreasureTask(TreasureItem item)
    {
        if (item.IfUnlock == true)
            return null;
        TaskItem task = Tasks.Find(t => t.TaskCategory == item.TCategory && t.Tasklevel == item.Tlevel && t.TaskPro == item.TPro);
        if (task.IfFinished == true)
            task.IfFinished = false;
        return task;
    }
    

    //完成一个任务
    public void FinishTask(TaskItem item)
    {
        item.IfFinished = true;
        RoleManager.Instance.UnlockTreasure(item.TaskPro, item.Tasklevel, item.TaskCategory);
    }

    //一局游戏结束时，检查某个任务是否完成
    public void checkTask(TaskItem item)
    {
        switch (item.taskId)
        {
            case 1:
                if(MaxArmor_1 > 20)
                {
                    FinishTask(item);
                }
                break;

            case 2:
                if(ifHasArmorAtEnd_2)
                {
                    FinishTask(item);
                }
                break;

            case 3:
                if (ArmorGot_3 >= 50)
                {
                    FinishTask(item);
                }
                break;

            case 4:
                if(ArmorGot_Skill_4 > 30)
                {
                    FinishTask(item);
                }
                break;

            case 5:
                if (ifHealthLT50_ATFirst5Round_5)
                {
                    FinishTask(item);
                }
                break;

            case 6:
                if (!ifDefendGT10_6)
                {
                    FinishTask(item);
                }
                break;

            case 11:
                if (!ifHealthLowerHalf_11)
                {
                    FinishTask(item);
                }
                break;

            case 12:
                if (HealthRecovered_12 > FightManager.Instance.MaxHP)
                {
                    FinishTask(item);
                }
                break;

            case 13:
                if (IfHealthAtEndLower30p_13)
                {
                    FinishTask(item);
                }
                break;

            case 14:
                if (ifArmorGThealth_14)
                {
                    FinishTask(item);
                }
                break;

            case 15:
                if (HealthLT30_Round_15 >= 3)
                {
                    FinishTask(item);
                }
                break;

            case 16:
                if (MaxDamageGT25_16 > 25)
                {
                    FinishTask(item);
                }
                break;

            case 21:
                if (IfHealthAtEndLarger80p_21)
                {
                    FinishTask(item);
                }
                break;

            case 22:
                if (DamageCaused_22 > 100)
                {
                    FinishTask(item);
                }
                break;

            case 23:
                if (MaxDamage_23 > 20)
                {
                    FinishTask(item);
                }
                break;

            case 24:
                if (!ifArmorLT0_24)
                {
                    FinishTask(item);
                }
                break;

            case 25:
                if (ifHealthAtEndIs1_25)
                {
                    FinishTask(item);
                }
                break;

            case 26:
                if (IfEndIn5Round_26)
                {
                    FinishTask(item);
                }
                break;

        }
    }

    public void matchTask(int taskid)
    {
        if (RoleManager.Instance.Task_1 == null && RoleManager.Instance.Task_2 == null)
            return;

        if (RoleManager.Instance.Task_1.taskId == taskid || RoleManager.Instance.Task_2.taskId == taskid)
        {
            if(taskid == 2)
            {
                if(FightManager.Instance.DefCount > 0)
                {
                    ifHasArmorAtEnd_2 = true;
                }
            }
            else if(taskid == 5)
            {
                if(FightManager.Instance.CurHP < FightManager.Instance.MaxHP / 2 && FightManager.Instance.TurnCount <= 5)
                {
                    ifHealthLT50_ATFirst5Round_5 = true;
                }
            }
            else if(taskid == 6)
            {
                if(FightManager.Instance.DefCount > 10)
                {
                    ifDefendGT10_6 = true;
                }
            }
            else if (taskid == 11)
            {
                if (FightManager.Instance.CurHP < FightManager.Instance.MaxHP / 2)
                {
                    ifHealthLowerHalf_11 = true;
                }
            }
            else if(taskid == 13)
            {
                if(FightManager.Instance.CurHP < (int)((float)FightManager.Instance.MaxHP * 0.3))
                {
                    IfHealthAtEndLower30p_13 = true;
                }
            }
            else if(taskid == 14)
            {
                if (FightManager.Instance.CurHP < FightManager.Instance.DefCount)
                {
                    ifArmorGThealth_14 = true;
                }
            }
            else if (taskid == 15)
            {
                if (FightManager.Instance.CurHP < (int)((float)FightManager.Instance.MaxHP * 0.3))
                {
                    HealthLT30_Round_15++;
                }
                else
                {
                    HealthLT30_Round_15 = 0;
                }
            }
            else if (taskid == 21)
            {
                if (FightManager.Instance.CurHP > (int)((float)FightManager.Instance.MaxHP * 0.8))
                {
                    IfHealthAtEndLarger80p_21 = true;
                }
            }
            else if(taskid == 24)
            {
                if(FightManager.Instance.DefCount <= 0)
                {
                    ifArmorLT0_24 = true;
                }
            }
            else if(taskid == 25)
            {
                if(FightManager.Instance.CurHP == 1)
                {
                    ifHealthAtEndIs1_25 = true;
                }
            }
            else if(taskid == 26)
            {
                if(FightManager.Instance.TurnCount <= 5)
                {
                    IfEndIn5Round_26 = true;
                }
            }
        }
    }
    public void matchTask(int taskid, int temp)
    {
        if (RoleManager.Instance.Task_1 == null && RoleManager.Instance.Task_2 == null)
            return;

        if(RoleManager.Instance.Task_1.taskId == taskid || RoleManager.Instance.Task_2.taskId == taskid)
        {
            if(taskid == 1)
            {
                if(temp > MaxArmor_1)
                {
                    MaxArmor_1 = temp;
                }
            }
            else if(taskid == 3)
            {
                ArmorGot_3 += temp;
            }
            else if(taskid == 4)
            {
                ArmorGot_Skill_4 += temp;
            }
            else if(taskid == 12)
            {
                HealthRecovered_12 += temp;
            }
            else if(taskid == 16)
            {
                if(temp > MaxDamageGT25_16)
                    MaxDamageGT25_16 = temp;
            }
            else if(taskid == 22)
            {
                DamageCaused_22 += temp;
            }
            else if(taskid == 23)
            {
                if (temp > MaxDamage_23)
                    MaxDamage_23 = temp;
            }
            
        }
    }

    public void TaskInit()
    {
        Tasks = new List<TaskItem>();
        Tasks.Add(new TaskItem(1, TreasureCategory.BUFF, TreasureLevel.RARE, TreasurePro.GENERAL));
        Tasks.Add(new TaskItem(2, TreasureCategory.BUFF, TreasureLevel.EPIC, TreasurePro.GENERAL));
        Tasks.Add(new TaskItem(3, TreasureCategory.BUFF, TreasureLevel.LEGEND, TreasurePro.GENERAL));
        Tasks.Add(new TaskItem(4, TreasureCategory.BUFF, TreasureLevel.LEGEND, TreasurePro.PALADIN));
        Tasks.Add(new TaskItem(5, TreasureCategory.BUFF, TreasureLevel.LEGEND, TreasurePro.MONK));
        Tasks.Add(new TaskItem(6, TreasureCategory.BUFF, TreasureLevel.LEGEND, TreasurePro.SAMURAI));

        Tasks.Add(new TaskItem(11, TreasureCategory.ROUND, TreasureLevel.RARE, TreasurePro.GENERAL));
        Tasks.Add(new TaskItem(12, TreasureCategory.ROUND, TreasureLevel.EPIC, TreasurePro.GENERAL));
        Tasks.Add(new TaskItem(13, TreasureCategory.ROUND, TreasureLevel.LEGEND, TreasurePro.GENERAL));
        Tasks.Add(new TaskItem(14, TreasureCategory.ROUND, TreasureLevel.LEGEND, TreasurePro.PALADIN));
        Tasks.Add(new TaskItem(15, TreasureCategory.ROUND, TreasureLevel.LEGEND, TreasurePro.MONK));
        Tasks.Add(new TaskItem(16, TreasureCategory.ROUND, TreasureLevel.LEGEND, TreasurePro.SAMURAI));

        Tasks.Add(new TaskItem(21, TreasureCategory.PERGAME, TreasureLevel.RARE, TreasurePro.GENERAL));
        Tasks.Add(new TaskItem(22, TreasureCategory.PERGAME, TreasureLevel.EPIC, TreasurePro.GENERAL));
        Tasks.Add(new TaskItem(23, TreasureCategory.PERGAME, TreasureLevel.LEGEND, TreasurePro.GENERAL));
        Tasks.Add(new TaskItem(24, TreasureCategory.PERGAME, TreasureLevel.LEGEND, TreasurePro.PALADIN));
        Tasks.Add(new TaskItem(25, TreasureCategory.PERGAME, TreasureLevel.LEGEND, TreasurePro.MONK));
        Tasks.Add(new TaskItem(26, TreasureCategory.PERGAME, TreasureLevel.LEGEND, TreasurePro.SAMURAI));

    }


}
