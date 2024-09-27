Support: Discord (https://discord.gg/K88zmyuZFD)

Online Documentation: https://inabstudios.gitbook.io/world-dissolve/
You can find more specific information on the asset functionality and setup process.

### IMPORTING

After downloading the asset, import the appropriate .unitypackage file based on your Unity version and SRP (either Built-in.unitypackage, URP.unitypackage, or HDRP.unitypackage).

### LEARN

The asset includes 14 demo scenes that showcase how to use its features. Explore the Demo Scenes to understand how everything works in action. You can find them in "INab Studio/World Dissolve/Core URP|HDRP|Built-In/Example Scenes".
World Dissolve gives you the ability to dissolve objects and see through them, offering multiple features for easy customization and integration into your project. 

World Dissolve Setup:

Add the WorldDissolve.cs script to any game object in your scene. 
Add materials of all game objects that should be affected by the World Dissolve.
Create a new game object and add Mask.cs script. 
Include the newly created mask in the Masks List.
Turn on the Control Material Properties option.
Decide which Dissolve Type you want to use
Select all materials from the previous steps.
Change their shader to World Dissolve Burn.
Turn on Alpha Clipping and adjust the threshold.
Now click the Refresh button.
And everything is properly set up.

See Through Options:

The asset offers various options for see-through effects using custom-created effects, each with its own benefits and suitability for different scenarios. These effects can be freely combined as they are not dependent on each other.
Objects Fade: Fades out entire game objects when they are between the camera and the player.
Other: Provides additional options to create more customized see-through effects with World Dissolve by manipulating mask transforms when an object is detected between the camera and the player.
Distance Fade: Fades parts of objects based on their distance from the camera.
Round See Through: Creates a round circular see-through effect through walls and objects.

Every feature has it's own example scene so you could easily see how it can be configured.

See Online Documentation for more specific tutorials.

