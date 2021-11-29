using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;



namespace KoroBuilder
{
    public class Terminal
    {
        private void Start()
        {
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            ExecuteProcessTerminal("git status --porcelain");
            //ExecuteProcessTerminal("ioreg -l | awk '/IOPlatformSerialNumber/ { print $4;}'");
#endif
        }

        public static string ExecuteProcessTerminal(string argument)
        {
            try
            {
                UnityEngine.Debug.Log("= Start Executing [" + argument + "] =");
                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = "/bin/bash",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false,
                    Arguments = " -c \"" + argument + " \""
                };
                Process myProcess = new Process
                {
                    StartInfo = startInfo
                };
                myProcess.Start();

                string output = myProcess.StandardOutput.ReadToEnd();
                UnityEngine.Debug.Log(output);
                myProcess.WaitForExit();
                UnityEngine.Debug.Log("End command");

                return output;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
                return null;
            }
        }
    }
}