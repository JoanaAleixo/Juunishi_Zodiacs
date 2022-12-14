using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] GameObject cameraMover;

    [SerializeField] Transform posišaoCamera;
    [SerializeField] GameObject[] cenarios;

    public GameObject CameraMover { get => cameraMover; set => cameraMover = value; }
    public Transform PosišaoCamera { get => posišaoCamera; set => posišaoCamera = value; }

    public void BotaoMovimento(int cenario)
    {
        PosišaoCamera = cenarios[cenario].transform;
        CameraMover.transform.position = new Vector3 (PosišaoCamera.transform.position.x, PosišaoCamera.transform.position.y, -10) ;
    }
}
