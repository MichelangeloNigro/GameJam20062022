using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWorld : MonoBehaviour
{
    public void Select() {
        TurnManager.Instance.PlayerActor.SelectCard();
    }
}
