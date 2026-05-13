using Unity.Entities;
using Unity.Transforms;
using System.Runtime.CompilerServices;

namespace XO.PopUtils
{
    public static partial class Pop
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetParent(this ref EntityCommandBuffer ecb, EntityManager em, Entity entity, Entity parent)
        {
            if (em.HasComponent<Parent>(entity))
            {
                ecb.SetComponent(entity, new Parent { Value = parent });
            }
            else
            {
                ecb.AddComponent(entity, new Parent { Value = parent });
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetParent(this EntityManager em, Entity entity, Entity parent)
        {
            if (em.HasComponent<Parent>(entity))
            {
                em.SetComponentData(entity, new Parent { Value = parent });
            }
            else
            {
                em.AddComponentData(entity, new Parent { Value = parent });
            }
        }
    }
}