
using System;
using System.Collections.Generic;

public class DatabaseEntry
{
    private Dictionary<string, object> mData;

    public DatabaseEntry(Dictionary<string, object> inData)
    {
        this.mData = inData;
    }

    public void AddEntry(string inKey, object inData)
    {
        if (this.mData == null)
            return;
        if (!this.mData.ContainsKey(inKey))
            this.mData.Add(inKey, inData);
        else
            this.mData[inKey] = inData;
    }

    public object GetValue(string inKey)
    {
        object obj;
        if (this.mData.TryGetValue(inKey, out obj))
            return obj;
        throw new System.Exception(string.Format("DatabaseEntry.GetValue: Couldn't find value with key {0}. Returning null.", (object)inKey));
        return (object)null;
    }

    public string GetStringValue(string inKey)
    {
        object obj;
        if (!this.mData.TryGetValue(inKey, out obj))
        {
            throw new System.Exception(string.Format("DatabaseEntry.GetStringValue: Couldn't find value with key {0}. Returning dummy data.", (object)inKey));
            return string.Empty;
        }
        if (obj.GetType() == typeof(int) || obj.GetType() == typeof(float))
            return obj.ToString();
        return (string)obj;
    }

    public int GetIntValue(string inKey)
    {
        object obj;
        if (!this.mData.TryGetValue(inKey, out obj))
        {
            throw new System.Exception(string.Format("DatabaseEntry.GetIntValue: Couldn't find value with key {0}. Returning dummy data.", (object)inKey));
            return 0;
        }
        if (obj.GetType() == typeof(float))
            return Convert.ToInt32(obj);
        return (int)obj;
    }

    public bool GetBoolValue(string inKey)
    {
        object obj;
        if (!this.mData.TryGetValue(inKey, out obj))
        {
            Console.WriteLine("DatabaseEntry.GetBoolValue: Couldn't find value with key {0}. Defaulting to false.", (object)inKey);
            return false;
        }
        bool result = false;
        if (!bool.TryParse(obj as string, out result))
            Console.WriteLine("DatabaseEntry.GetBoolValue: Couldn't parse value with key {0} and value {1}. Defaulting to false.", new object[2]
            {
        (object) inKey,
        obj
            });
        return result;
    }

    public float GetFloatValue(string inKey)
    {
        object obj;
        if (!this.mData.TryGetValue(inKey, out obj))
        {
            throw new System.Exception(string.Format("DatabaseEntry.GetFloatValue: Couldn't find value with key {0}. Returning dummy data.", (object)inKey));
            return 0.0f;
        }
        if (obj.GetType() == typeof(int))
            return Convert.ToSingle(obj);
        return (float)obj;
    }

    public bool HasKey(string inKey)
    {
        return this.mData.ContainsKey(inKey);
    }
}
