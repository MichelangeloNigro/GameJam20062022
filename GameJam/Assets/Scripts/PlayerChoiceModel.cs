using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceModel : MonoBehaviour {

    public List<GameObject> playerModels;
    
    void Start()
    {
        foreach (var model in playerModels) {
            model.SetActive(false);
        }
        var index = Random.Range(0, playerModels.Count);
        playerModels[index].SetActive(true);

    }
}
