using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;


public static class NetworkConvertor
{
    public static float ToFloat(this string value)
    {
        return Convert.ToSingle(value);
    }

    public static int ToInt(this string value)
    {
        return Convert.ToInt32(value);
    }

    public static int ToInt(this byte[] byteArray)
    {
        var floatArray = new int[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
        return floatArray[0];

    }

    public static int ByteToInt32(this byte[] byteArray)
    {
        var floatArray = new int[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
        return floatArray[0];

    }

    public static byte[] ToByte(this int Int)
    {
        var floatArray = new int[] { Int };
        // create a byte array and copy the floats into it...
        var byteArray = new byte[4];
        Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
        return byteArray;
    }

    public static NetworkGameObject GetGameObject(this byte[] data)
    {
        NetworkGameObject gameObject = new NetworkGameObject();

        gameObject.ID = data.Take(4).ToArray().ToInt();
        gameObject.position = data.Skip(4).Take(12).ToArray().ByteToVector3();
        gameObject.rotation = data.Skip(16).Take(16).ToArray().ByteToQuaternion();
        // gameObject.gameObject.name = data.Skip(32).ToArray().ByteToString();
        return gameObject;
    }

    public static NetworkGameObject NewGameObject(this byte[] data)
    {
        NetworkGameObject gameObject = new NetworkGameObject();


        string name = data.Skip(32).ToArray().ByteToString();
        gameObject.gameObject = (GameObject)Resources.Load(name);
        gameObject.ID = data.Take(4).ToArray().ToInt();
        gameObject.position = data.Skip(4).Take(12).ToArray().ByteToVector3();
        gameObject.rotation = data.Skip(16).Take(16).ToArray().ByteToQuaternion();
        gameObject.gameObject.name = name;
        return gameObject;
    }

    public static byte[] ToByte(this NetworkGameObject Obj)
    {
        byte[] result = new byte[0];
        result = result.AddByte(Obj.ID.ToByte());
        result = result.AddByte(Obj.gameObject.transform.position.Vector3ToByte());
        result = result.AddByte(Obj.gameObject.transform.rotation.QuaternionToByte());
        // result = result.AddByte(Obj.gameObject.name.StringToByte());
        return result;
    }
    public static byte[] ToByte(this NetworkGameObject Obj, int thisID)
    {
        byte[] result = new byte[0];
        result = result.AddByte(thisID.ToByte());
        result = result.AddByte(Obj.position.Vector3ToByte());
        result = result.AddByte(Obj.rotation.QuaternionToByte());
        result = result.AddByte(Obj.gameObject.name.StringToByte());
        return result;
    }

    public static byte[] Vector3ToByte(this UnityEngine.Vector3 vector3)
    {
        var floatArray = new float[] { vector3.x, vector3.y, vector3.z };
        // create a byte array and copy the floats into it...
        var byteArray = new byte[12];
        Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
        return byteArray;
    }
    public static Vector3 ByteToVector3(this byte[] byteArray)
    {
        var floatArray = new float[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
        return new Vector3(floatArray[0], floatArray[1], floatArray[2]);

    }


    public static byte[] QuaternionToByte(this Quaternion quaternion)
    {
        var floatArray = new float[] { quaternion.x, quaternion.y, quaternion.z, quaternion.w };
        // create a byte array and copy the floats into it...
        var byteArray = new byte[16];
        Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
        return byteArray;
    }
    public static Quaternion ByteToQuaternion(this byte[] byteArray)
    {
        var floatArray = new float[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
        return new Quaternion(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);

    }




    public static string ByteToString(this byte[] data)
    {
        return Encoding.Default.GetString(data);
    }

    public static byte[] StringToByte(this string data)
    {
        return Encoding.Default.GetBytes(data.ToArray());
    }

    public static byte[] AddByte(this byte[] source, byte[] Item)
    {
        int SourceSize = source.Length;
        Array.Resize(ref source, SourceSize + Item.Length);
        Array.Copy(Item, 0, source, SourceSize, Item.Length);
        return source;

    }
}
