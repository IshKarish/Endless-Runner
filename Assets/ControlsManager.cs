using TMPro;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private string _controls;
    private TextMeshProUGUI _controlsText;
    
    private void Awake()
    {
        _controlsText = GetComponentInChildren<TextMeshProUGUI>();
        
        _controls = PlayerPrefs.GetString("Controls", "JOYSTICK");
        _controlsText.text = _controls;
    }

    public void ChangeControls()
    {
        if (_controls == "JOYSTICK") _controls = "BUTTONS";
        else if (_controls == "BUTTONS") _controls = "JOYSTICK";

        _controlsText.text = _controls;

        PlayerPrefs.SetString("Controls", _controls);
        PlayerPrefs.Save();
    }
}
