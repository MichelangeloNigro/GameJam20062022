using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceModel : MonoBehaviour {

    public List<GameObject> playerModels;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var model in playerModels) {
            model.SetActive(false);
        }
        var index = Random.Range(0, playerModels.Count);
        playerModels[index].SetActive(true);

    }

}
