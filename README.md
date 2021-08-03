# MemoryEditingSoftware

This application enables user to 

## Progress : just started
[██_______] 20 %

## MemoryEditingSoftware : main application
-> C#, Prism MVVM WPF

## MemoryManipulation : DLL
-> C++
Contains the code that allows to access and edit the memory of any application on Windows

## MemoryEditingSoftware.Core : Module
-> C#, Prism MVVM WPF Module
Contains code that is used across the whole application by several modules

## MemoryEditingSoftware.Editor : Module
-> C#, Prism MVVM WPF Module
This module contains the editor to create new MES projects
For now ui based but a text edition version will be added

## MemoryEditingSoftware.ProjectSettings
-> C#, Prism MVVM WPF Module
Contains the dialogs to create a new project and to modify the settings of the actual project
Not implemented yet, only created and linked the views with the viewmodels and mainwindow

## MemoryEditingSoftware.Executer : Module (not started yet)
-> C#, Prism MVVM WPF Module
This module will be the graphical user interface between the user and the C++ DLL
It will be thread safe, which remains to be implemented
