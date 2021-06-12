using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.danmaku2ass
{
    /// <summary>
    /// 碰撞处理
    /// </summary>
    public class Collision
    {
        private readonly int lineCount;
        private readonly List<int> leaves;

        public Collision(int lineCount)
        {
            this.lineCount = lineCount;
            leaves = Leaves();
        }

        private List<int> Leaves()
        {
            var ret = new List<int>(lineCount);
            for (int i = 0; i < lineCount; i++) ret.Add(0);
            return ret;
        }

        /// <summary>
        /// 碰撞检测
        /// 返回行号和时间偏移
        /// </summary>
        /// <param name="display"></param>
        /// <returns></returns>
        public Tuple<int, float> Detect(Display display)
        {
            List<float> beyonds = new List<float>();
            for (int i = 0; i < leaves.Count; i++)
            {
                float beyond = display.Danmaku.Start - leaves[i];
                // 某一行有足够空间，直接返回行号和 0 偏移
                if (beyond >= 0)
                {
                    return Tuple.Create(i, 0f);
                }
                beyonds.Add(beyond);
            }

            // 所有行都没有空间了，那么找出哪一行能在最短时间内让出空间
            float soon = beyonds.Max();
            int lineIndex = beyonds.IndexOf(soon);
            float offset = -soon;
            return Tuple.Create(lineIndex, offset);
        }

        public void Update(float leave, int lineIndex, float offset)
        {
            leaves[lineIndex] = Utils.IntCeiling(leave + offset);
        }

    }
}
