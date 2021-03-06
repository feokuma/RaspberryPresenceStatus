# Raspberry Presence Status

A led matrix display for online presence status. This is very usefull indicate when you are on a meeting.

<img src="images/LedMatrixAvaliableStatus.jpg" alt="Led matrix showing avaliable status" style="width:200px;"/>

## Technologies

-   .Net 5
-   Raspberry Pi 3

## Libraries and tools

-   [.Net Core IoT Libraries](https://github.com/dotnet/iot)
-   NUnit

## References

-   [Setting up your Raspberry Pi](https://www.raspberrypi.com/documentation/computers/getting-started.html#setting-up-your-raspberry-pi)
-   [Deploy .NET apps to Raspberry Pi](https://docs.microsoft.com/en-us/dotnet/iot/deployment)
-   [Raspberry PI passwordless SSH Access](https://www.raspberrypi.com/documentation/computers/remote-access.html#passwordless-ssh-access)
-   [Remote Debugging on Linux ARM](https://github.com/OmniSharp/omnisharp-vscode/wiki/Remote-Debugging-On-Linux-Arm)
-   [WS2812 Driver Sample](https://github.com/dotnet/iot/tree/main/src/devices/Ws28xx)

## Diagram

This is the schematic to connect the boards

![Schematic](images/RaspberryPresenceStatus_bb.png)

## Debugging on Raspberry Pi 3

### VSCode tasks.json

The following tasks enable to build, publish and deploy the code to RaspberryPi remotly. It's necessary to use a ssh key to enable passwordless. There are some instructions to do [here](https://www.raspberrypi.org/documentation/computers/remote-access.html)

The task `"deploy"` require the `rsync` tool on linux or `putty` on windows

```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "build",
            "command": "dotnet",
            "type": "process",
            "args": [
                "build",
                "${workspaceFolder}/src/<ProjectName>.csproj",
                "/property:GenerateFullPaths=true",
                "/consoleloggerparameters:NoSummary"
            ],
            "problemMatcher": "$msCompile"
        },
        {
            "label": "publish",
            "command": "dotnet",
            "type": "process",
            "presentation": {
                "reveal": "always",
                "panel": "shared",
                "showReuseMessage": false,
                "echo": false,
                "clear": true
            },
            "options": {
                "cwd": "${workspaceFolder}"
            },
            "args": [
                "publish",
                "${workspaceFolder}/src/<ProjectName>.csproj",
                "-r",
                "linux-arm"
            ]
        },
        {
            "label": "deploy",
            "type": "shell",
            "windows": {
                "command": "cmd",
                "args": [
                    "/C",
                    "pscp.exe",
                    "-pw",
                    "raspberry",
                    "${workspaceFolder}/src/bin/Debug/net5.0/linux-arm/*.dll",
                    "pi@10.10.10.10:/home/pi/<FolderOfDllsOnRPi>"
                ]
            },
            "linux": {
                "command": "rsync",
                "args": [
                    "-cavzu",
                    "${workspaceFolder}/src/bin/Debug/net5.0/linux-arm/*",
                    "pi@10.10.10.10:~/<FolderOfDllsOnRPi>"
                ]
            },
            "dependsOrder": "sequence",
            "dependsOn": ["build", "publish"]
        }
    ]
}
```

### VSCode launch.json

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Remote Launch",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "deploy",
            "program": "~/.dotnet/dotnet",
            "args": ["<ProjectName>.dll"],
            "cwd": "~/<FolderOfDllsOnRPi>",
            "stopAtEntry": false,
            "console": "internalConsole",
            "env": {
                "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "linux": {
                "pipeTransport": {
                    "pipeCwd": "${workspaceRoot}",
                    "pipeProgram": "ssh",
                    "pipeArgs": ["pi@10.10.10.10"],
                    "debuggerPath": "~/.vsdbg/vsdbg"
                }
            },
            "windows": {
                "pipeTransport": {
                    "pipeCwd": "${workspaceRoot}",
                    "pipeProgram": "PLINK.EXE",
                    "pipeArgs": ["pi@10.10.10.11", "-pw", "<ssh-password>"],
                    "debuggerPath": "~/.vsdbg/vsdbg"
                }
            }
        }
    ]
}
```
