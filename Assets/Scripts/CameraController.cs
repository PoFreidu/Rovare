using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineCamera cinemachineCamera;

    private void Start() {
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow() {
        cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();
        cinemachineCamera.Follow = Player.Instance.transform;
    }
}
