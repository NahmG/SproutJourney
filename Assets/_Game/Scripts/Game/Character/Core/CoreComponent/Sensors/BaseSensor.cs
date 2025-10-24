using UnityEngine;

namespace Core.Sensor
{
    using Navigation;
    public abstract class BaseSensor : MonoBehaviour
    {
        [SerializeField]
        protected LayerMask layer;

        protected SensorCore Sensor;
        protected NavigationCore Navigation;

        public virtual void Initialize(SensorCore sensor, NavigationCore navigation)
        {
            Sensor = sensor;
            Navigation = navigation;
        }

        public virtual void UpdateData() { }
    }
}
