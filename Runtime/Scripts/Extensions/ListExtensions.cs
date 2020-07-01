using System.Collections.Generic;

namespace Unity.FilmInternalUtilities {
    internal static class ListExtensions {

        
        internal static void RemoveNullMembers<T>(this IList<T> list) {
            for (int i = list.Count-1; i >= 0 ; --i) {
                if (null != list[i])
                    continue;
                
                list.RemoveAt(i);
            }
        }

    }

}

