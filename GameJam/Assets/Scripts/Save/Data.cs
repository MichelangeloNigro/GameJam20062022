using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Data
{
    public string currScene;
    public Vector3 Position;
    public List<string> activeGenerator=new List<string>();
    public List<string> pickedUp=new List<string>();
    public List<string> diaryPage=new List<string>();
    public int Corda;
    public int Picchetti;
    public float timePlayed;

}
