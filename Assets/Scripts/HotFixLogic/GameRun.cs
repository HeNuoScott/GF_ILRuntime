using UnityEngine;
using Sirius;
using System.IO;
using System;

public class GameRun
{

    public static void Init()
    {
        Debug.Log("HotFixLogic Init");

        HotFixEntry.LoadPreFabs();
    }

    public static void Run()
    {
        Debug.Log("HotFixLogic Run");

        HotFixEntry.InitCustomComponents();
    }

    /// <summary>
    /// Ϊaot assembly����ԭʼmetadata�� ��������aot�����ȸ��¶��С�
    /// һ�����غ����AOT���ͺ�����Ӧnativeʵ�ֲ����ڣ����Զ��滻Ϊ����ģʽִ��
    /// </summary>
    //public static unsafe void LoadMetadataForAOTAssembly()
    //{
    //    // ���Լ�������aot assembly�Ķ�Ӧ��dll����������õ�mscorlib.dll����
    //    //
    //    // ���ش��ʱ unity��buildĿ¼�����ɵ� �ü����� mscorlib��ע�⣬����Ϊԭʼmscorlib
    //    //
    //    string mscorelib = @$"{Application.dataPath}/../build-win64-2020.3.33/huatuo/Managed/mscorlib.dll";
    //    byte[] dllBytes = File.ReadAllBytes(mscorelib);

    //    fixed (byte* ptr = dllBytes)
    //    {
    //        // ����assembly��Ӧ��dll�����Զ�Ϊ��hook��һ��aot���ͺ�����native���������ڣ��ý������汾����
    //        int err = Huatuo.HuatuoApi.LoadMetadataForAOTAssembly((IntPtr)ptr, dllBytes.Length);
    //        Debug.Log("LoadMetadataForAOTAssembly. ret:" + err);
    //    }
    //}
}
