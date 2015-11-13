using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;


public static class NetworkConvertor {
    public static float ToFloat(this string value) {
        return Convert.ToSingle(value);
    }

    public static int ToInt(this string value) {
        return Convert.ToInt32(value);
    }

    public static int ToInt(this byte[] byteArray) {
        var floatArray = new int[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
        return floatArray[0];

    }

    public static byte[] ToByte(this int Int) {
        var floatArray = new int[] { Int };
        // create a byte array and copy the floats into it...
        var byteArray = new byte[4];
        Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
        return byteArray;
    }

    public static NetworkGameObject GetGameObject(this byte[] data) {
        NetworkGameObject gameObject = new NetworkGameObject();

        gameObject.ID = data.Take(4).ToArray().ToInt();
        gameObject.position = data.Skip(4).Take(12).ToArray().ToVector3();
        gameObject.rotation = data.Skip(16).Take(16).ToArray().ToQuaternion();
        // gameObject.gameObject.name = data.Skip(32).ToArray().ByteToString();
        return gameObject;
    }
    public static NetworkGameObject2D GetGameObject2D(this byte[] data) {
        NetworkGameObject2D gameObject = new NetworkGameObject2D();
        gameObject.ID = data.Take(4).ToArray().ToInt();
        gameObject.position = data.Skip(4).Take(8).ToArray().ToVector2();
        gameObject.rotation = data.Skip(12).Take(8).ToArray().ToQuaternion2D();
        return gameObject;
    }


    public static NetworkGameObject NewGameObject(this byte[] data) {
        NetworkGameObject gameObject = new NetworkGameObject();
        string name = BToString(data.Skip(32).ToArray());
        gameObject.gameObject = (GameObject)Resources.Load(name);
        gameObject.ID = data.Take(4).ToArray().ToInt();
        gameObject.position = data.Skip(4).Take(12).ToArray().ToVector3();
        gameObject.rotation = data.Skip(16).Take(16).ToArray().ToQuaternion();
        gameObject.gameObject.name = name;
        return gameObject;
    }

    public static NetworkGameObject2D NewGameObject2D(this byte[] data) {
        NetworkGameObject2D gameObject = new NetworkGameObject2D();

        string name = BToString(data.Skip(20).ToArray());
        gameObject.gameObject = (GameObject)Resources.Load(name);
        gameObject.ID = data.Take(4).ToArray().ToInt();
        gameObject.position = data.Skip(4).Take(8).ToArray().ToVector2();
        gameObject.rotation = data.Skip(12).Take(8).ToArray().ToQuaternion2D();
        gameObject.gameObject.name = name;
        return gameObject;
    }

    public static byte[] ToByte(this NetworkGameObject Obj) {
        byte[] result = new byte[0];
        result = result.AddByte(Obj.ID.ToByte());
        result = result.AddByte(Obj.gameObject.transform.position.ToByte());
        result = result.AddByte(Obj.gameObject.transform.rotation.ToByte());
        // result = result.AddByte(Obj.gameObject.name.StringToByte());
        return result;
    }
    public static byte[] ToByte(this NetworkGameObject Obj, int thisID) {
        byte[] result = new byte[0];
        result = result.AddByte(thisID.ToByte());
        result = result.AddByte(Obj.position.ToByte());
        result = result.AddByte(Obj.rotation.ToByte());
        result = result.AddByte(Obj.gameObject.name.ToByte());
        return result;
    }

    public static byte[] ToByte(this NetworkGameObject2D Obj) {
        byte[] result = new byte[0];
        result = result.AddByte(Obj.ID.ToByte());
        result = result.AddByte(((Vector2)Obj.gameObject.transform.position).ToByte());
        result = result.AddByte(new Vector2(Obj.gameObject.transform.rotation.y, Obj.gameObject.transform.rotation.z).ToByte());
        // result = result.AddByte(Obj.gameObject.name.StringToByte());
        return result;
    }
    public static byte[] ToByte(this NetworkGameObject2D Obj, int thisID) {
        byte[] result = new byte[0];
        result = result.AddByte(thisID.ToByte());
        result = result.AddByte(Obj.position.ToByte());
        result = result.AddByte(Obj.rotation.ToByte2D());
        result = result.AddByte(Obj.gameObject.name.ToByte());
        return result;
    }

    public static byte[] ToByte(this Vector3 vector3) {
        var floatArray = new float[] { vector3.x, vector3.y, vector3.z };
        // create a byte array and copy the floats into it...
        var byteArray = new byte[12];
        Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
        return byteArray;
    }
    public static byte[] ToByte(this Vector2 vector2) {
        var floatArray = new float[] { vector2.x, vector2.y };
        var byteArray = new byte[8];
        Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
        return byteArray;
    }

    public static Vector3 ToVector3(this byte[] byteArray) {
        var floatArray = new float[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
        return new Vector3(floatArray[0], floatArray[1], floatArray[2]);

    }
    public static Vector2 ToVector2(this byte[] byteArray) {
        var floatArray = new float[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
        return new Vector2(floatArray[0], floatArray[1]);

    }

    public static byte[] ToByte(this Quaternion quaternion) {
        var floatArray = new float[] { quaternion.x, quaternion.y, quaternion.z, quaternion.w };
        // create a byte array and copy the floats into it...
        var byteArray = new byte[16];
        Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
        return byteArray;
    }
    public static byte[] ToByte2D(this Quaternion quaternion) {
        var floatArray = new float[] { quaternion.y, quaternion.z };
        // create a byte array and copy the floats into it...
        var byteArray = new byte[8];
        Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
        return byteArray;
    }
    public static Quaternion ToQuaternion(this byte[] byteArray) {
        var floatArray = new float[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
        return new Quaternion(floatArray[0], floatArray[1], floatArray[2], floatArray[3]);

    }

    public static Quaternion ToQuaternion2D(this byte[] byteArray) {
        var floatArray = new float[byteArray.Length / 4];
        Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
        return new Quaternion(0, floatArray[0], floatArray[1], 1);

    }





    public static string BToString(this byte[] data) {
        return Encoding.Default.GetString(data);
    }

    public static byte[] ToByte(this string data) {
        return Encoding.Default.GetBytes(data.ToArray());
    }

    public static byte[] AddByte(this byte[] source, byte[] Item) {
        int SourceSize = source.Length;
        Array.Resize(ref source, SourceSize + Item.Length);
        Array.Copy(Item, 0, source, SourceSize, Item.Length);
        return source;

    }
}
