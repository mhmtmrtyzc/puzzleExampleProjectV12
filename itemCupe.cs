using UnityEngine;
using System.Collections;
[System.Serializable]
public class itemCupe
{
    public Sprite cupeResim;
    public int cupeQueue;
    public string cupeName;
    public int cupeStat;
   

    

    public itemCupe(string imgCupe,int cupeQue,string name,int cupeType)
    {
        cupeResim = Resources.Load<Sprite>(imgCupe);
        cupeQueue = cupeQue;
        cupeName = name;
        cupeStat = cupeType;

    }
}
