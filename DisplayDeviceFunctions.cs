using System.Runtime.InteropServices;

namespace DisplayResolutionChanger;

public class DisplayDeviceFunctions
{
	public enum ChangeResult
	{
		Success = 0,
		Failed = 1,
		NeedsRestart = 2,
	}

	public static ChangeResult ChangeDisplayResolutionAndScale(int width, int height, int scale)
	{
		var result = ChangeDisplayResolution(width, height);
		if (result is ChangeResult.Success)
		{
			return ChangeDisplayScale(scale) ? result : ChangeResult.Failed;
		}

		return result;
	}

	public static ChangeResult ChangeDisplayResolution(int width, int height)
	{
		var device = new User32.DeviceMode
		{
			dmDeviceName = new string(new char[32]),
			dmFormName = new string(new char[32]),
		};
		device.dmSize = (short)Marshal.SizeOf(device);
		
		if (User32.EnumDisplaySettings(null, User32.ENUM_CURRENT_SETTINGS, ref device) == 0)
		{
			return ChangeResult.Failed;
		}

		device.dmPelsWidth = width;
		device.dmPelsHeight = height;
		if (User32.ChangeDisplaySettings(ref device, User32.CDS_TEST) != User32.DISP_CHANGE_SUCCESSFUL)
		{
			return ChangeResult.Failed;
		}

		return User32.ChangeDisplaySettings(ref device, User32.CDS_UPDATEREGISTRY) switch
		{
			User32.DISP_CHANGE_SUCCESSFUL => ChangeResult.Success,
			User32.DISP_CHANGE_RESTART => ChangeResult.NeedsRestart,
			_ => ChangeResult.Failed
		};
	}

	public static bool ChangeDisplayScale(int scaling)
	{
		return User32.SystemParametersInfo(0x009F, scaling, User32.NULL, 1);
	}
}
