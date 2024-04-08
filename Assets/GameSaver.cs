using UnityEngine;

public class GameSaver
{
    public void SavePlayerData(Transform playerTransform)
    {
        PlayerPrefs.SetFloat("PlayerX", playerTransform.position.x);
        PlayerPrefs.SetFloat("PlayerY", playerTransform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", playerTransform.position.z);

        PlayerPrefs.SetFloat("PlayerRX", playerTransform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("PlayerRY", playerTransform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("PlayerRZ", playerTransform.rotation.eulerAngles.z);

        PlayerPrefs.Save();
    }

    public void LoadSavedPlayerData(Transform playerTransform)
    {
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");

        playerTransform.position = new Vector3(x, y, z);

        x = PlayerPrefs.GetFloat("PlayerRX");
        y = PlayerPrefs.GetFloat("PlayerRY");
        z = PlayerPrefs.GetFloat("PlayerRZ");

        playerTransform.rotation = Quaternion.Euler(x, y, z);
    }

    public void SaveCameraData(Transform cameraTransform)
    {
        PlayerPrefs.SetFloat("CameraRX", cameraTransform.rotation.eulerAngles.x);

        PlayerPrefs.Save();
    }
}
