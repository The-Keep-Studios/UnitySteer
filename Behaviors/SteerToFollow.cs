using UnityEngine;

namespace UnitySteer.Behaviors
{
    /// <summary>
    /// Steers a vehicle to follow a transform's position at a fixed distance
    /// (which may or may not be another vehicle)
    /// </summary>
    /// <seealso cref="SteerForPursuit"/>
    [AddComponentMenu("UnitySteer/Steer/... to Follow")]
    public class SteerToFollow : Steering
    {
        /// <summary>
        /// Target transform
        /// </summary>
        [SerializeField] private Transform _target;

        /// <summary>
        /// Should the vehicle's own velocity be considered in the seek calculations?
        /// </summary>
        /// <remarks>
        /// If true, the vehicle will slow down as it approaches its target
        /// </remarks>
        [SerializeField] private bool _considerVelocity = true;

        /// <summary>
        /// How far behind we should follow the target
        /// </summary>
        [SerializeField] private Vector3 _distance;

        private Vector3 _lastTargetPosition = Vector3.zero;


        /// <summary>
        /// The target.
        /// </summary>
        public Transform Target
        {
            get { return _target; }
            set
            {
                _target = value;
                ReportedArrival = false;
            }
        }


        /// <summary>
        /// Should the vehicle's velocity be considered in the seek calculations?
        /// </summary>
        /// <remarks>
        /// If true, the vehicle will slow down as it approaches its target
        /// </remarks>
        public bool ConsiderVelocity
        {
            get { return _considerVelocity; }
            set { _considerVelocity = value; }
        }

        /// <summary>
        /// Calculates the force to apply to the vehicle to reach a point
        /// </summary>
        /// <returns>
        /// A <see cref="Vector3"/>
        /// </returns>
        protected override Vector3 CalculateForce()
        {
            Vector3 targetVelocity = Target.position - _lastTargetPosition;
            _lastTargetPosition = Target.position;
            Vector3 force = (Target == null)
                ? Vector3.zero
                : Vehicle.GetSeekVector(Target.TransformPoint(_distance) + targetVelocity, _considerVelocity);



            return force;
        }
    }
}