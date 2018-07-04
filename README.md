# Gonity-Example-Survival-Shooter

The purpose of this example is to showcase the main features of **Gonity**
([https://github.com/tromagon/Gonity](https://github.com/tromagon/Gonity)).

These features include the use of an Entity Component System, the View Mediator design pattern, Injection and Event handling. For more information please check out the [wiki page](https://github.com/tromagon/Gonity/wiki).

On top of it, one approach adopted here is to **"overthrow the MonoBehaviour tyranny"** as described by Richard Fine in his talk from [Unite Europe 2016](https://unity3d.com/learn/tutorials/topics/scripting/overthrowing-monobehaviour-tyranny-glorious-scriptableobject). In the Data and DataObjects folders, you will find an example on how to control game data through ScriptableObjects instead of MonoBehaviours. Using this method, it is now possible to change data during play mode and keep these changes intact.

**Note: the Gonity folder is used as a github submodule pointing to the main Gonity repository. Thus, when cloning, or downloading this repository as a Zip file, the folder will be empty. The easiest way to fix this is to use GitHub Desktop which will automatically fetch all necessary files from the Gonity repository.**

Alternatively, you can also savagely fill the content of the folder by manually grabbing the files from the Gonity Repository.
