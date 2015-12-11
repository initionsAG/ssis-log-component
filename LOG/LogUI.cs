using System;
using Microsoft.SqlServer.Dts.Runtime;
using System.Windows.Forms;
using Microsoft.SqlServer.Dts.Runtime.Design;

namespace Isag.Log
{
    /// <summary>
    /// Implements SSIS task UI interface
    /// <see cref="Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI"/>
    /// </summary>
    public class LogUI : IDtsTaskUI
    {
        private TaskHost _taskHost = null;
    
        public ContainerControl GetView()
        {
            return new frmLogUI(_taskHost);
        }

        public void Initialize(TaskHost taskHost, IServiceProvider serviceProvider)
        {
            _taskHost = taskHost;
        }

        /// <summary>
        /// Functionality hook, when ssis component is removed / deleted from dataflow
        /// <see cref="Microsoft.SqlServer.Dts.Runtime.Design.IDtsTaskUI.Delete"/>
        /// </summary>
        /// <param name="parentWindow"></param>
        public void Delete(IWin32Window parentWindow)
        {

        }

        /// <summary>
        /// Functionality hook, wehen ssis component is instantiated
        /// </summary>
        /// <param name="parentWindow"></param>
        public void New(IWin32Window parentWindow)
        {
            _taskHost.Properties["MessageText"].SetValue(_taskHost, "");
            _taskHost.Properties["ComponentType"].SetValue(_taskHost, ComponentType.GetDefaultComponentId());
        }
    }
}