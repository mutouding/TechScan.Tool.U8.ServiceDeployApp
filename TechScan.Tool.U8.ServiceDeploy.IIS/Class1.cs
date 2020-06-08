using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.IIS
{
    public class Class112
    {
        public static void ExecDeploy(Action<DeployLogType ,string> act)   //Action<string> act)
        {
            //act($"我是{nameof(Class112)}");

            act(DeployLogType.Debug, "我是Debug");
            act(DeployLogType.Info, "我是Info");
            act(DeployLogType.Error, "我是Error");
        }
    }
}
