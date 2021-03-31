# Status Update 3

This past sprint has brought us to the make or break point. We spent the time getting all of the final infrastructure systems into their place in the AI toolchain and are poised to see them coming together. The network infrastructure has been built to a point where all of the simple bugs have been ironed out, much of the basic behaviour is consistent across the network, but some key pieces still have yet to be brought to a working state. Lots of art has been completed, and only a few pieces remain to be modeled.

State layer of AI has been built, and possible behaviours have been abstractly implemented
Terrain generation refined
Multiplayer networking bugs fixed

Our quantifiable metrics were to get the state machine created, finish the menu system, develop multiple art assets to completion, and finish networked play.       

## Successes
- Code was added to procedurally pick a location on the map and generate a campsite there which will serve as the spawn point for the players along with adding in the playing spawning mechanics. Brandon and Carlton worked on this and took 2 hours.
- Code was added to place resource objects on the map including rocks, grass, and trees. Brandon implemented this in 4 hours.
- State machine abstraction and behaviour frameworks were developed. This took Andey about 8 hours
- Plans for the state machine transition system began being developed. Andey spent 3 hours on this.
- The key exchange protocol was worked on for a few hours. It has yet to reach the functional state. Andey spent 2 hours on it.
- A super basic AI prefab was created. Work began to add this to the networking infrastructure. Andey spent 2 hours on this.
- Host and Client and now move separately and have their own camera Carlton.5 hours
- James spent 12 hours drawing and creating four custom 2D character models and running animations.
- It took James around 2 hours to create an inventory GUI, crafting GUI, and a gameplay UI to be used as the player runs around.
- James spent some time learning blender and then created a 3D Tree prefab in Blender. This took roughly 6 hours.

## Challenges

- Networking behaviour was and has been very wacky and sometimes unpredictable causing more time to be devoted to ironing out the player movement.
- When multiple players are in the same game, the server is unable to give each of their terrain generators the same seed causing each player to see a different map. Brandon and Carlton spent 3 hours trying to fix this but were unable to find a solution as of yet.

- The key exchange protocol is not playing well with the network components. Some initial investigation and discussion amongst the team might suggest that the problem causing the difference in map seed might also be causing the issues in synchronizing and updating the weights.
- Unity’s implementation/reference to C# is not as feature rich as the .NET C# leading to some challenges when trying to do more “backend” related tasks for bootstrapping the AI. 

## Changes
- The AI is largely moving away from the basic “sensor” based approach discussed last week and going to a Behaviour based approach. While this will likely have little impact to the final outcomes of the AI, it simplified the state machine transitions greatly by shifting the requirements from the sensors to the atomic behaviour components.

## The Next 3 Weeks:
### Andey:
- Finish key exchange protocol (week 1)
- Finish the transition high level abstraction (week 2)
- Build first behaviour controllers (week 1)
- Work with Brandon to build subsequent behaviour controllers (week 2)
- Build regression learning system (week 3)

### Brandon:
- Integrate completed game objects into the terrain generation code whenever they become available.
- Work with Andey to implement behavior controllers

### Carlton:
- Crafting and Inventory System 
- Fix Camera Occlusion (rendering only what is needed)
- World and Object interaction
- Menus
- Work on menus with James
- Sound integration
- Bug Fixing 

### James:
- Create Title Screen Art
- Make Host/Find Game Menu Art
- Make 3D Tent, Fire, Grass, Rocks
- Aid in Sound Design
- Make Sasquatch Sprite
- Create Animated Water texture and put on top of water tile
- Make extra custom 3D items from list
- Work on inventory system with Carlton


## Confidence Levels
| Name | Confidence |
|---|---|
|Andey|3|
|Brandon|3|
|Carlton|4|
|James|4|
|Average|3.5|


