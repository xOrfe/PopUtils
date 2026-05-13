using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace XO.PopUtils
{
    [BurstCompile]
    public static partial class Pop
    {
        public struct ForceResult
        {
            public float3 LinearForce;
            public float3 Torque;

            public ForceResult(float3 linearForce, float3 torque)
            {
                LinearForce = linearForce;
                Torque = torque;
            }

            public static ForceResult operator +(ForceResult a, ForceResult b)
            {
                return new ForceResult
                {
                    LinearForce = a.LinearForce + b.LinearForce,
                    Torque = a.Torque + b.Torque
                };
            }

            public static ForceResult operator -(ForceResult a, ForceResult b)
            {
                return new ForceResult
                {
                    LinearForce = a.LinearForce - b.LinearForce,
                    Torque = a.Torque - b.Torque
                };
            }
        }

        [BurstCompile]
        public static ForceResult SolveForces(
            in NativeArray<Force> forces,
            float3 centerOfMass)
        {
            var totalForce = float3.zero;
            var totalTorque = float3.zero;

            for (int i = 0; i < forces.Length; i++)
            {
                var current = forces[i];

                var force = current.Direction;
                var applicationPoint = current.Origin;

                var r = applicationPoint - centerOfMass;

                totalForce += force;
                totalTorque += math.cross(r, force);
            }

            return new ForceResult
            {
                LinearForce = totalForce,
                Torque = totalTorque
            };
        }
    }
}