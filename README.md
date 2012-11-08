#Knuckleball

Knuckleball is a .NET wrapper for the excellent [mp4v2][1] library. It is intended to provide 
support for reading and writing MP4 files in a way that feels familiar to .NET developers. It 
is a work in progress.

## How To Use Knuckleball

At present, there is no binary distribution of Knuckleball. However, since Knuckleball has no
dependencies other than the libraries provided by the .NET Framework, building the binary from
source should be pretty straightforward. The library requires the .NET Framework v3.5 or higher.

Once you have built the Knuckleball.dll assembly and created a reference to it in your own
project, you should be able to do the following to change, say, the Title of the content of
a file:

```c-sharp
MP4File file = new MP4File(@"C:\path\to\valid\mp4file.m4v");
file.ReadTags();
file.Title = "My New Title";
file.WriteTags();
```

Please note that the API is very fluid at this point, and is likely to be changed without
fanfare or a great deal of notice. Only a 1.0 release will constitute a frozen API, with any
guarantees of stability from one release to the next.

One caveat is that you need to make sure there is a copy of libmp4v2.dll in the same directory
as the Knuckleball.dll assembly. A binary copy of this file can be found in the lib directory 
of the repository. If this file is not present, Knuckleball with throw an exception when you
attempt to read the tags.

## Frequently Asked Questions

### What functionality of the mp4v2 project is supported?

At present, reading and writing metadata tags is all that is implemented. Knuckleball should be
able to read and write the tags from the project, including the so-called "reverse DNS" atoms.
I would expect chapter editing support to be next on the list, but no guarantees on a schedule.
I started this project because I wanted a metadata editing library, and I now have the functionality
I need for my personal use. I will happily review pull requests, and discuss merging them into the 
code base.

### Why did you want a .NET metadata editing library? Doesn't MetaX already do that?

MetaX didn't meet my needs, as I couldn't incorporate my own metadata search results into its
workflow. Additionally, while MetaX certainly uses the mp4v2 library and is a .NET application,
its developer has declined to make the source code avaiable. Additionally, Knuckleball doesn't
provide a GUI; it's strictly a developer library at this time. I think it would be wonderful if
the developer of MetaX decided to use Knuckleball as the basis for MetaX's integration with
mp4v2, but I've not made any efforts in that direction.

### Where is the binary version? What about a NuGet package?

I would expect a binary distribution at some point, if there is a demand for it. This does *not*
mean I'll build a binary and email it to you! As for NuGet, I've had remarkably bad luck with
that particular packaging solution in other projects I contribute to, so I would expect a NuGet
package only after there is a regular binary distribution.

### I found a bug! What do you want me to do with it?

If it's a bug in the .NET wrapper code, the best way to make sure it gets fixed is to submit a 
pull request with the fix. Failing that, you can submit an issue report, but you'll need to make
the MP4 file available with it. Issue reports with all of the appropriate collateral will be
prioritized much higher than those that don't; issue reports without all of the information required
to reproduce the issue are likely to be closed without action.

### Why "Knuckleball"?

I needed a name for this library, and all of the names that combined "MP4", "tags", and ".NET" 
just sounded either too utilitarian or lame. Additionally, I'm a baseball fan, and grew up in
Atlanta, Georgia, USA, watching [Phil Niekro][2] ply his trade with the local baseball team.
I've always admired the knuckleball and the pitchers who've used it successfully, so I decided
to name it "Knuckleball".

[1]:http://code.google.com/p/mp4v2
[2]:http://en.wikipedia.org/wiki/Phil_Niekro