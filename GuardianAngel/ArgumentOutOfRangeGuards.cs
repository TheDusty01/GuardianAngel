using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Patterns.Contracts;
using PostSharp.Reflection;

namespace GuardianAngel;

internal static class OutOfRangeGuardsHelper
{
	public static MethodInfo ThrowIfZero { get; } = typeof(ArgumentOutOfRangeException)
		.GetMethod(nameof(ArgumentOutOfRangeException.ThrowIfZero), BindingFlags.Public | BindingFlags.Static)!;

	public static MethodInfo ThrowIfNegative { get; } = typeof(ArgumentOutOfRangeException)
		.GetMethod(nameof(ArgumentOutOfRangeException.ThrowIfNegative), BindingFlags.Public | BindingFlags.Static)!;

	public static MethodInfo ThrowIfNegativeOrZero { get; } = typeof(ArgumentOutOfRangeException)
		.GetMethod(nameof(ArgumentOutOfRangeException.ThrowIfNegativeOrZero), BindingFlags.Public | BindingFlags.Static)!;

	public static MethodInfo ThrowIfGreaterThan { get; } = typeof(ArgumentOutOfRangeException)
		.GetMethod(nameof(ArgumentOutOfRangeException.ThrowIfGreaterThan), BindingFlags.Public | BindingFlags.Static)!;

	public static MethodInfo ThrowIfGreaterThanOrEqual { get; } = typeof(ArgumentOutOfRangeException)
		.GetMethod(nameof(ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual), BindingFlags.Public | BindingFlags.Static)!;

	public static MethodInfo ThrowIfLessThan { get; } = typeof(ArgumentOutOfRangeException)
		.GetMethod(nameof(ArgumentOutOfRangeException.ThrowIfLessThan), BindingFlags.Public | BindingFlags.Static)!;

	public static MethodInfo ThrowIfLessThanOrEqual { get; } = typeof(ArgumentOutOfRangeException)
		.GetMethod(nameof(ArgumentOutOfRangeException.ThrowIfLessThanOrEqual), BindingFlags.Public | BindingFlags.Static)!;

	public static Exception? ValidateValue(MethodInfo methodInfo, ValueType value, string paramName)
	{
		var method = methodInfo.MakeGenericMethod(value.GetType());

		try
		{
			method!.Invoke(null, new object[] { value, paramName });
		}
		catch (TargetInvocationException ex)
		{
			return ex.InnerException;
		}

		return null;
	}

	public static Exception? ValidateValue(MethodInfo methodInfo, ValueType value, object otherValue, string paramName)
	{
		var method = methodInfo.MakeGenericMethod(value.GetType());

		try
		{
			method!.Invoke(null, new object[] { value, otherValue, paramName });
		}
		catch (TargetInvocationException ex)
		{
			return ex.InnerException;
		}

		return null;
	}
}

[AttributeUsage(AttributeTargets.Parameter)]
public class NotZeroGuardAttribute : LocationContractAttribute,
	ILocationValidationAspect<ValueType>
{
	public Exception ValidateValue(ValueType value, string locationName, LocationKind locationKind, LocationValidationContext context)
	{
		return OutOfRangeGuardsHelper.ValidateValue(OutOfRangeGuardsHelper.ThrowIfZero, value, locationName)!;
	}
}

// ThrowIfNegative where T : INumberBase<T>
[AttributeUsage(AttributeTargets.Parameter)]
public class NotNegativeGuardAttribute : LocationContractAttribute,
	ILocationValidationAspect<ValueType>
{
	public Exception ValidateValue(ValueType value, string locationName, LocationKind locationKind, LocationValidationContext context)
	{
		return OutOfRangeGuardsHelper.ValidateValue(OutOfRangeGuardsHelper.ThrowIfNegative, value, locationName)!;
	}
}

// ThrowIfNegativeOrZero where T : INumberBase<T>
[AttributeUsage(AttributeTargets.Parameter)]
public class NotNegativeOrZeroGuardAttribute : LocationContractAttribute,
	ILocationValidationAspect<ValueType>
{
	public Exception ValidateValue(ValueType value, string locationName, LocationKind locationKind, LocationValidationContext context)
	{
		return OutOfRangeGuardsHelper.ValidateValue(OutOfRangeGuardsHelper.ThrowIfNegativeOrZero, value, locationName)!;
	}
}

// ThrowIfGreaterThan where T : IComparable<T>
[AttributeUsage(AttributeTargets.Parameter)]
public class NotGreaterThanGuardAttribute : LocationContractAttribute,
	ILocationValidationAspect<ValueType>
{
	private readonly object greaterThanValue;

	public NotGreaterThanGuardAttribute(object greaterThanValue)
	{
		this.greaterThanValue = greaterThanValue;
	}

	public Exception ValidateValue(ValueType value, string locationName, LocationKind locationKind, LocationValidationContext context)
	{
		return OutOfRangeGuardsHelper.ValidateValue(OutOfRangeGuardsHelper.ThrowIfGreaterThan, value, greaterThanValue, locationName)!;
	}
}

// ThrowIfGreaterThanOrEqual where T : IComparable<T>
[AttributeUsage(AttributeTargets.Parameter)]
public class NotGreaterThanOrEqualGuardAttribute : LocationContractAttribute,
	ILocationValidationAspect<ValueType>
{
	private readonly object greaterThanOrEqualValue;

	public NotGreaterThanOrEqualGuardAttribute(object greaterThanOrEqualValue)
	{
		this.greaterThanOrEqualValue = greaterThanOrEqualValue;
	}

	public Exception ValidateValue(ValueType value, string locationName, LocationKind locationKind, LocationValidationContext context)
	{
		return OutOfRangeGuardsHelper.ValidateValue(OutOfRangeGuardsHelper.ThrowIfGreaterThanOrEqual, value, greaterThanOrEqualValue, locationName)!;
	}
}

// ThrowIfLessThan where T : IComparable<T>
[AttributeUsage(AttributeTargets.Parameter)]
public class NotLessThanGuardAttribute : LocationContractAttribute,
	ILocationValidationAspect<ValueType>
{
	private readonly object lessThanValue;

	public NotLessThanGuardAttribute(object lessThanValue)
	{
		this.lessThanValue = lessThanValue;
	}

	public Exception ValidateValue(ValueType value, string locationName, LocationKind locationKind, LocationValidationContext context)
	{
		return OutOfRangeGuardsHelper.ValidateValue(OutOfRangeGuardsHelper.ThrowIfLessThan, value, lessThanValue, locationName)!;
	}
}

// ThrowIfLessThanOrEqual where T : IComparable<T>
[AttributeUsage(AttributeTargets.Parameter)]
public class NotLessThanOrEqualGuardAttribute : LocationContractAttribute,
	ILocationValidationAspect<ValueType>
{
	private readonly object lessThanOrEqualValue;

	public NotLessThanOrEqualGuardAttribute(object lessThanOrEqualValue)
	{
		this.lessThanOrEqualValue = lessThanOrEqualValue;
	}

	public Exception ValidateValue(ValueType value, string locationName, LocationKind locationKind, LocationValidationContext context)
	{
		return OutOfRangeGuardsHelper.ValidateValue(OutOfRangeGuardsHelper.ThrowIfLessThanOrEqual, value, lessThanOrEqualValue, locationName)!;
	}
}