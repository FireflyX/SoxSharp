## SoxSharp

SoxSharp is a C# library that serves as a wrapper to [SoX - the Sound eXchange tool](http://sox.sourceforge.net/).
SoX is a cross-platform (Windows, Linux, MacOS X, etc.) command line utility that can convert various formats of computer audio files in to other formats. It can also apply various effects to these sound files and play and record audio files on most platforms.


### How it works

#### Get file information

Usage is pretty straightforward:

```cs
using (Sox sox = new Sox("sox.exe"))
{
  FileInfo wavInfo = sox.GetInfo("test.wav");
  Console.WriteLine(wavInfo);
}
```

This is the same as executing `sox --info test.wav`. SoxSharp parses the SoX output and fills a **FileInfo** instance with the retrieved information.

When instantiating the **Sox** class, we pass to the constructor the location of the SoX executable to be used.


#### File conversions

The simplest way to perform an audio conversion is to just call the **Process** method with both input and output files:

```cs
using (Sox sox = new Sox("sox.exe"))
{
  sox.Process("test.wav", "test.mp3");
}
```

The previous code will launch SoX with 'test.wav' as input file and 'test.mp3' as output file (the equivalent of executing SoX with the following parameters: `sox test.wav test.mp3`). 

To force the output to be encoded with MP3 format (instead of letting SoX to guess it from the output file extension) and use a sample rate of 32 KHz:

```cs
using (Sox sox = new Sox("sox.exe"))
{
  sox.Output.Type = FileType.MP3;
  sox.Output.SampleRate = 32000;

  sox.Process("test.wav", "test.mp3");
}
```

This is equivalent to call SoX with the following parameters: `sox test.wav --type mp3 --rate 32000 test.mp3`.

SoX global options can be set through their respective properties in the **Sox** class. Format options to be applied to input and output can be stablished through their respective properties in **Sox.InputOptions** and **Sox.OutputOptions** members.

#### Events

You can subscribe to receive any warning or error message generated by SoX through the **OnLogMessage** event.

```cs
// Subscribe to OnLogMessage.
sox.OnLogMessage += sox_OnLogMessage;

void sox_OnLogMessage(object sender, LogMessageEventArgs e)
{
  Console.WriteLine(e.LogLevel + ": " + e.Message);
}
```

Also, while executing a **Process** call, to obtain updated progress information about the operation, you can subscribe to the **OnProgress** event:

```cs
// Subscribe to OnProgress event before calling Process method.
sox.OnProgress += sox_OnProgress;

void sox_OnProgress(object sender, ProgressEventArgs e)
{
  Console.WriteLine("{0} ({1}% completed)", e.Processed.ToString(@"hh\:mm\:ss\.ff"), e.Progress);
}
```


### Concurrency

All the work is done in the calling thread. Each **Sox** class instance can handle only one process at the same time, and will block the calling thread until finished. You are responsible for creating any worker thread if needed. 

The library provides two different methods to cancel any work that is in progress:

* Calling the **Abort()** method of the **Sox** class. 

* Inside the **OnProgress** event handler. **ProgressEventArgs** provides a boolean **Abort** member that can be set to **true** to end the current work.


### Library Reference

A detailed description of all components of the library is available at the [repository wiki](https://github.com/igece/SoxSharp/wiki/Reference-Guide). 


