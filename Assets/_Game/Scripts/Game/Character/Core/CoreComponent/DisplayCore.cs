using UnityEngine;

namespace Core.Display
{
    public class DisplayCore : BaseCore
    {
        [field: SerializeField]
        public float AtkDuration { get; private set; }
        [field: SerializeField]
        public float DeadDuration { get; private set; }
        [SerializeField]
        Animator anim;
        [SerializeField]
        Transform skinTf;
        [SerializeField]
        Transform sensorTf;

        float _scale;
        public float Scale => _scale;

        public Color Color { get; private set; }

        public void SetSkinScale(float value)
        {
            _scale = value;
            skinTf.localScale = Vector3.one * _scale;
        }

        public void SetSkinRotation(Vector3 vector, bool isLocal)
        {
            Quaternion qua = Quaternion.Euler(vector);
            SetSkinRotation(qua, isLocal);
        }

        public void SetSkinRotation(Quaternion quaternion, bool isLocal)
        {
            if (isLocal)
            {
                skinTf.localRotation = quaternion;
                sensorTf.localRotation = quaternion;
            }
            else
            {
                skinTf.rotation = quaternion;
                sensorTf.localRotation = quaternion;
            }
        }

        public void ChangeAnim(string animName)
        {
            anim.Play(animName);
        }
    }
}
