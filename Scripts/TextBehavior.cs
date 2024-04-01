using UnityEngine;
using System.IO;
using TMPro;
/// <summary>
/// 文档书写读取类
/// </summary>
public class TextBehavior
{

    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="key">查询关键字</param>
    /// <param name="value">内容</param>
    public void TextWriting(string path, string key, string value)
    {
        // 检查文件是否存在，如果不存在则创建文件
        if (!File.Exists(path))
        {
            Debug.Log("File no finding");
            // 创建空文件
            using (StreamWriter createFileSW = File.CreateText(path))
            {
                createFileSW.WriteLine("Hello");
            }

        }

        // 使用StreamWriter写入配置项
        using StreamWriter sw = new(path, true);
        sw.WriteLine(key + "=" + value);
    }

    public void TextClear(string path)
    {
        try
        {
            // 创建或打开文件，并清空内容
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.Write("");
            }
            Debug.Log("TXT文件已清空：" + path);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("清空TXT文件时出现错误：" + ex.Message);
        }
    }

    /// <summary>
    /// 读取文件的内容
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="key">读取的关键字</param>
    /// <returns></returns>
    public string TextRead(string path, string key)
    {
        // 检查文件是否存在，如果不存在则s创建文件
        if (!File.Exists(path))
        {
            Debug.Log("File no finding");
            // 创建空文件
            using (StreamWriter createFileSW = File.CreateText(path))
            {
                createFileSW.WriteLine("Hello");
            }
        }

        using (StreamReader sr = new(path))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith(key + "="))
                {
                   // Debug.Log(line[(key.Length + 1)..]);

                    if (line[(key.Length + 1)..] == "")
                        return null;
                    // 找到对应的配置项并返回值
                    return line[(key.Length + 1)..];
                }
            }
        }

        // 如果找不到对应的配置项，返回空字符串或其他默认值
        return "";
    }

    /// <summary>
    /// 删除特定的内容
    /// </summary>
    /// <param name="path">文件完整路径</param>
    /// <param name="key">要删除的关键字</param>
    public void TextDelete(string path, string key)
    {
        // 文档的全部内容
        string[] allLines = File.ReadAllLines(path);

        // 使用StreamWriter重新写入不包含指定键的配置项
        using StreamWriter sw = new(path, false);
        foreach (string line in allLines)
        {
            if (!line.StartsWith(key + "="))
            {
                sw.WriteLine(line);
            }
        }
    }

    /// <summary>
    /// 备用函数，删除整个文件
    /// </summary>
    /// <param name="path">文件的完整路径</param>
    public void DeleteFile(string path)
    {
        // 检查文件是否存在，如果存在则删除
        if (File.Exists(path))
        {
            File.Delete(path);
           // Debug.Log("File deleted: " + path);
        }
        else
        {
            Debug.Log("File not found: " + path);
        }
    }


    // newContent
    public bool CreateFile(string path, string fileName)
    {
        //先判断是否存在，再创建
        if (!File.Exists(path + $"/{fileName}.txt"))
        {
            FileStream fileStream = new FileStream(path + $"/{fileName}.txt", FileMode.OpenOrCreate);
            fileStream.Close();
            return true;
        }
        return false;

    }

    public bool CheckFileExit(string path, string fileName)
    {
        // Debug.Log(path + $"/{fileName}.txt");
        //先判断是否存在，再创建
        if (File.Exists(path + $"/{fileName}.txt"))
        {
            Debug.Log("Exit");
            return true;
        }
        return false;

    }

    public bool CheckDict(string path, string fileName, string key, string content)
    {
        
        if (File.Exists(path + $"/{fileName}.txt"))
        {
            Debug.Log("DictFileExit");
            using (StreamReader sr = new(path + $"/{fileName}.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Debug.Log(key + content);
                    if (line.StartsWith(key + "="))
                    {
                        Debug.Log(line[(key.Length + 1)..]);

                        if (line[(key.Length + 1)..] == content)
                            return true;
                        // 找到对应的配置项并返回值
                       
                    }
                   
                }
                return false;
            }
        }
        return false;

    }
    public void WriteDict(string path, string fileName, string key, string value)
    {
        Debug.Log("writeDict");
        if (File.Exists(path + $"/{fileName}.txt"))
        {
            // Debug.Log("writeDict_t");
            using (StreamWriter sw = new(path + $"/{fileName}.txt", true))
            {
                sw.WriteLine(key + "=" + value);
            }
        }
        
        
    }

    public void WriteFile(string path, string fileName, string tip)
    {
       // Debug.Log("writeFile"+File.Exists(path + $"/{fileName}.txt"));
        //Debug.Log(path + $"/{fileName}.txt");
        if (File.Exists(path + $"/{fileName}.txt"))
        {
            using (StreamWriter sw = new(path + $"/{fileName}.txt", true))
            {
                sw.WriteLine(tip);
            }
          //  Debug.Log("writeFile_T");
        }
    }
    public void ReadFile(string path, string fileName)
    {
        if (File.Exists(path + $"/{fileName}.txt"))
        {
          //  Debug.Log("readFile");
            string []t =File.ReadAllLines(path + $"/{fileName}.txt");
            foreach (var i in t)
            {
                Debug.Log(i);
            }
        }
    }

}