# sbox-steamid-converter
Convert between SteamID, SteamID3, SteamID32, SteamID64

```
Log.Info( SteamIDConverter.ToSteamID( "STEAM_0:1:642728822" ) );
Log.Info( SteamIDConverter.ToSteamID64( "STEAM_0:1:642728822" ) );
Log.Info( SteamIDConverter.ToSteamID32( "STEAM_0:1:642728822" ) );
Log.Info( SteamIDConverter.ToSteamID3( "STEAM_0:1:642728822" ) );

Log.Info( ----------------------- );

Log.Info( SteamIDConverter.ToSteamID( "76561199245723373" ) );
Log.Info( SteamIDConverter.ToSteamID64( "76561199245723373" ) );
Log.Info( SteamIDConverter.ToSteamID32( "76561199245723373" ) );
Log.Info( SteamIDConverter.ToSteamID3( "76561199245723373" ) );

Log.Info( ----------------------- );

Log.Info( SteamIDConverter.ToSteamID( "[U:1:1285457645]" ) );
Log.Info( SteamIDConverter.ToSteamID64( "[U:1:1285457645]" ) );
Log.Info( SteamIDConverter.ToSteamID32( "[U:1:1285457645]" ) );
Log.Info( SteamIDConverter.ToSteamID3( "[U:1:1285457645]" ) );

Log.Info( ----------------------- );

Log.Info( SteamIDConverter.ToSteamID( "1285457645" ) );
Log.Info( SteamIDConverter.ToSteamID64( "1285457645" ) );
Log.Info( SteamIDConverter.ToSteamID32( "1285457645" ) );
Log.Info( SteamIDConverter.ToSteamID3( "1285457645" ) );



Results;

STEAM_0:1:642728822
76561199245723373
1285457645
[U:1:1285457645]
-----------------------
STEAM_0:1:642728822
76561199245723373
1285457645
[U:1:1285457645]
-----------------------
STEAM_0:1:642728822
76561199245723373
1285457645
[U:1:1285457645]
-----------------------
STEAM_0:1:642728822
76561199245723373
1285457645
[U:1:1285457645]

```
