using UnityEngine;
using UnityEngine.Serialization;

public abstract class HealthCard : GeneralCard {
  [SerializeField] public int amount;
  public bool canUnstunnedActor;
  
  private void Cure() {
    //player.health += amount;
  }
  public override void Use(ActorWorld chooser, ActorWorld target){}
}
