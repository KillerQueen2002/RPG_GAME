using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Playermanager giup cac script khac co the tro den Player
    public static PlayerManager instance;
    public Player player;

    private void Awake()
    {
        //ap dung singleton chi co duy nhat 1 Playermanager
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

}
    