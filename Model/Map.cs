using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class Map<TKey, TValue>
{
    public List<TKey> mKeys = new List<TKey>();
    public List<TValue> mValues = new List<TValue>();
    public List<TKey> keys
    {
        get
        {
            return this.mKeys;
        }
        set
        {
            this.mKeys = value;
        }
    }

    public List<TValue> values
    {
        get
        {
            return this.mValues;
        }
        set
        {
            this.mValues = value;
        }
    }

    public void Awake()
    {
    }

    public void OnEnable()
    {
    }

    public Dictionary<TKey, TValue> GetDictionary()
    {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
        for (int index = 0; index < this.mKeys.Count; ++index)
            dictionary.Add(this.mKeys[index], this.mValues[index]);
        return dictionary;
    }

    public void Add(TKey inKey, TValue inData)
    {
        if (!this.ContainsKey(inKey))
        {
            this.mKeys.Add(inKey);
            this.mValues.Add(inData);
        }
        else
            this.SetMap(inKey, inData);
    }

    public void Remove(TKey inKey)
    {
        this.mValues.RemoveAt(this.mKeys.IndexOf(inKey));
        this.mKeys.Remove(inKey);
    }

    public bool ContainsKey(TKey inKey)
    {
        int count = this.mKeys.Count;
        for (int index = 0; index < count; ++index)
        {
            if (inKey.Equals((object)this.mKeys[index]))
                return true;
        }
        return false;
    }

    public bool ContainsValue(TValue inData)
    {
        int count = this.mValues.Count;
        for (int index = 0; index < count; ++index)
        {
            if (inData.Equals((object)this.mValues[index]))
                return true;
        }
        return false;
    }

    public void Clear()
    {
        this.mKeys.Clear();
        this.mValues.Clear();
    }

    public void SetMap(TKey inKey, TValue inData)
    {
        int index1 = 0;
        for (int index2 = 0; index2 < this.mKeys.Count; ++index2)
        {
            if (this.mKeys[index2].Equals((object)inKey))
                index1 = index2;
        }
        this.mValues[index1] = inData;
    }

    public TValue GetMap(TKey inKey)
    {
        int index1 = -1;
        for (int index2 = 0; index2 < this.mKeys.Count; ++index2)
        {
            if (this.mKeys[index2].Equals((object)inKey))
                index1 = index2;
        }
        if (index1 != -1)
            return this.mValues[index1];
        //Debug.LogFormat("Could not find key:{0} in map", (object)inKey);
        return default(TValue);
    }

    public Dictionary<TKey, TValue> ConvertToDictionary()
    {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
        try
        {
            for (int index = 0; index < this.mKeys.Count; ++index)
                dictionary.Add(this.mKeys[index], this.mValues[index]);
        }
        catch (Exception ex)
        {
            //Debug.LogError((object)"KeysList.Count is not equal to ValuesList.Count. It shouldn't happen!", (UnityEngine.Object)null);
        }
        return dictionary;
    }
}
