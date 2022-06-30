using System.Runtime.InteropServices;

namespace DisplayResolutionChanger;

internal class User32
{
	public const int NULL = 0;

	public const int ENUM_CURRENT_SETTINGS = -1;

	public const int CDS_UPDATEREGISTRY = 0x01;
	public const int CDS_TEST = 0x02;

	public const int DISP_CHANGE_SUCCESSFUL = 0;
	public const int DISP_CHANGE_RESTART = 1;
	public const int DISP_CHANGE_FAILED = -1;
	public const int DISP_CHANGE_BADMODE = -2;
	public const int DISP_CHANGE_NOTUPDATED = -3;
	public const int DISP_CHANGE_BADFLAGS = -4;
	public const int DISP_CHANGE_BADPARAM = -5;

	[StructLayout(LayoutKind.Sequential)]
	public struct DeviceMode
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string dmDeviceName;
		public short dmSpecVersion;
		public short dmDriverVersion;
		public short dmSize;
		public short dmDriverExtra;
		public int dmFields;
		public short dmOrientation;
		public short dmPaperSize;
		public short dmPaperLength;
		public short dmPaperWidth;
		public short dmScale;
		public short dmCopies;
		public short dmDefaultSource;
		public short dmPrintQuality;
		public short dmColor;
		public short dmDuplex;
		public short dmYResolution;
		public short dmTTOption;
		public short dmCollate;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string dmFormName;
		public short dmLogPixels;
		public short dmBitsPerPel;
		public int dmPelsWidth;
		public int dmPelsHeight;
		public int dmDisplayFlags;
		public int dmDisplayFrequency;
		public int dmICMMethod;
		public int dmICMIntent;
		public int dmMediaType;
		public int dmDitherType;
		public int dmReserved1;
		public int dmReserved2;
		public int dmPanningWidth;
		public int dmPanningHeight;
	};

	[DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
	public static extern bool SystemParametersInfo(int uiAction, int uiParam, int pvParam, int fWinIni);

	[DllImport("user32.dll")]
	public static extern int EnumDisplaySettings(string? deviceName, int modeNum, ref DeviceMode devMode);

	[DllImport("user32.dll")]
	public static extern int ChangeDisplaySettings(ref DeviceMode devMode, int flags);
}