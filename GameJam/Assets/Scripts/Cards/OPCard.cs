using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Card/OpCard",fileName = "OpCard_")]
public class OPCard : AttackCard
{
    public override void Use(ActorWorld chooser, ActorWorld target) {
        Debug.Log($"heal for {damage}");
    }
}
