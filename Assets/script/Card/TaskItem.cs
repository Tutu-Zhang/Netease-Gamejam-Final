using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskItem : MonoBehaviour
{
    public int taskId = -1;
    public TreasureLevel Tasklevel;
    public TreasureCategory TaskCategory;
    public TreasurePro TaskPro;
    public bool IfFinished = false;

    public TaskItem(int taskid, TreasureCategory category, TreasureLevel tasklevel, TreasurePro pro)
    {
        taskId = taskid;
        Tasklevel = tasklevel;
        TaskCategory = category;
        TaskPro = pro;
        IfFinished = false;
    }

}
