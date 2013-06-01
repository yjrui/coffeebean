using MobilEditCom;
using System;
namespace COMlib
{
	public class CKeyboard : CItem
	{
		public enum EKeys
		{
			Num_0 = 48,
			Num_1,
			Num_2,
			Num_3,
			Num_4,
			Num_5,
			Num_6,
			Num_7,
			Num_8,
			Num_9,
			Star = 42,
			Hash = 35,
			ConnEnd = 69,
			ConnStart = 83,
			Function = 70,
			Menu = 77,
			Clear = 67,
			LeftArrow = 60,
			RightArrow = 62,
			UpArrow = 94,
			DownArrow = 118,
			VolumeUp = 85,
			VolumeDown = 68,
			Power = 80,
			Joystick = 74,
			Return = 82,
			Operator = 79,
			SoftKey_1 = 91,
			SoftKey_2 = 93,
			Camera = 109,
			Focus = 102,
			VoiceRecorder = 114,
			PTT = 112,
			Browser = 98
		}
		internal IKeyboardCapability MedKeyboardCapability;
		internal CKeyboard(CItem parent, ICapability medCapability) : base(parent)
		{
			this.MedKeyboardCapability = (IKeyboardCapability)medCapability;
		}
		public bool IsSupported(CKeyboard.EKeys key)
		{
			return this.MedKeyboardCapability.IsKeySupported((int)key) != 0;
		}
		public void Press(CKeyboard.EKeys key)
		{
			if (!this.IsSupported(key))
			{
				throw new ArgumentException("Unsupported key");
			}
			this.MedKeyboardCapability.PressKey((int)key);
		}
		public void Release(CKeyboard.EKeys key)
		{
			if (!this.IsSupported(key))
			{
				throw new ArgumentException("Unsupported key");
			}
			this.MedKeyboardCapability.ReleaseKey((int)key);
		}
	}
}
