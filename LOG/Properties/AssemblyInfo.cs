using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("LOG")]
#if     (SQL2008)
[assembly: AssemblyDescription("for Integration Services 2008")]
#elif   (SQL2012)
[assembly: AssemblyDescription("for Integration Services 2012")]
#elif   (SQL2014)
[assembly: AssemblyDescription("for Integration Services 2014")]
#else
[assembly: AssemblyDescription("for Integration Services 2008")]
#endif
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("LOG")]
[assembly: AssemblyCopyright("Copyright © initions AG 2011-2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
#if     (SQL2008)
[assembly: Guid("c96e81b1-cf88-4cad-9035-770bde358948")]
#elif   (SQL2012)
[assembly: Guid("5D578F11-F299-4288-8643-559CDB97DC39")]
#elif   (SQL2014)
[assembly: Guid("35E25135-9FD2-43D3-9874-018911CE6323")]
#else
[assembly: Guid("c96e81b1-cf88-4cad-9035-770bde358948")]
#endif

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.3.0")]
