using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    private static UImanager _instance = null;
    public static UImanager instance { get { return _instance; } }

    [SerializeField]
    private GameObject[] playerHP_Objs = null;

    private void Awake()
    {
        _instance = this;
    }

    public void PlayerHP()
    {
        int minusHP = 3 - Datamanager.instance.playerHP;
        for(int i = 0; i<minusHP; i++)
        {
            playerHP_Objs[i].SetActive(false);
        }
    }
}
