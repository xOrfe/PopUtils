using Unity.Collections;
using Unity.Mathematics;

namespace XO.PopUtils
{
    public static partial class Pop
    {
        
        public struct ForceResult
        {
            public float3 LinearForce;
            public float3 Torque;
            
            public static ForceResult operator +(ForceResult a, ForceResult b)
            {
                return new ForceResult
                {
                    LinearForce = a.LinearForce + b.LinearForce,
                    Torque      = a.Torque + b.Torque
                };
            }
            
            public static ForceResult operator -(ForceResult a, ForceResult b)
            {
                return new ForceResult
                {
                    LinearForce = a.LinearForce - b.LinearForce,
                    Torque      = a.Torque - b.Torque
                };
            }
        }

        public static ForceResult SolveForces(
            NativeList<Ray> forces,
            float3 centerOfMass)
        {
            float3 totalForce = float3.zero;
            float3 totalTorque = float3.zero;

            for (int i = 0; i < forces.Length; i++)
            {
                Ray ray = forces[i];

                float3 force = ray.Direction;
                float3 applicationPoint = ray.Origin;

                float3 r = applicationPoint - centerOfMass;

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