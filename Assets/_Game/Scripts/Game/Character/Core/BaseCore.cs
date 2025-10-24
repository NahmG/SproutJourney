using Core;
using UnityEngine;

public abstract class BaseCore : MonoBehaviour
{
    public virtual void Initialize(CoreSystem core) { }
    public virtual void UpdateData() { }

    public virtual void FixedUpdateData() { }

}
