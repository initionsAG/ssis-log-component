using System;
using Microsoft.SqlServer.Dts.Runtime;
using System.ComponentModel;

namespace Isag.Log
{
    /// <summary>
    /// Component to Log Custom SSIS Events based on a given Text or SSIS Variable
    /// The Component can fire different Types of Events. The Message for the Events can either be set manually or orginiate from an SSIS Variable (or both).
    /// </summary>
//Define DTS Task Attribute for different SQL Server Versions - Compiler will use statement according to build profile (e.g. for SQL Server 2008 etc)
#if     (SQL2008) 
    [DtsTask(DisplayName  = "LOG",
            IconResource = "log.ico",
            UITypeName   = "Isag.Log.LogUI, Isag.Log, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1a52a4025059ca5a",
            TaskType     = "PackageMaintenance",
            TaskContact  = "initions AG",
            RequiredProductLevel = DTSProductLevel.None
    )]
#elif   (SQL2012)
    [DtsTask(DisplayName = "LOG",
            IconResource = "log.ico",
            UITypeName   = "Isag.Log.LogUI, Isag.Log2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=68970804c7c661ea",
            TaskType     = "PackageMaintenance",
            TaskContact  = "initions AG",
            RequiredProductLevel = DTSProductLevel.None
    )]
#elif   (SQL2014)
    [DtsTask(
        DisplayName          = "LOG",
        IconResource         = "log.ico",
        UITypeName           = "Isag.Log.LogUI, Isag.Log3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6a4e053840a8eff7",
        TaskType             = "PackageMaintenance",
        TaskContact          = "initions AG",
        RequiredProductLevel = DTSProductLevel.None
    )]
#elif   (SQL2016)
    [DtsTask(
        DisplayName          = "LOG",
        IconResource         = "log.ico",
        UITypeName           = "Isag.Log.LogUI, initions.Henry.SSIS.Log2016, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6a4e053840a8eff7",
        TaskType             = "PackageMaintenance",
        TaskContact          = "initions AG",
        RequiredProductLevel = DTSProductLevel.None
    )]
#else
    [DtsTask(DisplayName  = "LOG",
                IconResource = "log.ico",
                UITypeName   = "Isag.Log.LogUI, Isag.Log, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1a52a4025059ca5a",
                TaskType     = "PackageMaintenance",
                TaskContact  = "initions AG",
                RequiredProductLevel = DTSProductLevel.None
        )]
#endif
    public class LOG : Task
    {
        /// <summary>
        /// Custom SSIS property for the ComponentType
        /// </summary>
        [Category("Task Settings"), DisplayName("Component Type"), Description("Component Type")]
        public int ComponentType { get; set; }

        /// <summary>
        /// Custom SSIS property with the message text that will be logged
        /// </summary>
        [Category("Task Settings"), DisplayName("Message Text"), Description("This Message will be written to log, eventually variable placeholders will be replaced first")]
        public string MessageText { get; set; }

        /// <summary>
        /// Overrides Execute logic
        /// Core Logic: It Fires a Custom Event, with the given ComponentType and Message
        /// <see cref="Microsoft.SqlServer.Dts.Runtime.Task.Execute"/>
        /// </summary>
        public override DTSExecResult Execute(Connections connections, VariableDispenser variableDispenser, IDTSComponentEvents componentEvents, IDTSLogging log, object transaction)
        {
            bool refFire = false;
            componentEvents.FireInformation(ComponentType, "", GetMessageText(variableDispenser, componentEvents, MessageText), "", 0, ref refFire);

            return base.Execute(connections, variableDispenser, componentEvents, log, transaction);
        }

        /// <summary>
        /// Overrides default validate logic, method will be called each time ssis tries to validate the package
        /// <see cref="Microsoft.SqlServer.Dts.Runtime.Task.Validate"/>
        /// </summary>
        /// <param name="connections"></param>
        /// <param name="variableDispenser"></param>
        /// <param name="componentEvents"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public override DTSExecResult Validate(Connections connections, VariableDispenser variableDispenser, IDTSComponentEvents componentEvents, IDTSLogging log)
        {
            if (string.IsNullOrEmpty(MessageText))
            {
                componentEvents.FireWarning(0, "LOG", "The message is empty!", "", 0);
            }
            else
            {
                try
                {
                    GetMessageText(variableDispenser, componentEvents, MessageText);
                }
                catch (Exception ex)
                {
                    componentEvents.FireError(1, "LOG", "the chosen variable cannot be read: " + ex.Message, "", 0);
                    return DTSExecResult.Failure;
                }
            }
           
            return base.Validate(connections, variableDispenser, componentEvents, log);
        }


        /// <summary>
        /// Reads the Value of an SSIS Variable by a given variablename and a VariableDispenser
        /// </summary>
        /// <param name="variableDispenser">the variableDispenser of a running SSIS instance</param>
        /// <param name="variableName">the name of the SSIS variable</param>
        /// <returns>the value of the variable as a string, returns an empty string if variable is not found</returns>
        public static string GetValueFromVariable(VariableDispenser variableDispenser, string variableName)
        {
            if (!variableDispenser.Contains(variableName)) return string.Empty;
            
            Variables vars = null;
            variableDispenser.LockOneForRead(variableName, ref vars);
            string result = vars[variableName].Value.ToString();
            vars.Unlock();

            return result;
        }

        /// <summary>
        /// Resolves the variable placeholders in message text and returnes the resolved message
        /// Variable placeholders will be lookedup and the value will be inserted in the placeholder
        /// </summary>
        /// <param name="variableDispenser">the ssis variable dispenser to access runtime variables</param>
        /// <param name="componentEvents"></param>
        /// <param name="messageText">the message text possibly containing variable placeholder</param>
        /// <returns>returns the resolved message where all variable placeholders are replaced by their correspoding values</returns>
        public string GetMessageText(VariableDispenser variableDispenser, IDTSComponentEvents componentEvents, string messageText)
        {
            try
            {
                if (messageText != "")
                {
                    while (messageText.Contains("@("))
                    {

                        int start = messageText.IndexOf("@(", 0);
                        int end = messageText.IndexOf(")", start);

                        string varName = messageText.Substring(start + 2, end - start - 2);

                        messageText = messageText.Replace("@(" + varName + ")", GetValueFromVariable(variableDispenser, varName));
                    }
                }
            }
            catch (Exception ex)
            {
                componentEvents.FireError(0, "", "Fehler beim Anfordern eines Variablen-Wertes: " + ex.Message, "", 0);
                throw ex;
            }

            return messageText;
        }
    }
}