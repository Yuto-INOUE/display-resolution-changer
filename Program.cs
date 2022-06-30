namespace DisplayResolutionChanger;

internal static class Program
{
	public static void Main(string[] args)
	{
		if (!OperatingSystem.IsWindows()) throw new NotSupportedException("This program is for Windows only.");

		Console.WriteLine("[1]: 3840 x 2160 / 200%");
		Console.WriteLine("[2]: 1920 x 1080 / 100%");
		Console.Write("Which would you like to? -> ");

		var input = Console.ReadLine();
		switch (input?.Trim())
		{
			case "1":
				Exec(3840, 2160, 2);
				break;

			case "2":
				Exec(1920, 1080, -8);
				break;

			default:
				Environment.ExitCode = -1;
				break;
		}

		static void Exec(int width, int height, int scale)
		{
			var result = DisplayDeviceFunctions.ChangeDisplayResolutionAndScale(width, height, scale);
			Environment.ExitCode = result is DisplayDeviceFunctions.ChangeResult.Success
				? 1
				: -1;
		}
	}
}