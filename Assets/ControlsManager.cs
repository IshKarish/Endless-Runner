using TMPro;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private string controls;
    private TextMeshProUGUI controlsText;
    
    private void Awake()
    {
        controlsText = GetComponentInChildren<TextMeshProUGUI>();
        
        controls = PlayerPrefs.GetString("Controls", "JOYSTICK");
        controlsText.text = controls;
    }

    public void ChangeControls()
    {
        if (controls == "JOYSTICK") controls = "BUTTONS";
        else if (controls == "BUTTONS") controls = "JOYSTICK";

        controlsText.text = controls;

        PlayerPrefs.SetString("Controls", controls);
        PlayerPrefs.Save();
    }
}
