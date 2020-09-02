<p align="center" style=";height:320px">
  <img src="https://github.com/KDani-99/FivePD-API/blob/master/Images/logo.svg" width="640" />
</p>

### Reporting errors

Create a new issue here (on GitHub) in the Issues tab with the appropriate label regarding your issue. This way, we can easily manage and solve issues quickly.

**Important issue**: `World.GetNextPositionOnSidewalk()` FiveM Source code sets that to Vector3.Zero if it couldn't find a good position around the area you specified (works within like 20-30ft of the player). ([**Source**](https://github.com/citizenfx/fivem/blob/master/code/client/clrcore/External/World.cs#L1189))

### Requesting new features

To request a new feature, either create a **pull request** or create a new **issue** with the **feature request** label.

### Get started

Before you start making your callouts, it's recommended to take a look at [how to write your first script in FiveM](https://docs.fivem.net/docs/scripting-manual/runtimes/csharp/) as this API is basically nothing more, but a wrapper to make it easier to write callouts with the reduced chance of doing something error prone (With extended features).

##### Creating a new callout using the example and cmd compiler
1. Download one of the [examples](https://github.com/KDani-99/FivePD-API/tree/master/Examples)).
2. Open the file and start making your new callout.
3. Type the following command in cmd (cs compiler) to compile your code.: (Assuming that you have the references in the same directory)<br/>
**Note:** .net.dll must be the assembly name. (For detailed instructions, [visit the official command line building csc](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/command-line-building-with-csc-exe))
```
csc -target:library CalloutName.cs -reference:CalloutAPI.net.dll -reference:CitizenFX.Core.dll -out:CalloutName.net.dll
```

##### From scratch in Visual Studio
1. Create a new C# Class Library project and make sure the target framework version is 4.5.2.
2. Go to Project > <ProjectName> Properties > Change the assembly name to <ProjectName>.net
3. Add CalloutAPI.dll from the FivePD download as a reference.
4. Add CitizenFX.Core.dll from your FiveM client as a reference. (`<client-root>\citizen\clr2\lib\mono\4.5\CitizenFX.Core.dll`)
5. Derive Callout class

(For detailed instructions on how to create your first FiveM script, [click here](https://docs.fivem.net/docs/scripting-manual/runtimes/csharp/ "refer here"))

For documentation, [visit the "wiki" tab](https://github.com/KDani-99/FivePD-API/wiki).

### Additional Support/Resources
We have an Offical FivePD API Discord Server which you can visit [HERE](https://discord.gg/tHZ4Yqc).

If you wish to view videos on creating callouts using this API you can find a playlist of videos [HERE](https://www.youtube.com/playlist?list=PL3m-r_SUQzQyEcMn__cyQSTNoegN7RNbS).
