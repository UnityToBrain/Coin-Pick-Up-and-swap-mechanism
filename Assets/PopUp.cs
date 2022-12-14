using DG.Tweening;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public Ease easeType;
    private void OnEnable()
    {
        transform.DOScale(1f, 0.5f).SetEase(easeType);
    }
}
