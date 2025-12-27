using System.Configuration;

namespace PaintDotCat
{
	/// <summary>
	/// 設定の 保存/復元 用
	/// </summary>
	public class UserSettings : ApplicationSettingsBase
	{
		[UserScopedSetting()]
		[DefaultSettingValue("32")]
		public int ImgWidth
		{//画像サイズ
			get{	return (int)this["ImgWidth"];	}
			set{	this["ImgWidth"] = value;	}
		}

		[UserScopedSetting()]
		[DefaultSettingValue("32")]
		public int ImgHeight
		{//画像サイズ
			get{	return (int)this["ImgHeight"];	}
			set{	this["ImgHeight"] = value;	}
		}

		[UserScopedSetting()]
		[DefaultSettingValue("true")]
		public bool ConfirmAtSave
		{//SaveAs の際に確認を取るか否か
			get{	return (bool)this["ConfirmAtSave"];	}
			set{	this["ConfirmAtSave"] = value;	}
		}

		[UserScopedSetting()]
		[DefaultSettingValue("false")]
		public bool ShowFullPath
		{//キャプションにフルパス名を表示するか否か
			get{	return (bool)this["ShowFullPath"];	}
			set{	this["ShowFullPath"] = value;	}
		}
	}

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

			var Settings = new UserSettings();

            var MainGUI = new MainForm( Settings );
			Application.Run( MainGUI );
			MainGUI.ModifySettings( Settings );

			Settings.Save();
        }
    }
}