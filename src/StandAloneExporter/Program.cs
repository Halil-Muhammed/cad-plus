﻿//*********************************************************************
//CAD+ Toolset
//Copyright(C) 2020 Xarial Pty Limited
//Product URL: https://cadplus.xarial.com
//License: https://cadplus.xarial.com/license/
//*********************************************************************

using System;
using System.Diagnostics;
using System.Linq;
using Xarial.CadPlus.Xport.EDrawingsHost;
using Xarial.CadPlus.Xport.SwEDrawingsHost;

namespace Xarial.CadPlus.Xport.StandAloneExporter
{
    public class Program
    {
        public const string LOG_MESSAGE_TAG = "XARIAL:::XPORT:::";

        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                var srcFile = args[0];
                var outFile = args[1];
                var versNmb = args[2];

                var vers = EDrawingsVersion_e.Default;

                foreach (EDrawingsVersion_e curVer in Enum.GetValues(typeof(EDrawingsVersion_e))) 
                {
                    if (string.Equals(curVer.ToString(), $"v{versNmb}")) 
                    {
                        vers = curVer;
                        break;
                    }
                }

                WriteLine($"eDrawings version: {vers}");

                using (var publisher = new EDrawingsPublisher(vers))
                {
                    WriteLine($"Opening '{srcFile}'...");
                    publisher.OpenDocument(srcFile).GetAwaiter().GetResult();

                    WriteLine($"Saving '{srcFile}' to '{outFile}'...");
                    publisher.SaveDocument(outFile).GetAwaiter().GetResult();

                    WriteLine($"Closing '{srcFile}'...");
                    publisher.CloseDocument().GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                //TODO: extract message exception only
                WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }

        private static void WriteLine(string line)
        {
            Console.WriteLine(LOG_MESSAGE_TAG + line);
        }
    }
}