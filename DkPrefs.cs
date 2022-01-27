namespace Tool.Compet.Preference {
	using Tool.Compet.Core;
	using UnityEngine;

	/// This is back-compatibility preferences, store user's settings at local disk storage.
	/// Note that, to avoid data-type mismatching between app's version. We decide to store/retrieve all
	/// settings via `string` type.
	/// For eg,. old version of app stores user's height as `float` value,
	/// but at new version, we store that height as `double` value which is not supported by `PlayerPrefs`.
	/// So to overcome that back-compability problem, we should read/write all settings value as `string`.

	/// Dependencies: `Newtonsoft.Json`.
	/// - Note1: We prefer use `XXX.TryParse()` instead since `XXX.Parse()` which causes exception if invalid input given.
	/// - Note2: About string-comparision in C#, we should always use `string.Equals(a, b)` or `"a".Equals(b)` instead of
	/// compare with `==` operator since we must care about reference-equality (for eg,. generic type) and value-equality.

	/// ■ Standalone storage path:
	/// - MacOS: ~/Library/Preferences/com.Kilobytes.SuperBattleOnline.plist
	/// - Windows: HKCU\Software\ExampleCompanyName\ExampleProductName
	/// - Android: /data/data/pkg-name/shared_prefs/pkg-name.v2.playerprefs.xml

	/// ■ In-editor play mode storage path:
	/// - MacOS: /Library/Preferences/[bundle identifier].plist
	/// - Windows: HKCU\Software\Unity\UnityEditor\ExampleCompanyName\ExampleProductName

	/// Ref: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
	public class DkPrefs {
		public static bool GetBoolean(string key, bool defaultValue = false) {
			return GetString(key).ParseBooleanDk(defaultValue);
		}
	
		public static int GetInt(string key, int defaultValue = 0) {
			return GetString(key).ParseIntDk(defaultValue);
		}
	
		public static long GetLong(string key, long defaultValue = 0L) {
			return GetString(key).ParseLongDk(defaultValue);
		}
	
		public static float GetFloat(string key, float defaultValue = 0F) {
			return GetString(key).ParseFloatDk(defaultValue);
		}
	
		public static double GetDouble(string key, double defaultValue = 0D) {
			return GetString(key).ParseDoubleDk(defaultValue);
		}
	
		public static string GetString(string key, string defaultValue = null) {
			return PlayerPrefs.GetString(key, defaultValue);
		}
	
		public static T GetJsonObj<T>(string key) where T : class {
			return DkJsons.Json2Obj<T>(GetString(key, null));
		}
	
		public static void PutBoolean(string key, bool value, bool alsoSave = false) {
			PutString(key, value.ToString(), alsoSave);
		}
	
		public static void PutInt(string key, int value, bool alsoSave = false) {
			PutString(key, value.ToString(), alsoSave);
		}
	
		public static void PutLong(string key, long value, bool alsoSave = false) {
			PutString(key, value.ToString(), alsoSave);
		}
	
		public static void PutFloat(string key, float value, bool alsoSave = false) {
			PutString(key, value.ToString(), alsoSave);
		}
	
		public static void PutDouble(string key, double value, bool alsoSave = false) {
			PutString(key, value.ToString(), alsoSave);
		}
	
		public static void PutString(string key, string value, bool alsoSave = false) {
			PlayerPrefs.SetString(key, value);
			if (alsoSave) {
				PlayerPrefs.Save();
			}
		}
	
		public static void PutJsonObj(string key, object serializableObj, bool alsoSave = false) {
			PutString(key, DkJsons.Obj2Json(serializableObj), alsoSave);
		}
	
		public static bool ContainsKey(string key) {
			return PlayerPrefs.HasKey(key);
		}
	
		public static void DeleteKey(string key, bool alsoSave = false) {
			PlayerPrefs.DeleteKey(key);
			if (alsoSave) {
				PlayerPrefs.Save();
			}
		}
	
		public static void Clear(bool alsoSave = true) {
			PlayerPrefs.DeleteAll();
			if (alsoSave) {
				PlayerPrefs.Save();
			}
		}
	
		/**
		 * Force persist preferences to local disk. Call this when we want to save preferences.
		 * Note that, until call this function, the changes made by other functions are NOT yet persisted.
		 */
		public static void Save() {
			PlayerPrefs.Save();
		}
	}
}
