using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private GameObject editButton;
    [SerializeField] private GameObject editPanel;
    
    private RawImage _profileImage;
    private WebCamTexture _webCamTexture;
    
    private int _cameraNum;

    private void Awake()
    {
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.Camera))
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.Camera);
        
        _profileImage = GetComponent<RawImage>();

        Texture2D loadedPhoto = LoadPhoto();
        if (loadedPhoto) _profileImage.texture = loadedPhoto;
        else _profileImage.enabled = false;
    }

    public void StartEdit()
    {
        if (!_profileImage.enabled) _profileImage.enabled = true;
        
        _webCamTexture = new WebCamTexture();
        _profileImage.texture = _webCamTexture;
        _webCamTexture.Play();

        editButton.SetActive(false);
        editPanel.SetActive(true);
    }

    public void FlipCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0) return;

        if (_cameraNum == 0) _cameraNum = 1;
        else _cameraNum = 0;
        
        _webCamTexture = new WebCamTexture(devices[_cameraNum].name);
        _profileImage.texture = _webCamTexture;
        _webCamTexture.Play();
    }

    public void CapturePhoto()
    {
        Texture2D capturedPhoto = new Texture2D(_webCamTexture.width, _webCamTexture.height);
        capturedPhoto.SetPixels(_webCamTexture.GetPixels());
        capturedPhoto.Apply();

        _profileImage.texture = capturedPhoto;
        
        SavePhoto(capturedPhoto);
        
        editButton.SetActive(true);
        editPanel.SetActive(false);
    }

    private void SavePhoto(Texture2D texture)
    {
        byte[] imageBytes = texture.EncodeToPNG();
        string base64Str = System.Convert.ToBase64String(imageBytes);

        PlayerPrefs.SetString("Photo", base64Str);
        PlayerPrefs.Save();
    }

    private Texture2D LoadPhoto()
    {
        string base64Str = PlayerPrefs.GetString("Photo");
        if (string.IsNullOrEmpty(base64Str)) return null;

        byte[] imageBytes = System.Convert.FromBase64String(base64Str);

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);

        return texture;
    }
}
