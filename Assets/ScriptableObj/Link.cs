using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLink", menuName = "ScriptableObject/Link", order = 2)]
public class Link : ScriptableObject
{
    public Material roadMaterial;
    public Material barrageMaterial;
    public Material neutralMaterial;
    public Material waitingMaterial;
}
