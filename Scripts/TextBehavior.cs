using UnityEngine;
using System.IO;
using TMPro;
/// <summary>
/// �ĵ���д��ȡ��
/// </summary>
public class TextBehavior
{

    /// <summary>
    /// д���ļ�
    /// </summary>
    /// <param name="path">�ļ�·��</param>
    /// <param name="key">��ѯ�ؼ���</param>
    /// <param name="value">����</param>
    public void TextWriting(string path, string key, string value)
    {
        // ����ļ��Ƿ���ڣ�����������򴴽��ļ�
        if (!File.Exists(path))
        {
            Debug.Log("File no finding");
            // �������ļ�
            using (StreamWriter createFileSW = File.CreateText(path))
            {
                createFileSW.WriteLine("Hello");
            }

        }

        // ʹ��StreamWriterд��������
        using StreamWriter sw = new(path, true);
        sw.WriteLine(key + "=" + value);
    }

    public void TextClear(string path)
    {
        try
        {
            // ��������ļ������������
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.Write("");
            }
            Debug.Log("TXT�ļ�����գ�" + path);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("���TXT�ļ�ʱ���ִ���" + ex.Message);
        }
    }

    /// <summary>
    /// ��ȡ�ļ�������
    /// </summary>
    /// <param name="path">�ļ�·��</param>
    /// <param name="key">��ȡ�Ĺؼ���</param>
    /// <returns></returns>
    public string TextRead(string path, string key)
    {
        // ����ļ��Ƿ���ڣ������������s�����ļ�
        if (!File.Exists(path))
        {
            Debug.Log("File no finding");
            // �������ļ�
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
                    // �ҵ���Ӧ�����������ֵ
                    return line[(key.Length + 1)..];
                }
            }
        }

        // ����Ҳ�����Ӧ����������ؿ��ַ���������Ĭ��ֵ
        return "";
    }

    /// <summary>
    /// ɾ���ض�������
    /// </summary>
    /// <param name="path">�ļ�����·��</param>
    /// <param name="key">Ҫɾ���Ĺؼ���</param>
    public void TextDelete(string path, string key)
    {
        // �ĵ���ȫ������
        string[] allLines = File.ReadAllLines(path);

        // ʹ��StreamWriter����д�벻����ָ������������
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
    /// ���ú�����ɾ�������ļ�
    /// </summary>
    /// <param name="path">�ļ�������·��</param>
    public void DeleteFile(string path)
    {
        // ����ļ��Ƿ���ڣ����������ɾ��
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
        //���ж��Ƿ���ڣ��ٴ���
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
        //���ж��Ƿ���ڣ��ٴ���
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
                        // �ҵ���Ӧ�����������ֵ
                       
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