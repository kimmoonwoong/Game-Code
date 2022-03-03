using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class ItemHealingEft : ItemEffect
{
    public int healingPoint = 0;
    GameObject player = null;
    public override bool ExecuteRole()
    {
        Debug.Log("PlayerHp Add: " + healingPoint);
        player = GameObject.Find("Player");
        Player play = player.GetComponent<Player>();
        play.health += healingPoint;
        return true;
    }
}
