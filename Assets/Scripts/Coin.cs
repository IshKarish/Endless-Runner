using UnityEngine;

[CreateAssetMenu(fileName = "New Coin", menuName = "Coin")]
public class Coin : ScriptableObject
{
    public Material material;
    public int worth;
}
