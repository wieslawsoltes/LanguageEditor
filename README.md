# LanguageEditor

[![Build status](https://ci.appveyor.com/api/projects/status/7khsyl0fw7fqp9xw/branch/master?svg=true)](https://ci.appveyor.com/project/wieslawsoltes/languageeditor/branch/master)

[![Github All Releases](https://img.shields.io/github/downloads/wieslawsoltes/languageeditor/total.svg)](https://github.com/wieslawsoltes/LanguageEditor/releases)
[![GitHub Release](https://img.shields.io/github/release/wieslawsoltes/languageeditor.svg)](https://github.com/wieslawsoltes/LanguageEditor/releases/latest)
[![Github Releases](https://img.shields.io/github/downloads/wieslawsoltes/languageeditor/latest/total.svg)](https://github.com/wieslawsoltes/LanguageEditor/releases)

A dedicated xml language translation files editor.

## Download LanguageEditor

| Platform                   | Type        | Version       | Download                                                                                                                               |
|----------------------------|-------------|---------------|----------------------------------------------------------------------------------------------------------------------------------------|
| Windows .NET4.5 (Any CPU)  | Portable    | 1.0.0         | [LanguageEditor-1.0.0-AnyCPU.zip](https://github.com/wieslawsoltes/LanguageEditor/releases/download/1.0.0/LanguageEditor-1.0.0-AnyCPU.zip) |
| Windows .NET4.5 (32-bit)   | Portable    | 1.0.0         | [LanguageEditor-1.0.0-x86.zip](https://github.com/wieslawsoltes/LanguageEditor/releases/download/1.0.0/LanguageEditor-1.0.0-x86.zip)       |
| Windows .NET4.5 (64-bit)   | Portable    | 1.0.0         | [LanguageEditor-1.0.0-x64.zip](https://github.com/wieslawsoltes/LanguageEditor/releases/download/1.0.0/LanguageEditor-1.0.0-x64.zip)       |

### CI Builds

[Download](https://ci.appveyor.com/project/wieslawsoltes/languageeditor/build/artifacts) bleeding edge builds from the CI server.

## About

LanguageEditor is a dedicated xml language translation files editor.

LanguageEditor requires .NET Framework 4.5 and Windows 7 SP1 or above.

## Language file format

Use only UTF-8 compatible text editors (e.g. Notepad or Visual Studio Code) to edit xml language files
and [use special characters in XML](https://www.dvteclipse.com/documentation/svlinter/How_to_use_special_characters_in_XML.3F.html#gsc.tab=0).

```XML
<?xml version="1.0" encoding="UTF-8"?>
<Language id="en-US" original="English" translated="English">
    <String key="0x00010001" value="&amp;File" />
    <String key="0x00020001" value="&amp;Edit" />
    <!-- Comment -->
    <String key="0x00060001" value="&amp;Help" />
</Language>
```

## LanguageEditor Sources

Sources are available in the [git source code repository](https://github.com/wieslawsoltes/LanguageEditor.git).

## Building LanguageEditor

To build program from sources you will need the following components:

### Microsoft Visual Studio 2017

For building `LanguageEditor.sln` solution use [Visual Studio 2017](https://www.visualstudio.com).

## License

LanguageEditor is licensed under the [MIT license](LICENSE.TXT).
