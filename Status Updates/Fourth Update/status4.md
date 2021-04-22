# Status Update 4

This sprint has been very productive for us as a team! Our time was spent refining a lot of our in-place systems and building up or AI. Behavior controllers have been built and our crafting system has been implemented. At this point all of the groundwork has been laid out and we will just be finishing our Artificial Intelligence and polishing up our already in place systems.

Key exchange protocol has been built. Artificial Intelligence Spawning has been created. Generation of resources has been refined. Lots of art has been completed.

Our quantifiable metrics were to get the Key exchange protocol done, finish the crafting system, get near completion on art assets, and finish networked play.

## Successes
-	Camera and Player Movement touched up and refined. (1hr- Carlton and James)
-	Player Animations have been added in.(2hrs- Carlton).
-	Working on the inventory system (not finished). (6 hrs- Carlton).
-	Recorded Sounds. (2 hrs- Carlton).
-	Created AISpawnSystem. (2hrs- Carlton).
-	Wrote the key exchange protocol for sharing and combining weights and biases for the neural network (6 hrs - Andey)
-	Wrote initial wandering behavior script (2 hrs - Andey)
-	Studies the wandering behavior script to better understand how to create the rest and also implement a basic sleep behavior (2 hrs -Brandon)
-	Built technical PoC for the neural network/transition function (12 hrs - Andey)
-	Performed research and began architecting regression system (2 hrs - Andey)
-	Added the Sasquatch spawn location to always be near the opposite side of the map from the camp so it is never next to the camp or inside a wall. (1 hr- Brandon, Carlton)
-	Created Sprites for beartrap, net, lantern, sticky tar trap, bundle of sticks, hatchet, rocks, metal, sasquatch (7 hours - James).
-	Created Sprites for tent, tree, Big Rocks, Ore (2 hours - James). 
-	Fixed Chat Issue and Camera Issue (2 hours - James)

## Challenges

-	There were some obvious challenges presented by importing 3D models such as the time to create and the learning curve of blender. Along with this it caused major problems with our players FPS and lag. So it was decided to just move to all 2D sprites for interactables as we already have a billboarding script in place so it will be easier to implement and cause less strain on the system.

-	Time management, with other classes coming to a close as well we each have a lot of other school work to do.


## Changes
- Moved from 3D objects inside the world environment to 2D sprites with the billboarding script attached.

## Final Sprint:
### Andey:
- Migrate Neural Network Transition system into game
- Finish regression learning model
- Build key distribution protocol

### Brandon:
- Integrate completed game objects into the terrain generation code whenever they become available.
- Work with Andey to implement additional behavior controllers
- Add berry bush resource generation to existing resources

### Carlton:
-	Crafting and Inventory System 
-	World and Object interaction
-	Sound integration
-	Fix Camera Occlusion (rendering only what is needed)
-	Menus
-	Work on menus with James
-	Bug Fixing 

### James:
-	Create Berrie Bush Sprites
-	Create Title Screen
-	Create Find Game Menu Screen
-	Create Grass and Campfire Sprite
-	Finished 3D block textures
-	Bug fixing

## Confidence Levels
| Name | Confidence |
|---|---|
|Andey|3|
|Brandon|2|
|Carlton|4|
|James|3.5|
|Average|3.125|
