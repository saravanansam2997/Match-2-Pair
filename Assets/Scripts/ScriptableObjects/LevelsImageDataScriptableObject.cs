using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelsManagerData", menuName = "ScriptableObjects/LevelsManagerScriptableObject", order = 1)]
public class LevelsManagerScriptableObject : ScriptableObject
{

    // Start is called before the first frame update
   public List<Sprite> levelsSprites;
   public List<GridSizeData> levelsGridSize;
 
}

public enum GridSize
{
    Zero=0,
    ONE=1,
    TWO=2,
    THERE=3,
    FOUR=4,
    FIVE=5,
    SIX=6,
    SEVEN=7,
    EIGHT=8,
    NINE=9    
}
public enum Difficulty
{
    None,
    Easy,
    Medium,
    Hard
}

[System.Serializable]
public class GridSizeData
{
public GridSize RowGrid=GridSize.Zero;
public GridSize ColumnGrid=GridSize.Zero;
public Vector3 gridSize_offset=Vector3.zero;
public  Vector3 gridSize_Scale=Vector3.zero;
public Vector3 gridSize_Position=Vector3.zero;

}