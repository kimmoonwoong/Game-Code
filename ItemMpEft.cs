using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ItemEft/Consumable/Mp")]
public class ItemMpEft : ItemEffect
{
    public int mpPoint = 0;
    GameObject player = null;
    public override bool ExecuteRole()
    {
        player = GameObject.Find("Player");
        Player play = player.GetComponent<Player>();

        play.mp += mpPoint;
        return true;
    }

}
