using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Player player;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);// 1 instance chi dc su dung boi 1 obj duy nhat
        else
            instance = this;
    }
}
