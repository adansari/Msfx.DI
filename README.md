# Msfx.DI
A Conventional .net dependency injection framework empower developers to write loosely coupled components.

## Features
- **No** default injection 
- Lightweight, fast and zero boilerplate code required
- Registration and Auto-wiring by Convention
- Inject dependencies by Type or Type name
- Lifetime manager to manage instance life, currently local and static `default` instaces are being supported
- Controlled scanning i.e Current Namespace `default`, Recursive Namespace and Assembly
- Very little to learn to use this framework effectively
- Higly flexible and extensible design

## Release Status
***Under review*** - For review, please clone the repo and use the `Mxfx.DI.Lab` project to get your hand dirty. Do share your feedback and suggestion for improvement and next step.

## Getting Started
Lets get started with very basic example. 

```csharp
[Injectable]
public abstract class Animal
{
    public abstract void MakeSound();
}
```
An abstract class attributed as `Injectable` - it is must to have this attibute to make type as injectable.
```csharp
[Injectable]
public class Cat : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Meowwww");
    }
}
```
Now, a class `Cat` implementing the `Animal` abtract class.

```csharp
class Program
{
    static void Main(string[] args)
    {
        DIContext di = new AttributeBasedDIContext(typeof(Program)).Scan();

        Animal cat = di.Inject<Cat>();
        cat.MakeSound();//  Output =>  Meowwww
    }
}
```
Finally, creating the `DIContext` by passing the Type `Program` from current namespace to scan the same for dependency registration.

## Usage
Here is a more elaborative usage. 
 ```csharp
[Injectable] // This attribute is a must for dependency injection
public abstract class Computer
{
    protected OS _os;
    protected Processor _processor;
    public virtual Display Display { get; set; }
    public virtual Processor Processor { get { return this._processor; } }
    public virtual OS OS { get { return this._os; } }
    public virtual RAM RAM { get; set; }
    public abstract void Boot();
    public abstract void InstallOS(OS osToInstall);
}
 ```
An abstract class `Computer` composed of all its dependecies. And now here is the implementation.

```csharp
[Injectable(InstanceType = InstanceType.Local)] // A non-static instance of Desktop will be injected
public class Desktop : Computer
{
    protected Desktop() { } // A protected or private needed

    [AutoInject] // Constructor Injection 
    public Desktop([Inject(typeof(AMD))] Processor processor) // AMD Processor will be injected
    {
        this._processor = processor;
    }

    [AutoInject] // Property Injection
    [Inject(typeof(CRTMonitor))] // CRTMonitor will be injected here 
    public override Display Display { get; set; }

    [AutoInject] // Another Property Injection
    [InjectValue(8)] // Value(s) i.e. 8 will be passed to RAM constructor
    public override RAM RAM { get; set; }
    
    [AutoInject] // Method Injection
    public override void InstallOS(OS osToInstall) // Primary Target dependency will be injected for OS, refer the abstract class OS 
    {
        this._os = osToInstall;
    }

    public override void Boot()
    {
        Console.WriteLine("System booting...");
        Display.TurnON();
        RAM.GetReady();
        Processor.Compute();
        OS.Operate();
    }
}
```
And finally, let's have a one liner to instantiate a `Desktop` with all its dependencies

```csharp
var dIContext = new AttributeBasedDIContext(typeof(Program)).Scan();
var computer = dIContext.Inject<Desktop>();
computer.Boot();
```
Output
```console
System booting...
CRTMonitor starting
8 GB - RAM getting ready
AMD Processor
Windows operating...
```
Couple of points to note:
- Currently only Static and Non-Static i.e. local instance types are supported, by default static instance is created unless specified as `[Injectable(InstanceType = InstanceType.Local)]`
- You need to mark attribute `[AutoInject]` for Constructor, Method, Property and Field auto wiring.
- You can do the explicit injection for Constructor/Method Parameters, Property and Field by using the attribute `[Inject(typeof(..))]` i.e. `[Inject(typeof(AMD))]` AMD will be injected for Processor
- If you don't specify the explicit injection the Primary target dependency will be injected. Please note, for abstract class and interface you must specify their Primary target dependency if you are not using the explicit injection

- You can use the attribute `[InjectValue(....)]` to supply the dependency's constructor parameters

Now let's look at its dependencies one by one. Here is the `Processor` 
```csharp
[Injectable]
public abstract class Processor { public abstract void Compute(); }

[Injectable]
[InjectFor(typeof(Processor))] // Setting the Intel as a Primary target dependency for Processor
public class Intel : Processor
{
    public override void Compute() { Console.WriteLine("Intel Processor"); }
}

[Injectable]
public class AMD : Processor
{
    public override void Compute() { Console.WriteLine("AMD Processor"); }
}
```
Then we have Displays.

```csharp
[Injectable]
public abstract class Display { public abstract void TurnON(); }

[Injectable]
public class CRTMonitor : Display
{
    public override void TurnON() { Console.WriteLine("CRTMonitor starting"); }
}

[Injectable]
[InjectFor(typeof(Display))] // Setting the LCDMonitor as a Primary target dependency for Display
public class LCDMonitor : Display
{
    public override void TurnON() { Console.WriteLine("LCD starting"); }
}
```
Then RAM.

```csharp
[Injectable]
public class RAM
{
    protected RAM() { } // A protected or private needed

    protected int _sizeInGB;
    public RAM(int sizeInGB) { this._sizeInGB = sizeInGB; }

    public virtual void GetReady() { Console.WriteLine(_sizeInGB + " GB - RAM getting ready"); }
}

[Injectable]
public class DDRRAM : RAM
{
    protected DDRRAM() { } // A protected or private needed
    public DDRRAM(int sizeInGB) : base(sizeInGB) { }

    public override void GetReady() { Console.WriteLine(_sizeInGB + " GB - DDR RAM getting ready"); }
}
```
And finally an OS.
```csharp
[Injectable]
public abstract class OS { public abstract void Operate(); }

[Injectable]
[InjectFor(typeof(OS))] // Setting the Windows as a Primary target dependency for OS
public class Windows : OS
{
    public override void Operate() { Console.WriteLine("Windows operating..."); }
}

[Injectable]
public class Linux : OS
{
    public override void Operate() { Console.WriteLine("Linux operating..."); }
}
```
## Road-map
Coming soon..
## Contribute
Code of Conduct goes here
## License
Coming soon..
