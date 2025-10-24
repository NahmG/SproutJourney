using System.Collections.Generic;
using UnityEngine;

namespace Core.Sensor
{
    using System.Linq;
    using Navigation;
    public class SensorCore : BaseCore
    {
        [SerializeField]
        List<BaseSensor> sensors;

        // ----------- PARAM ---------------
        [field: SerializeField]
        public NavigationCore Navigation { get; protected set; }

        // ----------- DATA --------------
        public bool IsGrounded { get; internal set; }
        public bool IsGoUpBridge { get; internal set; }
        public Character Target { get; internal set; }
        public Vector3 TargetDir { get; internal set; }

        public override void Initialize(CoreSystem core)
        {
            base.Initialize(core);

            foreach (var sensor in sensors)
            {
                sensor.Initialize(this, Navigation);
            }
        }

        public override void UpdateData()
        {
            foreach (var sensor in sensors)
            {
                sensor.UpdateData();
            }
        }
    }
}
