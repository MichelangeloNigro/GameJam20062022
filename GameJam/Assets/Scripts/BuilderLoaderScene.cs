using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuilderLoaderScene : MonoBehaviour {
   [SerializeField] string sceneName;




   public string LoadSceneSelected() => sceneName;
}
