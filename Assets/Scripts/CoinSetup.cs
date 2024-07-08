using UnityEngine;

public class CoinSetup : MonoBehaviour
{
    [HideInInspector] public Coin coin;
    [HideInInspector] public int worth;

    private void Start()
    {
        GetComponent<MeshRenderer>().material = coin.material;
        worth = coin.worth;
    }
}
