using System;
using DG.Tweening;
using UnityEngine;

public class Sprout : MonoBehaviour
{
    public Action<Sprout> _OnCollected;

    [SerializeField]
    Transform skinTf;

    [SerializeField]
    float floatHeight = 0.5f, duration = 1.5f;

    Tween floatTween;

    void OnEnable()
    {
        Animate();
    }

    void OnDestroy()
    {
        floatTween.Kill();
    }

    void Animate()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * floatHeight;

        // Loop up and down forever
        floatTween = skinTf.DOMoveY(endPos.y, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetRelative(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _OnCollected?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
