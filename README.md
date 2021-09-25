# Flash CLI

---

Flash is a command line interface to help you creating files and folders using your custom template files, in order to avoid repetitive tasks.

## Creating your templates

In this example we'll use txt and c# files, but you can define any kind of files.

To create a new template, you just have to add a new folder inside the folder **flash-templates**, which has to be located together with flash executable.

Inside your folder, just put your template files and folders and a **config.json** (optional).

The folder structure is like this:

```
flash-templates
    use-case
        config.json
        Readme.txt
        EmptyFolderToCreate
        UseCaseName
            UseCaseName.cs
            UseCaseNameInput.cs
```

With the structure above, you can use the "use-case" template.

## The config.json file

The config.json file is optional and contains all of the template configuration, which are the template description and variables.

The template description will be shown to the user when they interact with the flash CLI, to help them understand what that template does.

The variables are used to replace the occurrences on the template files and folders.
 
The config.json structure example:

```json
{
    "description" : "Create a use case based on clean architecture",
    "variables" : [
        {
            "replace" : "UseCaseName",
            "question" : "Enter the use case name:"
        },
        {
            "replace" : "Repository",
            "question" : "Enter the repository name:"
        }
    ]
}
```

With the code above, you can use in terminal:

```bash
$ flash new use-case
```

As you declared two variables **UseCaseName** and **Repository**, you will be asked to enter these values:

```bash
$ flash new use-case
Enter the use case name: MyFirstUseCase
Enter the repository name: MyRepo
```

## The magic happening

What will happen next?

Whenever the occurrence "UseCaseName" is found, it'll be replaced by "MyFirstUseCase", regardless of whether it's the folder name, file name, part of the file name, file content, etc. The same will occur with the "Repository" variable.

In the terminal's current directory, we'll have:

```
Readme.txt
EmptyFolderToCreate
MyFirstUseCase
    MyFirstUseCase.cs
    MyFirstUseCaseInput.cs
```

Let's suppose that inside the **Readme.txt** file the initial value was:

```
Hello world! Your UseCaseName is good as also your repo Repository.
```

After the execution, it'll be:

```
Hello world! Your MyFirstUseCase is good as also your repo MyRepo.
```

## Special Functions

There are some special variables that can be used in file templates. They are:

- **camel(VariableName)** = Puts the first character from a variable as lower case
- **pascal(VariableName)** = Puts the first character from a variable as upper case
- **lower(VariableName)** = Puts all characters from a variable as lower case
- **upper(VariableName)** = Puts all characters from a variable as upper case

With these functions, you get more flexibility on the template file, allowing doing things like this: 

```csharp
public class MyUseCase
{
    private readonly IMyUseCaseRepository _camel(MyUseCase)Repository;

    public MyUseCase(IMyUseCaseRepository camel(MyUseCase)Repository)
    {
        _camel(MyUseCase)Repository = camel(MyUseCase)Repository;
    }
}
```

Considering the value of "MyUseCase" as "SaveCustomer", the result would be:

```csharp
public class SaveCustomer
{
    private readonly ISaveCustomerRepository _saveCustomerRepository;

    public SaveCustomer(ISaveCustomerRepository saveCustomerRepository)
    {
        _saveCustomerRepository = saveCustomerRepository
    }
}
```

## How to use

Download the latest version of the flash executable from [Releases Page](https://github.com/ivanbicalho/flash-cli/releases) and place it in a folder that is in [PATH environment variable](https://en.wikipedia.org/wiki/PATH_(variable)) (in order to use flash at all locations on the terminal).

After that, create a folder named **flash-templates** in the same directory as the flash executable and configure your templates. To verify that your templates are configured correctly, use the **flash validate** command.
