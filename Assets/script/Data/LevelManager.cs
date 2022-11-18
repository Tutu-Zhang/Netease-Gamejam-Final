using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int level;
    public int AttackFix;
    public int DefFix;
    public float diffCount;

    private void Awake()
    {
        Debug.Log("LevelManager∆Ù∂Ø");
        Instance = this;
        level = -1;
        AttackFix = 5;
        DefFix = 5;
        diffCount = 0.5f;
        Debug.Log(level);
    }
}
