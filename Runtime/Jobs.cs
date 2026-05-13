using System.Runtime.CompilerServices;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;

namespace XO.PopUtils
{
    public static partial class Pop
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetBatchSize(int taskCount, int minBatch = 16, int maxBatch = 128)
        {
            int workers = JobsUtility.JobWorkerCount;
            if (workers <= 0) return taskCount;

            int batchSize = taskCount / (workers * 4);
            return math.clamp(batchSize, minBatch, maxBatch);
        }
    }
}