using System.Collections.Generic;

namespace PressIt
{
    public static class ListUtillities
    {
        /// <summary>
        /// Gets list, sorts it then returns min value.
        /// </summary>
        public static T GetMinValue<T>(this List<T> list)
        {
            SortList(list);
            return list[0];
        }
        /// <summary>
        /// Gets list, sorts it then returns max value.
        /// </summary>
        public static T GetMaxValue<T>(this List<T> list)
        {
            SortList(list);
            return list[list.Count - 1];
        }
        private static void SortList<T>(List<T> list)
        {
            list.Sort();
        }
    }
}