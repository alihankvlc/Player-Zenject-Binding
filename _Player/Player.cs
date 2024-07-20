using Assets.KVLC;
using UnityEngine;

public class Player
{
    public FirstPersonController FirstPersonController { get; private set; }
    public CharacterController CharacterController { get; private set; }
    public Animator Animator { get; private set; }
    public InputHandler Input { get; private set; }
    public PlayerData Data { get; private set; }
    public SaveLoadService SaveLoadService { get; private set; }

    public Player(PlayerData data, CharacterController controller, FirstPersonController firstPersonController, Animator anim,
        InputHandler inputHandler, SaveLoadService saveLoadService)
    {
        this.SaveLoadService = saveLoadService;
        this.FirstPersonController = firstPersonController;
        this.Data = data;
        this.CharacterController = controller;
        this.Animator = anim;
        this.Input = inputHandler;
    }

    public void SavePlayerData()
    {
        Vector3 playerPosition = CharacterController.transform.position;
        Vector3 playerRotation = CharacterController.transform.rotation.eulerAngles;

        Vector3 playerCameraRotation = FirstPersonController.CameraSettings.Target.transform.eulerAngles;
        Vector3 finalCameraRotation = new(playerCameraRotation.x, 0.0f, 0.0f);

        Data.SetPlayerPosition(playerPosition);
        Data.SetPlayerRotation(playerRotation);
        Data.SetPlayerCameraRotation(finalCameraRotation);

        SaveLoadService.SaveData(Data);
    }
    public void LoadPlayerData()
    {
        if (SaveLoadService.ContainsKey<PlayerData>())
        {
            PlayerData loadedData = SaveLoadService.LoadData<PlayerData>();

            Data.SetPlayerPosition(loadedData.GetPlayerPosition);
            Data.SetPlayerRotation(loadedData.GetPlayerRotation);
            Data.SetPlayerCameraRotation(loadedData.GetPlayerCameraRotation);

            CharacterController.transform.position = Data.GetPlayerPosition;
            CharacterController.transform.rotation = Quaternion.Euler(loadedData.GetPlayerRotation);

            FirstPersonController.CameraSettings.Target.transform.localRotation =
                Quaternion.Euler(loadedData.GetPlayerCameraRotation);
        }
    }
    public void ClearPlayerData()
    {
        SaveLoadService.Clear<PlayerData>();
    }

    public void SetCursorMode(CursorLockMode mode)
    {
        Cursor.visible = mode == CursorLockMode.None;
        Cursor.lockState = mode;
    }
}
