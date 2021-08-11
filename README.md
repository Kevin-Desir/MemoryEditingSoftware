# MemoryEditingSoftware

This application enables user to edit the memory of any application on Windows with a friendly GUI.

C#, Prism MVVM WPF Application with a C++ DLL.

This application cannot be used to find memory addresses, it can only read or write in them. To find the addresses you will need another, specialized tool.

## Progress : [███████___] 70 % until first version

## MemoryEditingSoftware : main application


## MemoryManipulation : DLL
-> C++
Contains the code that allows to access and edit the memory of any application on Windows

## MemoryEditingSoftware.Core : Prism Module
Contains code that is used across the whole application by several modules

## MemoryEditingSoftware.Editor : Prism Module
This module contains the editor to create new MES projects

For now ui based but a text edition version will be added

## MemoryEditingSoftware.ProjectSettings
Contains the dialogs to create a new project and to modify the settings of the actual project

Not implemented yet, only created and linked the views with the viewmodels and mainwindow

## MemoryEditingSoftware.Executer : Prism Module
This module is the graphical user interface between the user and the C++ DLL

To access and edit the memory of the program. It is thread safe
