# WIC for .NET

A .NET library that makes the Windows Imaging Component (WIC) available to managed code. The library provides a thin layer of abstractions and extension methods to make it easier to work with the raw WIC interface. 

Version 1.x of the library targets .NET Standard 2.0, so it can be used in .NET Framework and UWP (.NET Native) apps. Version 2.x targets .NET 8.0 and can be used in trimming and Native AOT scenarios.

## What is the Windows Imaging Component (WIC)?

> _"The Windows Imaging Component (WIC) is a Component Object Model based imaging codec framework
> introduced in Windows Vista (…) for working with and processing digital images and image metadata."_
> &mdash; from the [Wikipedia article](wikipedia)

 [wikipedia]: https://en.wikipedia.org/wiki/Windows_Imaging_Component

Windows Imaging Component allows you to accomplish tasks such as:

 * decoding and encoding bitmap images or single bitmap image frames in various formats (GIF, ICO, JPEG, PNG, TIFF, and more)
 * reading and writing image metadata
 * converting bitmaps to different pixel formats (bit depth, channels)
 * transforming bitmaps (clip, flip horizontally or vertically, rotate by 90° angles, scale)

## How do you get started?

 1. Familiarize yourself with the WIC, if you don't know it yet.
    See e.g. the [Windows Imaging Component documentation on MSDN][msdn].

 2. Add the NuGet package [`WIC.DotNet`][nuget-package] to your project.

 3. In your code, start by instantiating a `WICImagingFactory` object.  
    For Version 1.x use: `new WICImagingFactory()`  
    For Version 2.x use: `WICImagingFactory.Create()`  
    Most other WIC components can be created directly or indirectly through this factory object.
    
 [msdn]: https://msdn.microsoft.com/en-us/library/windows/desktop/ee719902.aspx
 [nuget-package]: https://www.nuget.org/packages/WIC.DotNet/

## Is there any example code?

You can find some samples on GitHub in the [Samples] directory and you can also refer to the the WIC samples on [MSDN].

[Samples]: https://github.com/sbaeumlisberger/WIC-DotNet/tree/develop/Samples
