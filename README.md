# MemoryEditingSoftware

This application enables user to edit the memory of any application on Windows with a friendly GUI.

Mainly C#, Prism MVVM WPF Application with a small C++ DLL.

This application cannot be used to find memory addresses, it can only read or write in them. To find the addresses you will need another, specialized tool.

## Progress : [█████████_] 90 % until first version

Remaining: some minor improvements and issues need to be fixed but the application is working correctly. Eg: Date of creation of a project, handle error if process not found, etc...

Has not been tested enough to ensure a correct functionning.

## Modules decomposition:

### MemoryEditingSoftware : main application


### MemoryManipulation : DLL
-> The sole C++ project of the application.

Contains the code that allows to access and edit the memory of any application on Windows.

### MemoryEditingSoftware.Core : Prism Module
Contains code that is used across the whole application by several modules.

### MemoryEditingSoftware.Editor : Prism Module
This module contains the editor to choose what will be displayed in the when runned and to put the memory addresses etc...

UI based but maybe a text editor will be added if relevant.

### MemoryEditingSoftware.ProjectSettings
Contains the dialogs to create a new project and to modify the settings of the actual project.

### MemoryEditingSoftware.Run : Prism Module
This module is the graphical user interface between the user and the C++ DLL.

It generates buttons, textboxes etc... to access and edit the memory of the program. 

It is thread safe.

## footnote

This application was initially made two years ago in C++ using Qt creator but I was not reallt happy with the result.

As I was learning Prism MVVM, I found remaking it a good idea to train with a project that really uses Prism potential.

I learned a lot making it and am happy to have redone it.