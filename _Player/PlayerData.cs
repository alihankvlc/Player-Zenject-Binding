using Assets.KVLC;
using UnityEngine;

public sealed class PlayerData : ISaveable
{
    [field:SerializeField]
    public Vector3 GetPlayerPosition
    {
        get;
        private set;
    }

    [field: SerializeField]
    public Vector3 GetPlayerRotation
    {
        get;
        private set;
    }

    [field:SerializeField]
    public Vector3 GetPlayerCameraRotation
    {
        get;
        private set;
    }

    public void SetPlayerPosition(Vector3 position)
    {
        GetPlayerPosition = position;
    }
    public void SetPlayerRotation(Vector3 rotation)
    {
        GetPlayerRotation = rotation;
    }
    public void SetPlayerCameraRotation(Vector3 rotation)
    {
        GetPlayerCameraRotation = rotation;
    }
}
