---
caption: Command Line
title: Command line for running and configuring xBatch
description: Technical documentation for configuring and running the publishing job of xBatch using command line
order: 2
image: xbatch-command-line.png
---
![Command line output of xBatch](xbatch-command-line.png)

Refer the list of available arguments below. Use -- symbol to use arguments. Use --help argument to display help i the console.

For example the below command will open all SOLIDWORKS part files from the *D:\Demo\xBatch\Models* folder and run 2 macros: Macro1.swp and Macro2.swp. SOLIDWORKS 2020 will be started silently and in the background.

~~~
> xbatch.exe -i "D:\Demo\xBatch\Models" -f "*.sldprt*" -e -m "D:\Demo\xBatch\Macros\Macro1.swp" "D:\Demo\xBatch\Macros\Macro2.swp" -s silent background -v 2020
~~~

## Run job with input parameters

Job can be created dynamically with specified input parameters and settings.

| Short Flag  | Flag  |Required   | Summary  |
|---|---|---|---|
| -i  | --input  |Yes   | List of input directories or file paths to process. These are files which can be opened by SOLIDWORKS (e.g. SOLIDWORKS files, CATIA, STEP, DXF/DWG, etc.)  |
| -m  | --macros  |Yes   | List of macros to run: VBA macros (*.swp or *.swb) and VSTA macros (*.dll) are supported  |
| -f  | --filter  | No  |  Filter to extract input files, if input parameter contains directories |
| -e  |  --error | No  | If this option is used export will continue if any of the files or formats failed to process, otherwise the export will terminate  |
| -t  |  --timeout | No  | Timeout in seconds for processing a single file (e.g. opening the file and running the macros)")] |
| -s  |  --startup | No  | Specifies the startup options (silent, background, safe) for the host application. Multiple option can be used |
| -b  |  --batch|No|Maximum number of files to process in the single session of CAD application before restarting
| -v  |  --hostversion | No  | Version of SOLIDWORKS application. Use one of the following formats: 2020, Sw2020, SOLIDWORKS 2020. If this option is not specified than the oldest version installed on this system will be used. |
| -o  |  --open | No  | Specifies options (silent, readonly, rapid) for the file opening. Default: silent |

## Run existing job

Use **job** verb with the below parameters to run existing *.xbatch file from the command line

~~~
> xbatch.exe job --run D:\sample.xbatch
~~~

| Short Flag  | Flag  |Required   | Summary  |
|---|---|---|---|
| -r  |  --run | Yes  | Full path to *.xbatch file to run

## Start with initial document

xBatch user interface can be started with initial document. Use **file** verb with the below parameters to start interface with pre-opened file.

~~~
> xbatch.exe file --new
~~~

| Short Flag  | Flag  |Required   | Summary  |
|---|---|---|---|
| -n  |  --new | No  | Starts application and creates new file
| -o  |  --open | No  | Starts application and opens specified file