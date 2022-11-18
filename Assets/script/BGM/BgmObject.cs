using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmObject : MonoBehaviour
{

    private void Awake()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(objs[0]);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}


