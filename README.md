# Enable.Common.ValidationException

A .NET exception type that represents one or more business logic or validation error conditions.

[![Build status](https://ci.appveyor.com/api/projects/status/492qkdxkdvkwnnbe?svg=true)](https://ci.appveyor.com/project/EnableSoftware/enable-common-validationexception)

## Usage

```csharp
throw new ValidationException();
throw new ValidationException("exception message");
throw new ValidationException("exception message", wrappedException);
throw new ValidationException(new[] { "validation message 1", "validation message 2" });
throw new ValidationException("exception message", new[] { "validation message 1", "validation message 2" });
```

