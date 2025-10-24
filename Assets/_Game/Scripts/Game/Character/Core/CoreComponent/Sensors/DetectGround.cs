using UnityEngine;

namespace Core.Sensor
{
    using Navigation;
    public class DetectGroundSensor : BaseSensor
    {
        [SerializeField]
        Transform groundCheck;
        [SerializeField]
        float groundCheckRadius;

        Collider[] colliders;

        public override void Initialize(SensorCore sensor, NavigationCore navigation)
        {
            base.Initialize(sensor, navigation);
            colliders = new Collider[1];
        }

        public override void UpdateData()
        {
            base.UpdateData();

            Sensor.IsGrounded = Physics.OverlapSphereNonAlloc(groundCheck.position, groundCheckRadius, colliders, layer) > 0;
        }
    }
}

