using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// 处理数据类型转换，数制转换、编码转换相关的类
/// </summary>
public sealed class ConvertHelper
{

    /// <summary>
    /// 由结构体转换为byte数组
    /// </summary>
    public static byte[] StructureToByte(object obj)
    {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream memory = new MemoryStream();
        bf.Serialize(memory, obj);
        byte[] bytes = memory.GetBuffer();
        memory.Close();
        return bytes;
        //int size = Marshal.SizeOf(typeof(T));
        //byte[] buffer = new byte[size];
        //IntPtr bufferIntPtr = Marshal.AllocHGlobal(size);
        //try
        //{
        //    Marshal.StructureToPtr(structure, bufferIntPtr, true);
        //    Marshal.Copy(bufferIntPtr, buffer, 0, size);
        //}
        //finally
        //{
        //    Marshal.FreeHGlobal(bufferIntPtr);
        //}
        //return buffer;

    }
    /// <summary>
    /// 由byte数组转换为结构体
    /// </summary>
    public static object ByteToStructure(byte[] bytes)
    {

        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream memory = new MemoryStream(bytes);
        object ss = bf.Deserialize(memory);
        memory.Close();
        return ss;

        //object structure = null;
        //int size = Marshal.SizeOf(typeof(T));
        //IntPtr allocIntPtr = Marshal.AllocHGlobal(size);
        //try
        //{
        //    Marshal.Copy(dataBuffer, 0, allocIntPtr, size);
        //    structure = Marshal.PtrToStructure(allocIntPtr, typeof(T));
        //}
        //finally
        //{
        //    Marshal.FreeHGlobal(allocIntPtr);
        //}
        //return (T)structure;
    }
}