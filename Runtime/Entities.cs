using Unity.Entities;
using Unity.Transforms;

namespace XO.PopUtils
{
    public static partial class Pop
    {
        public static void SetParent(this Entity entity, EntityManager em, EntityCommandBuffer ecb, Entity parent)
        {
            if (em.HasComponent<Parent>(entity))
            {
                ecb.SetComponent(entity, new Parent
                {
                    Value = parent
                });
            }
            else
            {
                ecb.AddComponent(entity, new Parent
                {
                    Value = parent
                });
            }

            if (!em.HasComponent<LocalTransform>(entity))
            {
                ecb.AddComponent(entity, LocalTransform.Identity);
            }
        }
    }
}