
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System;
using UnityEditor;
using UnityEngine;

public class Ex_CreateFileNearSelectionMenuEditor : MonoBehaviour
{

    [MenuItem("Ex/Coucou %#c")]
    private static void Coucou()
    {
        UnityEngine.Debug.Log("Coucou");
    }


    public static string GetDateTimeId()
    {
        DateTime d = DateTime.Now;
        return $"{d.Year}_{d.Month}_{d.Day}_{d.Hour}_{d.Minute}_{d.Second}";
    }
    [MenuItem("Assets/Ex/Create/File/.txt")]
    private static void CreateFile_txt()
    {
        CreateFileWithExtension("txt", "coucou");
    }
    [MenuItem("Assets/Ex/Create/File/.json")]
    private static void CreateFile_json()
    {
        CreateFileWithExtension("json", "{}");
    }
    [MenuItem("Assets/Ex/Create/File/.md")]
    private static void CreateFile_md()
    {
        CreateFileWithExtension("md", "# Coucou");
    }
    [MenuItem("Assets/Ex/Create/File/.bat")]
    private static void CreateFile_bat()
    {
        CreateFileWithExtension("bat", "timeout 10");
    }
    [MenuItem("Assets/Ex/Cmd/Open")]
    private static void Cmd_Open()
    {
        string pathDirectory = GetSelectedDirectoryPath();
        QuickGit.OpenCmd(pathDirectory);
    }
    [MenuItem("Assets/Ex/Cmd/ipconfig")]
    private static void Cmd_IpConfig()
    {
        string pathDirectory = GetSelectedDirectoryPath();
        OpenCmd(pathDirectory, "ipconfig");
    }
    [MenuItem("Assets/Ex/Cmd/adb devices")]
    private static void Cmd_adbdevices()
    {
        string pathDirectory = GetSelectedDirectoryPath();
        OpenCmd(pathDirectory, "adb devices");
    }
    [MenuItem("Assets/Ex/Cmd/adb Wifi Connect")]
    private static void Cmd_adbconnectionwifi()
    {

        AdbConnection.Connect();

    }

    private static void CreateFileWithExtension(string extension, string defaulText)
    {
        string pathDirectory = GetSelectedDirectoryPath();
        string fileName = Path.Combine(pathDirectory, $"{GetDateTimeId()}.{extension.Trim('.')}");
        File.WriteAllText(fileName, defaulText);
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
    }

    public static string GetSelectedDirectoryPath()
    {
        string p = GetSelectedFilePathMethod();
        if (File.Exists(p))
        {
            return Path.GetDirectoryName(p);
        }
        if (Directory.Exists(p))
        {
            return p;
        }
        return "";
    }
    private static string GetSelectedFilePathMethod()
    {
        UnityEngine.Object[] selectedObjects = Selection.objects;
        foreach (UnityEngine.Object obj in selectedObjects)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);

            if (!string.IsNullOrEmpty(assetPath))
            {
                string absolutePath = Path.GetFullPath(assetPath);
                return absolutePath;
            }

        }
        return "";
    }

    public static void OpenCmd(string path, string command)
    {

        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/k {command}", // "/k" tells cmd to execute the command and keep the window open
            WorkingDirectory = path,
            UseShellExecute = true, // Use the OS shell to start the process
            CreateNoWindow = false // Create a new window
        };

        try
        {
            Process.Start(processStartInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred:");
            Console.WriteLine(ex.Message);
        }

    }


    public class AdbConnection
    {

        public static void Connect()
        {
            try
            {
                // Execute adb devices command
                ExecuteCommand("adb devices");

                // Execute adb tcpip 5555 command
                ExecuteCommand("adb tcpip 5555");

                // Execute adb shell ip -f inet addr show wlan0 command and capture output
                string output = ExecuteCommand("adb shell ip -f inet addr show wlan0");

                // Parse and display the IPv4 address
                string ipv4Address = ExtractIPv4Address(output);
                if (ipv4Address != null)
                {
                    UnityEngine.Debug.Log($"IPv4 Address: {ipv4Address}");
                    ExecuteCommand($"adb connect {ipv4Address}:5555");
                }
                else
                {
                    UnityEngine.Debug.Log("IPv4 Address not found.");
                }

            }
            catch (Exception ex)
            {
                UnityEngine.Debug.Log($"Error: {ex.Message}");
            }
        }

        static string ExecuteCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                if (process == null) throw new InvalidOperationException("Failed to start process.");

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new InvalidOperationException($"Command failed with exit code {process.ExitCode}: {error}");
                }

                return output;
            }
        }

        static string ExtractIPv4Address(string output)
        {
            // Regular expression to match IPv4 addresses
            Regex ipv4Regex = new Regex(@"inet\s+(\d+\.\d+\.\d+\.\d+)", RegexOptions.Compiled);
            Match match = ipv4Regex.Match(output);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return null;
        }
    }
}