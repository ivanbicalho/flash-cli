# Flash

---

Flash is command line interface to help you creating files and folders using your custom templates, in order to avoid repetitive tasks.

## Creating your templates

In this example we'll use txt and c# files, but you can define any kind of files.

The flash CLI reads all folders inside the folder 'flash-templates', in which have to exists in the same directory as flash CLI.

Each folder became a new template. Inside the folder, you have to have a **config.json** and your files.

The folder structure is like this:

```
flash-templates
    use-case
        config.json
        Readme.txt
        UseCaseName.cs
```

The config.json structure example:

```json
{
    "creations": [
        {
            "file": "Readme.txt"
        },
        {
            "folder" : "Features/UseCaseName",
            "file": "UseCaseName.cs"
        },
        {
            "folder" : "Features/UseCaseName",
            "file": "UseCaseNameInput.cs"
        }
    ],
    "variables": [
        {
            "replace": "UseCaseName",
            "question": "Digite o nome do caso de uso:"
        },
        {
            "replace": "Repository",
            "question": "Digite o nome do repositório:"
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
Digite o nome do caso de uso: MyFirstUseCase
Digite o nome do repositório: MyRepo
```

What will happen next?

Everywhere the occurrence "UseCaseName" were found, it'll be replaced by "MyFirstUseCase", regardless if it's the folder name, file name, part file name, file content, etc.

At the current directory in the terminal, the structure will be:

```
Readme.txt
Features
    MyFirstUseCase
        MyFirstUseCase.cs
        MyFirstUseCaseInput.cs
```

Let's suppose that inside of the file Readme.txt the initial value were:

```
Hello world! Your UseCaseName is good as also your repo Repository.
```

After the execution, it'll be:

```
Hello world! Your MyFirstUseCase is good as also your repo MyRepo.
```