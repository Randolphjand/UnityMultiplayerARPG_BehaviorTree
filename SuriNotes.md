This behavior tree is now almost fully integrated into the Kit.  This will no longer function without the kit.  It does make it more professional that this AI is just for your Kit.  You can increase the Kit integration now by possibly eliminating the MonsterActivityComponent and incorporating it into the BehaviorTreeRunner.

One important reason for this to be something that is on your GitHub for people to download is that I discovered after I messed up the original kit that the Kiwi Coder's site will only allow one download for each email address.

The Kiwi Coder stopped doing anything on Youtube after the videos on the behavior tree which makes it possible that he might not support that site in the future making it impossible for Kit owners to get the base behavior tree.

Will you please make it so we can control NPC's with the BT.  It would be great if we can make things like a village with NPC's following paths and stopping to do things.  Make NPC's harvest and drop off and go home and go back out to work.  Hunger, thirst, and tiredness should be made available to both Monsters and NPC's.

Just please make some nodes to show how to control the NPC's. 

I am dedicated to making the AI easy to use for non-coders.  I want to create a lot of nodes for people to just be able to use to make the AI do complicated things.  I am not a good enough coder to figure out how to do all of these things which is why I ask you to show in a few examples how to control NPC's and Monsters completely.

Utility AI can be integrated into BT's and I would do it if I could figure out how to add it.  If you would show me how to start, I will finish it.  It seems like Kiwi's InterruptSelector node may have been written to allow the possibility.
  
I have included a few files from Fashta's integration of Behavior Designer which I purchased from him. I would like to integrate the functionality into the Kit BT like he did for Behavior Designer.
There is a folder with some utilities he created that might be useful.

 
Can you make it so that we can create sub-trees that can be re-used.  Things like different battle trees is a common thing in many BT's.

Is there a way to make the trees only run if a PC is in sight of the controlled NPC or Monster?


 

  CHANGELOG

changed folder name
moved folders
added conditional node
added colorable log messages
added minimap
added minimap toolbar button
added instructions if user chooses to open bt editor from top menu
blackboard min height to 30% so it is always visible even if editor window is reduced in size
changed ability to reduce size of BT editor window to make it smaller
combined the two partial blackboards to one
Moved Create Behavior Tree Editor into MMORPG KIT Menu
Changed all Monster Nodes to Action nodes and moved them to actions folder
Changed action node names

added //commented code for using ticks for Update or //commented code for using a coroutine.
I have also //commented code for running the tree asnychronously using C# 
I have codeed the tree for running asnychronously using UniTask but I am not sure if it is correct or not.  Please check it. 
The ticks and coroutine or asnychronous functionality is for performance and slightly varied AI look.

slight changes to script templates
changed namespace from MultiplayerARPG.KiwiCoderBT to MultiplayerARPG.KitBT
changed namespace from TheKiwiCoder to KitBehaviorTree
change MonsterActivityComponentKiwiCoderBT to MonsterActivityComponentKitBT
added nodes that send and respond to Unity Action and UNITYEVENT

