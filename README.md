LittleBlocks.Reports
============

The library facilitates the generation of Excel and Json based results from different providers.

## Get Started

### How to use

```shell
Install-Package LittleBlocks.Reports
``` 

or 

```
dotnet add package LittleBlocks.Reports
```

Then you can add the following code to your startup to enable reporting support in **ServiceCollection**

```c#
services.AddReporting();
```

The following interfaces should be implemented and registered with **ServiceCollection**

#### Excel

The excel output renderer expects a template to be filled in by different providers. The excel rendered tries to map a named region in the excel to a simple or composite entity. The following interfaces should be provided to enable the end to end process:

- **ITemplateProvider": This is purely for the Excel output rendering and provides the empty template for Excel containing named regions. Multiple templates can be defined to cover each report type

As each section of the report can be provided from different data source, A series of interfaces need to be implemented for each entity 
- **SingleSnapshotDataRenderer<T> or CompositeSnapshotDataRenderer<T>**: Abstract classes to define how rendering needs to be done for a specific entity (T). Single is applying only one formatter and composite is required when multiple formatting is the target.
- **ExcelFormatter<T> or GroupExcelFormatter<T>**: Formatting the output including headers and rows. Group formatter is when single header is required with multiple grouped rows.

### How to Engage, Contribute, and Give Feedback

Description of the steps or process to be a contributor to the project.

Some of the best ways to contribute are to try things out, file issues, join in design conversations,
and make pull-requests.

* [Be an active contributor](./docs/CONTRIBUTING.md): Check out the contributing page to see the best places to log issues and start discussions.
* [Roadmap](./docs/ROADMAP.md): The schedule and milestone themes for project.

## Reporting bugs and useful features

Security issues and bugs should be reported by creating the relevant features and bugs in issues sections


## Related projects

- [LittleBlocks](https://github.com/LittleBlocks/LittleBlocks)
- [LittleBlocks.Ef](https://github.com/LittleBlocks/LittleBlocks.Ef)
- [LittleBlocks.Templates](https://github.com/LittleBlocks/LittleBlocks.Templates)
- [LittleBlocks.Excel](https://github.com/LittleBlocks/LittleBlocks.Excel)
- [CloseXml](https://github.com/closedxml)
