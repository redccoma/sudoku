using System;
using System.Collections.Generic;

/// <summary>
/// 글로벌 이벤트 처리
/// </summary>
/// <typeparam name="K"></typeparam>
/// <typeparam name="V"></typeparam>
public static class BroadcastTunnel<K, V>
{
    static Dictionary<K, List<Action<V>>> map;

    /// <summary>
    /// 글로벌 이벤트 송신
    /// </summary>
    /// <param name="key"></param>
    /// <param name="arg"></param>
    public static void Notify(K key, V arg)
    {
        if (null != BroadcastTunnel<K, V>.map)
        {
            List<Action<V>> list;
            if (BroadcastTunnel<K, V>.map.TryGetValue(key, out list))
            {
                for (int n = 0, cnt = list.Count; n < cnt; ++n)
                    list[n](arg);
            }
        }
    }

    /// <summary>
    /// 글로벌 이벤트 등록
    /// </summary>
    /// <param name="key"></param>
    /// <param name="receiver"></param>
    public static void Add(K key, Action<V> receiver)
    {
        if (null == BroadcastTunnel<K, V>.map)
            BroadcastTunnel<K, V>.map = new Dictionary<K, List<Action<V>>>();

        List<Action<V>> list;
        if (!BroadcastTunnel<K, V>.map.TryGetValue(key, out list))
            BroadcastTunnel<K, V>.map.Add(key, list = new List<Action<V>>(16));

        list.Add(receiver);
    }

    public static void Adds(K key, Action<V>[] receivers)
    {
        if (null == BroadcastTunnel<K, V>.map)
            BroadcastTunnel<K, V>.map = new Dictionary<K, List<Action<V>>>();

        List<Action<V>> list;
        if (!BroadcastTunnel<K, V>.map.TryGetValue(key, out list))
            BroadcastTunnel<K, V>.map.Add(key, list = new List<Action<V>>(receivers.Length));

        for (int n = 0, cnt = receivers.Length; n < cnt; ++n)
            list.Add(receivers[n]);
    }


    public static bool Remove(K key, Action<V> value)
    {
        if (null == BroadcastTunnel<K, V>.map)
            return false;

        List<Action<V>> list;
        if (!BroadcastTunnel<K, V>.map.TryGetValue(key, out list))
            return false;

        bool ret = list.Remove(value);
        if (0 == list.Count)
        {
            BroadcastTunnel<K, V>.map.Remove(key);
            if (0 == BroadcastTunnel<K, V>.map.Count)
                BroadcastTunnel<K, V>.map = null;
        }

        return ret;
    }

    public static bool RemoveAllByKey(K key)
    {
        if (null == BroadcastTunnel<K, V>.map)
            return false;

        bool ret = BroadcastTunnel<K, V>.map.Remove(key);
        if (0 == BroadcastTunnel<K, V>.map.Count)
            BroadcastTunnel<K, V>.map = null;

        return ret;
    }

    public static void RemoveAll()
    {
        if (null == BroadcastTunnel<K, V>.map)
            return;

        BroadcastTunnel<K, V>.map.Clear();
        BroadcastTunnel<K, V>.map = null;
    }
}