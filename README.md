# GuardianAngel
![](angel.jpg)

This library allows you to use custom guard attributes to perform guard clauses instead of writing redundant guard clauses everywhere. Behind the scenes it uses PostSharp which generates the code for this on compile time.

This repo is just a joke (and not production ready at all!) cause of this video by [Nick Chapsas](https://www.youtube.com/@nickchapsas): \
https://youtu.be/Ij0fu-fNLJM?t=70

## Setup
**Don't use it.** If you want to use a professional/production ready library like this use [PostSharp.Patterns.Contracts](https://doc.postsharp.net/contracts) instead.

## Usage
Instead of using ``ArgumentOutOfRangeException.ThrowIfZero`` you can decorate your method parameters with ``[NotZeroGuard]``.
```cs
void NotZero([NotZeroGuard] int value)
{
    Console.WriteLine($"This will never be 0: {value}");
}
```

For every static ``ThrowIf`` method in ``ArgumentOutOfRangeException`` there's guard attribute. Here's a full list:
- ``NotZeroGuard``
- ``NotNegativeGuard``
- ``NotNegativeOrZero``
- ``NotGreaterThanGuard``
- ``NotGreaterThanOrEqualGuard``
- ``NotLessThanGuard``
- ``NotLessThanOrEqualGuard``

## Sample project
More samples can be found in the [GuardianAngel.Sample](/GuardianAngel.Sample) project.