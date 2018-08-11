A simple tool for Chasm gamedata deserialization.

# NOTES
* Particles currently cannot be saved.
* When deserializing (-d) or serializing (-s) normal '.ser'
  files you can specify [input] or <output> file accordingly
  with  extension '.csv' to process standard CSV sheet from EXEL

# USAGE:
```
ChasmDes.exe [action] [type] [inputFile] <outputFile>
ACTIONS:
-d      deserialize/decode game-file into JSON
-s      serialize/encode JSON into compatible game-file
TYPES:
-csv    for game files with extension '*.ser' (except formatted_text)
-fmt    special case for 'formatted_text.ser' files
-anm    for '.bin' files related to animation data (animations.json.bin)
-atl    for '.bin' files related to texture data (texture_atlas.json.bin)
-con    for '.bin' files related to conversations (conversations.json.bin)
-rmm    for '.bin' files related to room managment (roommanager.json.bin)
-mus    for '.bin' files related to music managment (musicmanager.json.bin)
-ovw    for '.bin' files related to overworld state (overworld.json.bin)
-par    for '.bin' files related to particle data (particles.json.bin)
-usr    processes global savegame (UserInfo.cfg)
-sav    processes specific savegame slot (*.sav)
```

# CHANGELOG
Aug 11/18 v0.4a
- Added support for latest patch 1.03 as of 11.08.2018
- Added read/write support for CSV (you now can serialize/deserialize)
  proper CSV format, see https://www.ietf.org/rfc/rfc4180.txt for syntax

Aug 10/18 v0.4
- Added OverWorldState deserializing to savedgame
- Small change in JSOn format for FormattedText - it is now part
  of one big StrStore collection. If you have old version, just
  add content into strStore { } object manually.

Aug 07/18 v0.3
- Fixed support for savegames decoding/encoding
- added support for: conversations, formatted text, music manager
  room manager, particles, overworld
- preliminary support or custom identing for JSON

Aug 05/18 v0.2
- Added support for savegames, texture atlas and animations

Aug 03/18 v0.1
- initial release

# CREDITS
Kein Zantezuken  
