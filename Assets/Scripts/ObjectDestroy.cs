using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.z > transform.position.z)
        {
            Invoke(nameof(Destroy), 2);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
