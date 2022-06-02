using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lexyprotect
{
    public class httpdebuggerblock
    {
        static string disk = Strings.Mid(System.Environment.GetFolderPath(Environment.SpecialFolder.System), 1, 3);
        static int detectedhttpdebugger = 0;
        void httpdebuggercheck()
        {
            RegistryKey winLogonKey = Registry.CurrentUser.OpenSubKey(@"Software\MadeForNet\HTTPDebuggerPro", true);
            string currentKey = winLogonKey.GetValue("AppVer").ToString();

            if (currentKey.Contains("HTTP Debugger"))
            {
                detectedhttpdebugger = 1;
            }

        }


        void driversys()
        {
          bool httpdebugger = File.Exists(disk + @"Windows\System32\drivers\HttpDebuggerSdk.sys");
            if (httpdebugger == true)
            {
                detectedhttpdebugger = 1;
            }

        }


        void httpdebuggerservice()
        {
            foreach (ServiceController s in ServiceController.GetServices())
            {
                if ((s.ServiceName.Contains("HTTPDebugger") && s.Status == ServiceControllerStatus.Running) | (s.ServiceName.Contains("HTTPDebugger") && s.Status == ServiceControllerStatus.Stopped))
                {
                    detectedhttpdebugger = 1;
                }
            }
        }



        void checkhttpdebuggertitle()
        {
            bool title = Process.GetProcesses().Any(p => p.MainWindowTitle.Contains("HTTP Debugger"));
            if (title)
            {
                detectedhttpdebugger = 1;
            }
        

        }


        void proxyloop()
        {
          
                while (true)
                {
                    Thread.Sleep(1000);
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    HttpWebRequest.DefaultWebProxy = new WebProxy();
                
            }

        }



        void checkhttpdebuggerproc()
        {
            bool proc = Process.GetProcesses().Any(p => p.ProcessName.Contains("HTTPDebuggerSvc"));
            if (proc)
            {
                detectedhttpdebugger = 1;
            }
        }


        



    }
}
