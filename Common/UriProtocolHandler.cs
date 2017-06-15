using System;
using Microsoft.Win32;

namespace Common
{
	public class UriProtocolHandler
	{
		const string CURRENT_USER_CLASSES = @"SOFTWARE\Classes\";
		const string URI_SCHEME = "testapp";
		const string SHELL_OPEN_COMMAND_SUBKEY_NAME = @"Shell\Open\Command";
		const string DEFAULT_ICON_KEY_NAME = "DefaultIcon";
		const string URL_PROTOCOL_VALUE_NAME = "URL Protocol";
		const string URL_PROTOCOL_VALUE = "URL:test protocol";
		const string URI_ARGUMENT_VALUE = "-u \"%1\"";
		public void CreateUriProtocolHandler(string pathToApp)
		{
			Execute(pathToApp);

		}

		private void Execute(string pathToApp)
		{
			var appSubKey = FindOrCreateSubKey(Registry.CurrentUser, CURRENT_USER_CLASSES + URI_SCHEME);
			if (appSubKey != null)
			{
				SetValue(appSubKey, null, URL_PROTOCOL_VALUE);
				SetValue(appSubKey, URL_PROTOCOL_VALUE_NAME, URL_PROTOCOL_VALUE);

				var defaultIconSubKey = FindOrCreateSubKey(appSubKey, DEFAULT_ICON_KEY_NAME);
				if (defaultIconSubKey != null)
				{
					SetValue(defaultIconSubKey, null, $"\"{pathToApp}\"");
				}

				var shellSubKey = FindOrCreateSubKey(appSubKey, SHELL_OPEN_COMMAND_SUBKEY_NAME);
				if (shellSubKey != null)
				{
					SetValue(shellSubKey, null, $"\"{pathToApp}\" {URI_ARGUMENT_VALUE}");
				}
				appSubKey.Close();
			}
		}

		private RegistryKey FindOrCreateSubKey(RegistryKey rootToSearchFrom, string keyName)
		{
			if (rootToSearchFrom == null)
			{
				throw new ArgumentNullException(nameof(rootToSearchFrom));
			}

			var subKey = rootToSearchFrom.OpenSubKey(keyName, true);
			if (subKey == null)
			{
				subKey = rootToSearchFrom.CreateSubKey(keyName, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
			}
			return subKey;
		}

		private void SetValue(RegistryKey registryKey, string valueName, string valueToSet)
		{
			if (valueToSet == null)
			{
				throw new ArgumentNullException(nameof(valueToSet));
			}
			string registryValue = registryKey.GetValue(valueName) as string;
			if (!string.Equals(valueToSet, registryValue))
			{
				registryKey.SetValue(valueName, valueToSet, RegistryValueKind.String);
			}
		}
	}
}