# Msfx.DI
Very basic .net dependency injection framework allow developers to write loosely coupled components.
## Features
- **No** default injection 
- Lightweight, fast and zero boilerplate code required
- Registration and Auto-wiring by Convention
- Inject dependencies by Type or Type name
- Lifetime manager to manage instance life, currently local and static `default` instaces are being supported
- Controlled scanning i.e Current Namespace `default`, Recursive Namespace and Assembly
- Very little to learn to use this framework effectively
- Higly flexible and extensible design

## Status
***Under review*** - For review, please clone the repo and use the `Mxfx.DI.Lab` project to get your hand dirty

## Getting Started
Get started with Msfx.DI Lab and play around it. Do share your feedback and suggestion for improvement and next step.

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
Now, a class implementing the `Animal` abtract class.

```csharp
class Program
{
    static void Main(string[] args)
    {
        DIContext di = new AttributeBasedDIContext(typeof(Program)).Scan();

        Animal cat = di.Inject<Cat>();
        cat.MakeSound();
    }
}
```
Finally, creating the `DIContext` by passing the Type `Program` from current namespace to scan the same for dependency registration.

