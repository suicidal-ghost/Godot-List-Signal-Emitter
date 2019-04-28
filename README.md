# Godot-List-Signal-Emitter
Testing efficiency of  using signals to pass list elements incrementally.
This repo contains code for testing a C# signal emitter in a foreach loop in the godot engine.
Godot does not currently fully support lists and it is not currently possible to get or set lists that are on different nodes.
One solution to this issue is to pass the elements one at time using the signal system
The caviat to this solution is that the elements must be or inherit from godot objects

The code here is to test the efficiancy this solution.
The parent code goes in a spatial node with a child node attached to it with the child node code. 
running the game out of the editor should cause the code to execute with no input required.
After the time specified in the parent node variable: "TestTime" passes, the game will self terminate and
  print the results to the dubug log in the editor.
There are directions in the comments of the parent and child for disabling the emitter. This allows for 
  a control test to be run where no signals are emitted.
As programmed, the application will throw an exception upon closing. I never fixed this because it does not hinder testing
