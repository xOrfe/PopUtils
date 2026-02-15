using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.Transforms;

namespace XO.PopUtils
{
    public static partial class Pop
    {
        /// <summary>
        /// Creates a new LocalTransform with default values.
        /// 
        /// Method: LocalTransform initialization
        /// Input: None
        /// Output: LocalTransform with Position = zero, Rotation = identity, Scale = 1
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LocalTransform NewLocalTransform()
        {
            return new LocalTransform
            {
                Position = float3.zero,
                Rotation = quaternion.identity,
                Scale = 1
            };
        }

        /// <summary>
        /// Extracts rotation quaternion from a 4x4 transformation matrix.
        /// Assumes no scale or shear in the matrix.
        /// 
        /// Method: Rotation extraction from matrix
        /// Input: float4x4 m - transformation matrix
        /// Output: quaternion - extracted rotation
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static quaternion ExtractRotation(float4x4 m)
        {
            return new quaternion((float3x3)m);
        }

        // ==========================================
        // LOCAL SPACE (pos/rot) TO WORLD SPACE
        // ==========================================

        /// <summary>
        /// Transforms local position and rotation to world space.
        /// Combines parent transformation with local position/rotation.
        /// 
        /// Method: Local to World transformation (pos + rot)
        /// Input: float4x4 parentLtw - parent transform matrix
        ///        float3 localPos - position in local space
        ///        quaternion localRot - rotation in local space
        /// Output: float3 worldPos - position in world space
        ///         quaternion worldRot - rotation in world space
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToWorldSpace(
            float4x4 parentLtw,
            float3 localPos,
            quaternion localRot,
            out float3 worldPos,
            out quaternion worldRot)
        {
            worldPos = math.transform(parentLtw, localPos);

            var parentRot = ExtractRotation(parentLtw);
            worldRot = math.mul(parentRot, localRot);
        }

        /// <summary>
        /// Transforms local position to world space.
        /// 
        /// Method: Local position to World position transformation
        /// Input: float4x4 parentLtw - parent transform matrix
        ///        float3 localPos - position in local space
        /// Output: float3 - position in world space
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ToWorldSpace(float4x4 parentLtw, float3 localPos)
        {
            return math.transform(parentLtw, localPos);
        }

        /// <summary>
        /// Transforms local rotation to world space.
        /// 
        /// Method: Local rotation to World rotation transformation
        /// Input: float4x4 parentLtw - parent transform matrix
        ///        quaternion localRot - rotation in local space
        /// Output: quaternion - rotation in world space
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion ToWorldSpace(float4x4 parentLtw, quaternion localRot)
        {
            var parentRot = ExtractRotation(parentLtw);
            return math.mul(parentRot, localRot);
        }

        /// <summary>
        /// Generates world space transformation matrix from parent transform and local position/rotation.
        /// Assumes local scale is 1.
        /// 
        /// Method: Local pos/rot to World transformation matrix
        /// Input: float4x4 parentLtw - parent transform matrix
        ///        float3 localPos - position in local space
        ///        quaternion localRot - rotation in local space
        /// Output: float4x4 - world space transformation matrix
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 ToWorldSpace(float4x4 parentLtw, float3 localPos, quaternion localRot)
        {
            var localM = float4x4.TRS(localPos, localRot, new float3(1f));
            return math.mul(parentLtw, localM);
        }

        /// <summary>
        /// Transforms local space transformation matrix to world space.
        /// 
        /// Method: Local matrix to World matrix transformation
        /// Input: float4x4 parentLtw - parent transform matrix
        ///        float4x4 localLtw - local transform matrix
        /// Output: float4x4 - world space transformation matrix
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 ToWorldSpace(float4x4 parentLtw, float4x4 localLtw)
        {
            return math.mul(parentLtw, localLtw);
        }

        // ==========================================
        // WORLD SPACE (pos/rot) TO LOCAL SPACE
        // ==========================================

        /// <summary>
        /// Transforms world position and rotation to local space.
        /// Removes parent transformation to get local coordinates.
        /// 
        /// Method: World to Local transformation (pos + rot)
        /// Input: float4x4 parentLtw - parent transform matrix
        ///        float3 worldPos - position in world space
        ///        quaternion worldRot - rotation in world space
        /// Output: float3 localPos - position in local space
        ///         quaternion localRot - rotation in local space
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLocalSpace(
            float4x4 parentLtw,
            float3 worldPos,
            quaternion worldRot,
            out float3 localPos,
            out quaternion localRot)
        {
            var invParent = math.inverse(parentLtw);
            localPos = math.transform(invParent, worldPos);

            var parentRot = ExtractRotation(parentLtw);
            var invParentRot = math.inverse(parentRot);
            localRot = math.mul(invParentRot, worldRot);
        }

        /// <summary>
        /// Transforms world position to local space.
        /// 
        /// Method: World position to Local position transformation
        /// Input: float4x4 parentLtw - parent transform matrix
        ///        float3 worldPos - position in world space
        /// Output: float3 - position in local space
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ToLocalSpace(float4x4 parentLtw, float3 worldPos)
        {
            var invParent = math.inverse(parentLtw);
            return math.transform(invParent, worldPos);
        }

        /// <summary>
        /// Transforms world rotation to local space.
        /// 
        /// Method: World rotation to Local rotation transformation
        /// Input: float4x4 parentLtw - parent transform matrix
        ///        quaternion worldRot - rotation in world space
        /// Output: quaternion - rotation in local space
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion ToLocalSpace(float4x4 parentLtw, quaternion worldRot)
        {
            var invParentRot = math.inverse(ExtractRotation(parentLtw));
            return math.mul(invParentRot, worldRot);
        }

        /// <summary>
        /// Extracts local position and rotation from world space transformation matrix.
        /// Assumes no scale or shear in resulting matrix.
        /// 
        /// Method: World matrix to Local pos/rot extraction
        /// Input: float4x4 parentLtw - parent transform matrix
        ///        float4x4 worldM - world space transformation matrix
        /// Output: float3 localPos - position in local space
        ///         quaternion localRot - rotation in local space
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToLocalSpace(
            float4x4 parentLtw,
            float4x4 worldM,
            out float3 localPos,
            out quaternion localRot)
        {
            var localM = math.mul(math.inverse(parentLtw), worldM);

            localPos = localM.c3.xyz;
            localRot = new quaternion((float3x3)localM);
        }

        /// <summary>
        /// Transforms world space LocalToWorld component to local space relative to parent.
        /// 
        /// Method: World LocalToWorld to Local matrix transformation
        /// Input: LocalToWorld parentLtw - parent LocalToWorld component
        ///        LocalToWorld worldLtw - world LocalToWorld component
        /// Output: float4x4 - local space transformation matrix
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 ToLocalSpace(LocalToWorld parentLtw, LocalToWorld worldLtw)
        {
            return ToLocalSpace(parentLtw.Value, worldLtw.Value);
        }

        /// <summary>
        /// Transforms world space transformation matrix to local space relative to parent.
        /// 
        /// Method: World matrix to Local matrix transformation
        /// Input: float4x4 parentLtw - parent transform matrix
        ///        float4x4 worldLtw - world transform matrix
        /// Output: float4x4 - local space transformation matrix
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4x4 ToLocalSpace(float4x4 parentLtw, float4x4 worldLtw)
        {
            return math.mul(math.inverse(parentLtw), worldLtw);
        }
    }
}