using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManagerObject : MonoBehaviour
{

    private void Awake()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("RoleManager");

        if (objs.Length > 1)
        {
            //Destroy(objs[0]);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}