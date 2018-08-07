# ChasmDeserializer
Simple tool made to deserialize Chasm gamedata

# [NOTE]
* Particles currently cannot be saved.
* When decoding normal '.ser' files you can specify output file
  with extension '.csv' to receive standard CSV sheet for tables.

# USAGE:
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

# [CREDITS]
Kein Zantezuken
Tool is provided "as is" under Creative Common ND license.

# [CHANGELOG]
Aug 07/18 v0.3
- Fixed support for savegames decoding/encoding
- added support for: conversations, formatted text, music manager
  room manager, particles, overworld
- preliminary support or custom identing for JSON
[CHANGELOG]
Aug 05/18 v0.2
- Added support for savegames, texture atlas and animations
Aug 03/18 v0.1
- initial release

