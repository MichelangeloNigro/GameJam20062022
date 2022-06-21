using UnityEngine;

public abstract class HealthCard : GeneralCard {
  [SerializeField] public int amount;

  private void Cure() {
    //player.health += amount;
  }
  public override void Use(ActorWorld chooser, ActorWorld target){}
}
