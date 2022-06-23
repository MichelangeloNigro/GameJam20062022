[System.Serializable]
public class Status {
	public bool isAfflicted;
	public int numberOfTurnAfflicted;

	public void Paralize(ActorWorld currentActor) {
		currentActor.FinishTurn();
	}
	
	
	
}