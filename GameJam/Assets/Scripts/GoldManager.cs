using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour {
   public int numberOfGold;
   public static GoldManager instance;
   
   public void AddGold(int goldObtained) {
      numberOfGold += goldObtained;
   }
}
