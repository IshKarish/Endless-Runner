using UnityEngine;
using DG.Tweening;

public class CoinSetup : MonoBehaviour
{
    [HideInInspector] public Coin coin;
    [HideInInspector] public int worth;

    private void Start()
    {
        GetComponent<MeshRenderer>().material = coin.material;
        worth = coin.worth;
        
        transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
