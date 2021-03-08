# Status Update 2

The past three week have been eventful and productive! Last week we were back to our beginning stage of switching from 2D to 3D, now we are working on the creation of our AI, finishing up our networking, and beginning to set the stage for the art style. Our primary goal these past three weeks was to complete our Networking layer and will be completed by the end of this week. While the Network was being developed,  here are just a few other tasks that the groups has started to work on:

Management layer of our AI has been built.
Terrain generation refined 
Player lobbies implemented 

Our quantifiable metric was to get the Network built and AI started.        

## Successes
- Terrain generation now has a viability verification process to ensure the procedurally generated map is connected up with required resource nodes on the map. This was accomplished by Brandon and James and took 8 hours to implement. 
- The AI management layer was built into Server side code. It also built the system to allow for personalized behavior vectors to be added in and combined to create the state machine in the coming sprint. This took Andey 4 hours
- The Networking layer was built and implemented to allow for hosting, standalone servers, and client connections. Map spawning, player movement, and player lobby added.  Network physics were implemented with a generic force API to allow for arbitrary physics actions to be synchronized across the server. Andey spent 13 hours on this total and Carlton spent 15 hours in total.
- Added some new lighting effects and post-processing effects to help with the look of the game. These are needing some optimization still. This took James about 2 hours.


## Challenges
- We required a rewrite of the camera system to support the network infrastructure for the project. Furthermore, in order to make the dynamic camera viewpoints, they needed this rewrite as well. That was a challenge that sunk several hours of time unfortunately.
- The initial version of the terrain viability verification code used recursion to determine the playable area, however if the area was too large it would cause a stack overflow. Because of this we needed to convert the code to be done using loops instead which saved on memory

## Changes
- Moved away from a server authoritative schema. This would have needlessly complicated our game and we decided that players cheating wasn’t a major concern since it’s a game meant to be played with your friends in a pseudo-local style. If they want to screw with the physics or the game, that’s their prerogative.

## The Next 3 Weeks:
### Andey:
- Implement State Machine abstraction for cryptid behavior
- Build AI “robot”
- Build AI “sensors”
- Connect sensors to state machine

### Brandon:
- Implement other resource node generation like trees, rocks and grass all with different but realistic distributions.
- Implement base camp placement

### Carlton:
- Finish network connections 
- Work on player spawn with Brandon
- Start work on object interactions
- Inventory system/crafting
- Work on menus with James
- Sound integration

### James:
- Fix Camera Occlusion (rendering only what is needed)
- Add Grass that reacts to player movement
- Make four player characters and Sasquatch
- Inventory System/crafting/menus with Carlton
- Create 3D Objects such as Trees, Food Sources, Tent, Fire, Boulders 
- Aid in building sensors with Andey
- Research Water Mechanics

## Confidence Levels
| Name | Confidence |
|---|---|
|Andey|3|
|Brandon|4|
|Carlton|5|
|James|4|
|Average|4|

