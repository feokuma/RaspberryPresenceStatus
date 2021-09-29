# Raspberry Presence Status

A led matrix display for online presence status

## Technologies

- Aspnet Core 5
- NUnit

## Libraries and tools

- [.Net Core IoT Libraries](https://github.com/dotnet/iot)

## References

- [WS2812 Driver Sample](https://github.com/dotnet/iot/tree/main/src/devices/Ws28xx)

## Debugging on Raspberry Pi 3

### VSCode tasks.json

The following tasks enable to build, publish and deploy the code to RaspberryPi remotly. It's necessary to use a ssh key to enable passwordless. There are some instructions to do [here](https://www.raspberrypi.org/documentation/computers/remote-access.html)

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
      "command": "rsync",
      "type": "shell",
      "args": [
        "-cavzu",
        "${workspaceFolder}/src/bin/Debug/net5.0/linux-arm/*",
        "pi@192.168.12.90:~/<FolderOfDllsOnRPi>"
      ],
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
      "pipeTransport": {
        "pipeCwd": "${workspaceRoot}",
        "pipeProgram": "ssh",
        "pipeArgs": ["pi@10.10.10.10"],
        "debuggerPath": "~/.vsdbg/vsdbg"
      }
    }
  ]
}
```
