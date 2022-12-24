//sbox.Community - 2023

using System.IO;
using System.Text.RegularExpressions;

public static class SteamIDConverter
{
	public static readonly string SteamIDExpression = @"^STEAM_[0-5]:[01]:\d+$";
	public static readonly string SteamID3Expression = @"^\[U:[1]:[0-9]+\]$";
	public static readonly string SteamID32Expression = @"^[0-9]{1,16}$";
	public static readonly string SteamID64Expression = @"^7656[0-9]*$";
	public static readonly Regex SteamIDRegex = new Regex( SteamIDExpression, RegexOptions.IgnoreCase );
	public static readonly Regex SteamID3Regex = new Regex( SteamID3Expression );
	public static readonly Regex SteamID32Regex = new Regex( SteamID32Expression );
	public static readonly Regex SteamID64Regex = new Regex( SteamID64Expression );

	public static bool IsSteamID( string steamID ) => SteamIDRegex.Match( steamID ).Success;
	public static bool IsSteamID3( string steamID ) => SteamID3Regex.Match( steamID ).Success;
	public static bool IsSteamID32( string steamID ) => SteamID32Regex.Match( steamID ).Success;
	public static bool IsSteamID64( string steamID64 ) => !(SteamID64Regex.Matches( steamID64 ).Count <= 0);
	public static bool IsSteamID64( long steamID64 ) => IsSteamID64( steamID64.ToString() );

	public enum SteamIdentifierType
	{
		SteamID,
		SteamID3,
		SteamID32,
		SteamID64,
		Invalid
	}

	//https://stackoverflow.com/questions/46375288/how-to-get-the-normal-steam-id-from-the-steam-id-64-using-c-sharp
	public static string ToSteamID( string steamID )
	{
		var detect = DetectSteamID( steamID );
		long steamID64;

		if ( detect == SteamIdentifierType.SteamID )
			return steamID;
		else if ( detect == SteamIdentifierType.SteamID3 )
			steamID64 = ToSteamID64( steamID );
		else if ( detect == SteamIdentifierType.SteamID32 )
			steamID64 = ToSteamID64( steamID );
		else if ( detect == SteamIdentifierType.SteamID64 )
			steamID64 = long.Parse(steamID);
		else
		{
			Log.Error( "Not Valid SteamID" );
			return string.Empty;
		}

		if ( !IsSteamID64( steamID64 ) )
		{
			Log.Error( "Error, Not Valid SteamID" );
			return string.Empty;
		}

		var authserver = (steamID64 - 76561197960265728) & 1;
		var authid = (steamID64 - 76561197960265728 - authserver) / 2;
		return $"STEAM_0:{authserver}:{authid}";
	}
	public static string ToSteamID( long steamID ) => ToSteamID( steamID.ToString() );

	//https://stackoverflow.com/questions/55029403/converting-a-users-steam-id-from-console-to-the-64bit-version-in-c-sharp
	//https://github.com/arhi3a/Steam-ID-Converter/blob/master/steam_id_converter.py
	public static long ToSteamID64( string steamID )
	{
		var detect = DetectSteamID( steamID );

		if ( detect == SteamIdentifierType.SteamID )
		{
			steamID = ToSteamID( steamID );

			if ( !IsSteamID( steamID ) )
			{
				Log.Error( "Not valid SteamID!" );
				return 0;
			}

			var split = steamID.Split( ":" );

			var v = 76561197960265728;
			var y = long.Parse( split[1] );
			var z = long.Parse( split[2] );

			return (z * 2) + v + y;
		}
		else if ( detect == SteamIdentifierType.SteamID3 )
		{
			var steam32 = steamID.Replace( "[U:1:", "" ).Replace( "]", "" );
			return long.Parse( steam32 ) + 76561197960265728;
		}
		else if ( detect == SteamIdentifierType.SteamID32 )
			return long.Parse( steamID ) + 76561197960265728;
		else if ( detect == SteamIdentifierType.SteamID64 )
			return long.Parse( steamID );
		else
		{
			Log.Error( "Not Valid SteamID" );
			return 0;
		}
	}
	public static long ToSteamID64( long steamID ) => ToSteamID64( steamID.ToString() );

	public static long ToSteamID32( string steamID )
	{
		var detect = DetectSteamID( steamID );

		if ( detect == SteamIdentifierType.SteamID ) { }
		else if ( detect == SteamIdentifierType.SteamID3 )
			return long.Parse( steamID.Replace( "[U:1:", "" ).Replace( "]", "" ) );
		else if ( detect == SteamIdentifierType.SteamID32 )
			return long.Parse( steamID );
		else if ( detect == SteamIdentifierType.SteamID64 )
			steamID = ToSteamID( steamID );
		else
		{
			Log.Error( "Not Valid SteamID" );
			return 0;
		}

		if ( !IsSteamID( steamID ) )
		{
			Log.Error( "Error, Not Valid SteamID" );
			return 0;
		}

		var split = steamID.Split( ":" );

		var y = long.Parse( split[1] );
		var z = long.Parse( split[2] );

		return z * 2 + y;
	}

	public static long ToSteamID32( long steamID ) => ToSteamID32( steamID.ToString() );

	public static string ToSteamID3( string steamID )
	{
		var detect = DetectSteamID( steamID );

		if ( detect == SteamIdentifierType.SteamID ) { }
		else if ( detect == SteamIdentifierType.SteamID3 )
			return steamID;
		else if ( detect == SteamIdentifierType.SteamID32 )
			return $"[U:1:{steamID}]";
		else if ( detect == SteamIdentifierType.SteamID64 )
			steamID = ToSteamID( steamID );
		else
		{
			Log.Error( "Not Valid SteamID" );
			return string.Empty;
		}

		if ( !IsSteamID( steamID ) )
		{
			Log.Error( "Error, Not Valid SteamID" );
			return string.Empty;
		}

		return $"[U:1:{ToSteamID32( steamID )}]";
	}
	public static string ToSteamID3( long steamID ) => ToSteamID3( steamID.ToString() );

	public static SteamIdentifierType DetectSteamID( long steamID ) => DetectSteamID( steamID.ToString() );
	public static SteamIdentifierType DetectSteamID( string steamID )
	{
		if ( IsSteamID( steamID ) )
			return SteamIdentifierType.SteamID;
		else if ( IsSteamID64( steamID ) )
			return SteamIdentifierType.SteamID64;
		else if ( IsSteamID3( steamID ) )
			return SteamIdentifierType.SteamID3;
		else if ( IsSteamID32( steamID ) )
			return SteamIdentifierType.SteamID32;
		else
			return SteamIdentifierType.Invalid;
	}
}
