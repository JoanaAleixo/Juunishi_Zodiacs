using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayableCaracter")]
public class PlayableCaracterScptObj : CaracterCreation
{
    [SerializeField] FloatVariable _sP;
}
