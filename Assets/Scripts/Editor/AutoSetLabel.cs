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
        RecursionDir(dirTempInfo,"ABRes");
        // FileSystemInfo [] dirSceneDirInfo = dirTempInfo.GetFileSystemInfos();

        // string[] dirs = Directory.GetDirectories(rootPath);
        // AssetDatabase.RemoveUnusedAssetBundleNames();
        // foreach (FileSystemInfo dirInof in dirSceneDirInfo)
        // {
        //     FileInfo file = dirInof as FileInfo;
        //     if (file != null)
        //     {
        //         Debug.Log("file name:" + file.Name);
        //         // file.Name    
        //     }
        //     else
        //     {
        //         Debug.Log("dir name:" + dirInof.Name);
        //     }
        // }

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
                Debug.Log("file name:"+file.Name);
            }else
            {
                Debug.Log("dir name:" + _fileSystemInfo.Name);
                RecursionDir(_fileSystemInfo,moudleName);
            }
        }
    }

    static void AutoSetAssetBundleName(){

    }
}
