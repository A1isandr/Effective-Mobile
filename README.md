# Delivery Service

With this application you can monitor and filter current orders. Additionaly you can save orders to a `.txt` file.

## Overview
The application consists of WEB API, written in ASP.NET and desktop app, made with [AvaloniaUI](https://github.com/AvaloniaUI/Avalonia).

- API uses database (SQLite) to store orders and EntityFramework to map it.
- Desktop app retrieves orders from API and presents them as table. User can filter orders by district and time. Then user can save results as a `.txt` file to the default or custom folder on his PC.
- Both API and desktop app are provided with basic logging. In the desktop app all logs are stored in a files.

## Configuration
You can configure default saving and logging paths in the `App.config` file, which can be found in a `Frontend.Desktop` directory.
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="DefaultSavePath" value="\saves"/>
		<add key="DefaultLogPath" value="\logs"/>
	</appSettings>
</configuration>
```
Here you can change default values to whatever fits you most. Relative path will be treated relatively to a working directory of the app.

## Note
Desktop app can work on Windows, Linux and MacOS, although was tested only on Windows 11.
