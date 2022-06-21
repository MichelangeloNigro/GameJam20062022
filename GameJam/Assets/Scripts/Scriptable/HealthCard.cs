using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/HealthCard")]
public abstract class HealthCard : GeneralCard {
  [SerializeField] private int amount;

  private void Cure() {
    //player.health += amount;
  }
  public override void Use(){}
}
