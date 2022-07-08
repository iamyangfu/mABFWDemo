using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class AutoSetLabel
{
    [MenuItem("Tools/AutoSetLabel")]
    static void AutoSetAssetBundleLabel()
    {  
        string rootPath = Application.dataPath + "/ABRes/";
        DirectoryInfo dirTempInfo = new DirectoryInfo(rootPath);
        DirectoryInfo[] dirSceneDirInfo = dirTempInfo.GetDirectories();
        AssetDatabase.RemoveUnusedAssetBundleNames();
        foreach (DirectoryInfo dirInof in dirSceneDirInfo)
        {
            RecursionDir(dirInof, dirInof.Name);
        }

        AssetDatabase.Refresh();
    }

    private static void RecursionDir(FileSystemInfo fileSystemInfo,string moudleName)
    {
        if ( !fileSystemInfo.Exists )
        {
            return;
        }
        FileSystemInfo[] dirSceneDirInfos = ((DirectoryInfo)fileSystemInfo).GetFileSystemInfos();
        foreach (FileSystemInfo _fileSystemInfo in dirSceneDirInfos)
        {
            FileInfo file = _fileSystemInfo as FileInfo;
            if (file != null)
            {
                if (file.Name.EndsWith(".meta"))
                {
                    continue;
                }
                AutoSetAssetBundleName(file, moudleName);
            }else
            {
                RecursionDir(_fileSystemInfo,moudleName);
            }
        }
    }

    static void AutoSetAssetBundleName(FileInfo file,string moudleName )
    {
        string strABName = string.Empty;
        string strABPath = string.Empty;
        string filePath = file.FullName;
        string fileName = file.Name;

        strABPath = filePath.Replace(Application.dataPath, "Assets");
        strABName = moudleName + "/" + fileName;
        int _index = strABPath.LastIndexOf(moudleName);
        if (_index == -1)
        {
           return;
        }
        var tmpStr = strABPath.Substring(_index + moudleName.Length + 1);
        tmpStr = tmpStr.Replace("\\", "/");     //替换\为/  windows下的路径
        string [] strArr = tmpStr.Split('/');
        if (strArr.Length > 0)
        {
            strABName = moudleName + "/" + strArr[0];
        }
        if (File.Exists(strABPath))
        {
            AssetImporter assetImporter = AssetImporter.GetAtPath(strABPath); //获取AssetImporter
            assetImporter.assetBundleName = strABName;  //设置AssetBundle名称
            assetImporter.assetBundleVariant = "ab";    //设置AssetBundle的Variant名称
        }
    }

    [MenuItem("Tools/BuildAB")]
    static void BuildAssetBundle()
    {
        string outPath = Application.streamingAssetsPath;
        if ( ! Directory.Exists(outPath) )
        {
            Directory.CreateDirectory(outPath);
        }

        BuildPipeline.BuildAssetBundles(outPath,BuildAssetBundleOptions.None,BuildTarget.StandaloneOSX);

    }
}
