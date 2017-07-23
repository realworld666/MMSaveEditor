using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMSaveEditor.Utils
{
    static class ExtensionMethods
    {
        public static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic,
            TKey fromKey, TKey toKey)
        {
            if (dic.ContainsKey(fromKey))
            {
                TValue value = dic[fromKey];
                dic.Remove(fromKey);
                dic[toKey] = value;
            }
        }

        public static void RenameKey<TKey, TValue>(this Map<TKey, TValue> dic,
            TKey fromKey, TKey toKey)
        {
            if (dic.ContainsKey(fromKey))
            {
                TValue value = dic.GetMap(fromKey);
                dic.Remove(fromKey);
                dic.Add(toKey, value);
            }
        }
    }
}
