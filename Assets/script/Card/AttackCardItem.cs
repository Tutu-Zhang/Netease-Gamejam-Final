using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//����Ч������
public class AttackCardItem : CardItem, IPointerDownHandler
{
    private void Awake()
    {
        cardNum = 1;
    }
}
