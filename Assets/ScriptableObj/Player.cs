using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "ScriptableObject/Player", order = 1)]
public class Player : ScriptableObject {
    public Material material;
    public new string name;

    public int numberOfGamesPlayed = 0;
    public int numberOfWins = 0;
    public int numberOfLoose = 0;
}
