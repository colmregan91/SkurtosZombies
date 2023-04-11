using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : NetworkBehaviour
{
    private const string LOCALNAME = "localPlayer";
    private const string REMOTENAME = "remotePlayer";

    [SerializeField] private Transform followTarget;
    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            gameObject.name = LOCALNAME;
            FindObjectOfType<CinemachineVirtualCamera>().Follow = followTarget;
        }
        else
        {
            DisableComponents();
            gameObject.name = REMOTENAME;
        }
    }

    private void DisableComponents()
    {
        GetComponent<PlayerInput>().enabled = false;
        GetComponent<InputController>().enabled = false;
    }

    void Update()
    {
        // Leave blank if not needed
    }
}
