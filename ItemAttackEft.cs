using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ItemEft/Consumable/Attack")]
public class ItemAttackEft : ItemEffect
{
    public int attackPoint = 0;
    GameObject player = null;
    public override bool ExecuteRole()
    {
        player = GameObject.Find("Player");
        Player play = player.GetComponent<Player>();
        
        play.attackPower += attackPoint;
        return true;
    }
}
