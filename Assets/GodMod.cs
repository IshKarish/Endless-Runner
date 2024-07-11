using TMPro;
using UnityEngine;

public class GodMod : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    public static bool inGodMod;
    
    private void Update()
    {
        if (Input.touchCount == 3)
        {
            Vector2 touch1 = Input.GetTouch(0).position;
            Vector2 touch2 = Input.GetTouch(1).position;
            Vector2 touch3 = Input.GetTouch(2).position;

            if (IsTriangle(touch1, touch2, touch3))
                ActivateGodMod();
        }
    }
    
    private bool IsTriangle(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float area = Mathf.Abs((p1.x * (p2.y - p3.y) + p2.x * (p3.y - p1.y) + p3.x * (p1.y - p2.y)) / 2.0f);
        return area > 0;
    }

    private void ActivateGodMod()
    {
        titleText.text = "GODINATOR";
        inGodMod = true;
    }
}
