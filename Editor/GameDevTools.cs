using System.IO;
using System.Text;
using Python.Runtime;
using UnityEditor.Scripting.Python;
using UnityEngine;

namespace NickoJ.GameDevTools
{
    public static class GameDevTools
    {
        public const string RootMenu = "GameDev Tools";
        public const string AssetsRootMenu = "Assets/" + RootMenu;

        private static readonly StringBuilder StringBuilder = new();
        
        public static void ExecuteScript(string folder, string script, string command, params dynamic[] args)
        {
            DirectoryInfo directory = new DirectoryInfo(Application.dataPath).Parent?.Parent;

            if (directory is null) throw new DirectoryNotFoundException();

            string workDir = Path.Combine(directory.FullName, "gamedev-tools", "Tools");
            string scriptDir = Path.Combine(workDir, folder);
            string filePath = Path.Combine(scriptDir, script);
            string moduleName = script.Replace(".py", "");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Script {filePath}");
            }

            Debug.Log($"Executing script {filePath}");

            PythonRunner.EnsureInitialized();
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                
                if ((int)sys.path.count(workDir) == 0)
                {
                    sys.path.append(workDir);
                }

                if ((int) sys.path.count(scriptDir) == 0)
                {
                    sys.path.append(scriptDir);
                }

                try
                {
                    StringBuilder.Clear();
                    foreach (dynamic arg in args)
                    {
                        if (StringBuilder.Length != 0)
                        {
                            StringBuilder.Append(", ");
                        }

                        if (arg is string)
                        {
                            StringBuilder.Append('"').Append(arg).Append('"');
                        }
                        else
                        {
                            StringBuilder.Append(arg);
                        }
                    }

                    var scriptStr = @$"
import {moduleName}

{moduleName}.{command}({StringBuilder})";

                    PythonRunner.RunString(scriptStr);
                }
                finally
                {
                    sys.path.remove(workDir);
                    sys.path.remove(scriptDir);
                }
            }
        }
    }
}