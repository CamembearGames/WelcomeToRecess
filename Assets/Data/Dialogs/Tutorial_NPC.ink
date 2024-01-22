EXTERNAL ContinueTutorial()
EXTERNAL CancelTutorial()

-> main

=== main ===

Hello, are you the new student ?

 * [Yes (start tutorial)]
    Ok then you should follow me
    ~ ContinueTutorial()
    -> DONE
 * [No, I was here last year]
    Ok then I will see you around
    ~ CancelTutorial() 
    -> DONE

-> END

== function ContinueTutorial() ==
~ return 1
