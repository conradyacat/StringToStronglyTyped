StringToStronglyTyped
=====================

A simple utility to convert string values to strongly-typed objects.

[![NuGet](https://img.shields.io/nuget/v/StringToStronglyTyped.svg)](https://nuget.org/packages/StringToStronglyTyped)

# Examples
## Converting a string to another type
```C#

int @int = StronglyTypedConverter.ToType<int>("123");

decimal @decimal = StronglyTypedConverter.ToType<decimal>("123.456");
```

## Converting a Hashtable to a Strongly-typed object
```C#
public class TestModel
{
    [StronglyTypedMetadata]
    public short Int16 { get; private set; }

    [StronglyTypedMetadata]
    public decimal Decimal { get; private set; }  
}

...

var hashtable = new Hashtable();
hashtable.Add("Int16", "123");
hashtable.Add("Decimal", "0.123");

var testModel = StronglyTypedConverter.ToType<TestModel>(hashtable);
```