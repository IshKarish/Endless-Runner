using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = -2f;

    private void Update()
    {
        transform.position += Vector3.back * (scrollSpeed * Time.deltaTime);
    }
}
