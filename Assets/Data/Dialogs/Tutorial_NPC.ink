EXTERNAL ContinueTutorial()

-> main

=== main ===

Hello, are you the new student ?

 * [Yes (start tutorial)]
    Ok then you should follow me
     ** [Ok]
        ~ ContinueTutorial()
        -> DONE
        
     ** [No]
        -> DONE
 * [No, I was here last year]
    Ok then I will see you around
    -> DONE

-> END

== function ContinueTutorial() ==
~ return 1
