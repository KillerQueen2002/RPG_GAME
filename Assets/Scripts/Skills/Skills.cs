using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
   [SerializeField] protected float coolDown;
    protected float cooldownTimer;
    
    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }


    public virtual bool CanUseSkill()
    {
        if(cooldownTimer < 0)
        {
            
            cooldownTimer = coolDown;
            return true;    
        }   
        return false;
    }

    public virtual void UseSkill()
    {
        //su dung ki nang
    }


}
