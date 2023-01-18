using GuardianAngel;

internal class Program
{
	private static void Main(string[] args)
	{
		var calls = new List<Action>()
		{
			() => NotZero(0),
			() => NotZero(1),

			() => NotNegative(0),
			() => NotNegative(-1),

			() => NotNegativeOrZero(1),
			() => NotNegativeOrZero(0),
			() => NotNegativeOrZero(-1),

			() => NotGreaterThan(1),
			() => NotGreaterThan(2),

			() => NotGreaterThanOrEqual(0),
			() => NotGreaterThanOrEqual(1),
			() => NotGreaterThanOrEqual(2),

			() => NotLessThan(1),
			() => NotLessThan(0),

			() => NotLessThanOrEqual(2),
			() => NotLessThanOrEqual(1),
			() => NotLessThanOrEqual(0),

		};

		foreach (var call in calls)
		{
			TestCall(call);
		}
	}

	public static void TestCall(Action action)
	{
		try
		{
			action();
		}
		catch (ArgumentOutOfRangeException ex)
		{
			Console.WriteLine($"Call failed: {ex}");
			return;
		}

		Console.WriteLine($"Call succeeded!");
	}

	public static void NotZero([NotZeroGuard] int a)
	{
		Console.WriteLine("NotZero: " + a);
	}

	public static void NotNegative([NotNegativeGuard] int a)
	{
		Console.WriteLine("NotNegative: " + a);
	}

	public static void NotNegativeOrZero([NotNegativeOrZeroGuard] int a)
	{
		Console.WriteLine("NotNegativeOrZero: " + a);
	}

	public static void NotGreaterThan([NotGreaterThanGuard(1)] int a)
	{
		Console.WriteLine("NotGreaterThanGuard: " + a);
	}

	public static void NotGreaterThanOrEqual([NotGreaterThanOrEqualGuard(1)] int a)
	{
		Console.WriteLine("NotGreaterThanOrEqual: " + a);
	}

	public static void NotLessThan([NotLessThanGuard(1)] int a)
	{
		Console.WriteLine("NotLessThan: " + a);
	}

	public static void NotLessThanOrEqual([NotLessThanOrEqualGuard(1)] int a)
	{
		Console.WriteLine("NotLessThanOrEqual: " + a);
	}
}