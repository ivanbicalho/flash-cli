# Flash CLI

---

Flash is a command line interface to help you creating files and folders using your custom template files, in order to avoid repetitive tasks.

## Creating your templates

In this example we'll use txt and c# files, but you can define any kind of files.

To create a new template, you just have to add a new folder inside de folder **flash-templates**, which have to located together with flash executable.

Inside your folder, just put your template files and a **config.json**.

The folder structure is like this:

```
flash-templates
    use-case
        config.json
        Readme.txt
        UseCaseName.cs
        UseCaseNameInput.cs
```

## The config.json file

The config.json file contains all of the template configuration, the name of the files you want to create, the location of them and your variables, which are optionals.

The config.json structure example:

```json
{
    "creations": [
        {
            "templateFile": "Readme.txt"
        },
        {
            "location" : "Features/UseCaseName",
            "templateFile": "UseCaseName.cs"
        },
        {
            "location" : "Features/UseCaseName",
            "templateFile": "UseCaseNameInput.cs"
        },
        {
            "location": "EmptyFolderToCreate"
        }
    ],
    "variables": [
        {
            "replace": "UseCaseName",
            "question": "Enter the use case name:"
        },
        {
            "replace": "Repository",
            "question": "Enter the repository name:"
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

Whenever the occurrence "UseCaseName" is found, it'll be replaced by "MyFirstUseCase", regardless of whether it's the folder name, file name, part of the file name, file content, etc.

In the terminal's current directory, we'll have:

```
Readme.txt
EmptyFolderToCreate
Features
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