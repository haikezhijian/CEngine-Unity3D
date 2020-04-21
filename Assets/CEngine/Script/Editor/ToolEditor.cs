﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using CEngine;
using System.Text;
using System.Linq;

public class ToolEditor
{
    public static string[] _suffixs = new string[] { ".prefab", ".png" };
    public static string kSpriteExtension = ".png";

    public static bool IsSuffixAssetBundle(string suf)
    {
        foreach (var s in _suffixs)
        {
            if (s.Equals(suf))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsSpriteExtension(string suf)
    {
        return suf == kSpriteExtension;
    }

    [MenuItem("Assets/CreateAssetBundle")]
    public static void CreateAssetBundle()
    {
        foreach (var guid in Selection.assetGUIDs)
        {
            var p = AssetDatabase.GUIDToAssetPath(guid);

            if (AssetBundlePath.kAssetBundle == Path.GetFileName(p))
            {
                Debug.LogError("directory name error " + AssetBundlePath.kAssetBundle);
                return;
            }
            if (!Directory.Exists(p))
            {
                Debug.LogError("not directory " + p);
                return;
            }
            var isOk = false;
            var l = p.Split('/');
            StringBuilder sb = new StringBuilder();
            for (int i = l.Length - 1; i >= 0; --i)
            {
                if (l[i] == AssetBundlePath.kAssetBundle)
                {
                    isOk = true;
                    break;
                }
                sb.Insert(0, l[i] + (i != l.Length - 1 ? "/" : ""));
            }
            if (!isOk)
            {
                Debug.LogError(string.Format("directory {0} not Assetbundle child directory", p));
                return;
            }
            var directory = Directory.CreateDirectory(Path.GetFullPath(p));
            if (0 != directory.GetDirectories().Length)
            {
                Debug.LogError(string.Format("direcory {0} is not leaf node", p));
            }
            var files = directory.GetFiles();
            foreach (var file in files)
            {
                if (IsSpriteExtension(Path.GetExtension(file.Name)))
                {
                    var ti = AssetImporter.GetAtPath(p + '/' + file.Name) as TextureImporter;
                    if (ti.textureType == TextureImporterType.Sprite)
                    {
                        ti.spritePackingTag = sb.ToString() + ".packTag";
                    }
                    ti.assetBundleName = sb.ToString();
                }
                else if (IsSuffixAssetBundle(Path.GetExtension(file.Name)))
                {
                    var ti = AssetImporter.GetAtPath(p + '/' + file.Name);
                    ti.assetBundleName = sb.ToString();
                }
            }

        }
    }
}