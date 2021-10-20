using System.Collections.Generic;

namespace DownKyi.Core.Utils
{
    public static class ListHelper
    {

        /// <summary>
        /// 判断List中是否存在，不存在则添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void AddUnique<T>(List<T> list, T item)
        {
            if (!list.Exists(t => t.Equals(item)))
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// 判断List中是否存在，不存在则添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <param name="index"></param>
        public static void InsertUnique<T>(List<T> list, T item, int index)
        {
            if (!list.Exists(t => t.Equals(item)))
            {
                list.Insert(index, item);
            }
            else
            {
                list.Remove(item);
                list.Insert(index, item);
            }
        }

    }
}
