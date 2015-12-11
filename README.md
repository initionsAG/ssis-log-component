## initions SSIS Component "LOG"
"LOG" is a custom component for [SQL Server Integration Services](https://en.wikipedia.org/wiki/SQL_Server_Integration_Services). It can be added to the Control Flow of an SSIS-package to write messages to the log-output (Execution Results) or activate an Event Handler with a custom message.

### Requirements for development
* Microsoft VisualStudio 2012 or greater

### Requirements for usage
* Microsoft SSIS 2008R2 / 2012 / 2014

### How to use
* open SSIS Package
* find LOG component in SSIS Toolbox ![SSIS Toolbox](/resources/SSIS_Toolbox_LOG.PNG "SSIS Toolbox with LOG component")
* add "LOG" component to Control Flow ![Control Flow](/resources/Control_Flow_LOG.PNG "Control Flow with connected LOG component") and connect to any other component (i.e draw a connection from "LOG" to "Data Flow Task")
* open and configure "LOG" component:
    * choose "Component Type" (initialize, information, item begin,...)
    * add "Message Text" (like "DFT started")
    * add any "Variable" you like to the message text (i.e. ContainerStartTime)
    * ![LOG_02](/resources/LOG_02.PNG "Edit LOG component")
* result is written to 
    * SSIS Output ![Output](/resources/Output_LOG.PNG "Output") 
    * Exectuion Results ![Execution Results](/resources/Execution_Results.PNG "Execution Results") 

### Bugs
If you find a bug, please contact us on GitHub

### Changelog
2015-12-12
First Release

### License
[MIT License](LICENSE)