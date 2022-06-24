using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiLifeEnemy : MonoBehaviour {
   [SerializeField]private TMP_Text life;
   [SerializeField] private ActorWorld currentActor;

   private void Update() {
      var curhealt = currentActor.CurrentHealth;
      if (curhealt < 0) {
         curhealt = 0;
      }
      life.text = curhealt + "" ;
   }

}
