###Components of .Net Framework

.NET Framework consists of the common language runtime (CLR) and the .NET Framework class library.

![](https://dotnet.microsoft.com/static/images/illustrations/swimlane-architecture-framework.svg?v=ZuTW7j9pS1oiuMqx3E-Xvb9OEM_8ajDEcHbecyjRtLA)

[More Details](https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet-framework#architecture)

##### Common Language Runtime (CLR)

.Net Framework provides runtime environment called Common Language Runtime (CLR). It provides an environment to run all the .Net Programs. The code which runs under the CLR is called as Managed Code. Programmers need not to worry on managing the memory if the programs are running under the CLR as it provides memory management and thread management.

Programmatically, when our program needs memory, CLR allocates the memory for scope and de-allocates the memory if the scope is completed.

Language Compilers (e.g. C#, VB.Net, J#) will convert the Code/Program to Microsoft Intermediate Language (MSIL) intern this will be converted to Native Code by CLR. 

![](https://docs.microsoft.com/en-us/dotnet/csharp/getting-started/media/introduction-to-the-csharp-language-and-the-net-framework/net-architecture-relationships.png)

##### .Net Framework Class Library (FCL)
The .NET Framework class library is a collection of reusable types that tightly integrate with the common language runtime. The class library is object oriented, providing types from which your own managed code derives functionality. This not only makes the .NET Framework types easy to use but also reduces the time associated with learning new features of the .NET Framework. In addition, third-party components integrate seamlessly with classes in the .NET Framework.

The following are different types of applications that can make use of .net class library: 
1. Windows Application.
2. Console Application
3. Web Application.
4. XML Web Services.
5. Windows Services.

In short, developers just need to import the BCL(Base Class Library) in their language code and use its predefined methods and properties to implement common and complex functions like reading and writing to file, graphic rendering, database interaction, and XML document manipulation.

##### Other Components
1. [Common Type System (CTS)](https://docs.microsoft.com/en-us/dotnet/standard/base-types/common-type-system)
The common type system defines how types are declared, used, and managed in the common language runtime, and is also an important part of the runtime`s support for cross-language integration.
![](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/media/index/value-reference-types-common-type-system.png)

2. Common Language Specification (CLS)
It is a sub set of CTS and it specifies a set of rules that needs to be adhered or satisfied by all language compilers targeting CLR. It helps in cross language inheritance and cross language debugging.
[More details](https://docs.microsoft.com/en-us/dotnet/standard/language-independence-and-language-independent-components)