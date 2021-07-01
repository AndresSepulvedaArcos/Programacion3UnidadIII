using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Database",menuName ="Arcos/DatabaseMenu")]
public class ScritableDatabase : ScriptableObject
{
    public List<Rows> table = new List<Rows>();

}
