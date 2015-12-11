## initions SSIS Component "LOG"
"LOG" is a custom componnent for [SQL Server Integration Services](https://en.wikipedia.org/wiki/SQL_Server_Integration_Services). It can be added to the Control Flow of an SSIS-package to write direct to the log-output or activate an Event Handler with a custom message.

### Requirements for development
Microsoft VisualStudio 2014 or greater ???

### Requirements for usage
* Microsoft SSIS 2008R2, 2012, 2014
* A Database that can be accesed by SSIS

### Quickstart/Installation
// wie clonen, kompilieren, installieren
// Installer anbieten!

### How to use
* open SSIS Package
* add "LOG" component to Control Flow and connect to any other component (i.e draw a connection from "LOG" to "Data Flow Task")
* open and configure "LOG" component:
    * choose "Component Type" (initialize, information, item begin,...)
    * add "Message Text" (like "DFT started")
    * add any "Variable" you like to the message text (i.e. ContainerStartTime)

#### Screenshots


### Bugs
// wie auf Bugs reagieren? An wen wenden?
If you find a bug, please message me on //e-mail, twitter, ...///

### Changelog
2015-11-DD
First Release! Hello world!
### License
//MIT License?