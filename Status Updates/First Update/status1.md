# First Status Update
Over the past three weeks, we have gotten quite a bit done! At the start of the three weeks we planned to switch over from a fully 2D environment to a 3D environment. This decision was made as we already were encountering a lot of difficulties with not being able to reference depth in the game. The past three weeks were dedicated to getting us back to where we were previously with the project when everything was 2D. This primary goal has been accomplished and put us into a position to begin the super fun parts of creating the actual game objects, drawn environment, and much more. To list just a few of the tasks that have been completed:

# Project shift from 2D procedurally generated map to a now 3D procedurally generated map.
Player movement, camera movement and animations have been completed.
AI Design document was completed. (Andey - 6 hours)
Our biggest quantifiable metric was to get the project 100% back to where it was previously.

# Successes
3D environment is now procedurally generated
Terrain generation code from the 2D environment was successfully converted to a 3D environment in a collaborative effort between James and Brandon which took 2 hours.
Brandon also changed the generation code to create a wall around the border of the map taking an additional 1 hour to implement.
James implemented the creation of a clear block that generates over all water blocks to prevent the players from walking on water and a block spawning under plateaus to distinguish from regular grass blocks. This took around 1 hour.

# Camera Controller
The Cameras location is now decided by player input of q or e and the current position of the camera. The camera has four positions that are all possible sides of the player. Whenever this is changed the players movement is based on it. This took James about 5 hours to implement.

# Player Character Controller and Animation
Correct player movement animations were added to now show the players direction of movement and their movement behavior. This took Carlton 3 hours to implement.
The player movement is now based on the camera's position. 8 Directional movement is possible and decided by creating a new Vector based off of the current camera’s position vector and the direction pushed by the player. This took James about 6 hours to implement.
Collisions are working for the most part. The collision with the player and some objects have glitches that occur. This took Carlton and James  about one hour to create.
-      All of our implementations were pre-decided and ended up working as planned. This planning stage we have in our monday meetings allowed us to not have to repeatedly try different tactics.
-      Base game is now ready for further developments

# Challenges
Using Blend Trees instead of Unity’s default animation states allowed for easier and smoother animations. Additionally, it fixed previous bugs with player movement.
Creating a custom character controller and camera controller where the state of the camera controls the direction of character movement and rotation.
Collisions are currently having an issue where our player’s collider is phasing slightly into the 3D blocks and then applying a constant force sliding our character.
Player movement bug fixes so that if opposite keys are pressed there is no movement.
Losing a team member and re-organizing roles to fit the smaller team
Unity Versioning issues and code sync caused some issues on one of the team member’s machines, slowing down progress due to unavoidable technical problems

# Changes
	So far no changes are anticipated from our plan. The major challenges are coming in the next sprint, so any major re-evaluations will likely come in our retrospective after the next three weeks. Our major concerns going forward are the AI implementation, the cave system, and the camera animation.

# The Next 3 Weeks:
##  Andey:
Add host/server/client networking to introduce multiplayer
Create skeleton of cryptid game object to give the AI an interface to the game world
Build the basic AI mechanisms outlined in the AI design document
Outline what “sensors” need to be added to the Cryptid game object

##  Brandon:
Edit the terrain generation code to automatically connect any alcoves of grass that are separated from the rest of the map by creating a path through the plateaus that separate them.
Create code for distributing collectable resources on the map.
Look into how caves can be generated procedurally and realistically.

##  Carlton:
Fix Collision corner bug.
Work on the transition/animation of camera rotations.
Add Player name labels.
Begin work on functional player aspects such as crafting and resource gathering.

##  James:
Assist in the development of camera rotation animation
Work on the creation of custom 3D models for grass, plateaus, water, oil, and caves, crafting, resources.
Create Assets and UI systems for player interaction systems.
Form and implement solutions for 3D art generation such as moving grass.
Assist in alcove solution implementation.

# Confidence Levels
Andey 4
Brandon 5
Carlton 5
James 4

Average
4.5

