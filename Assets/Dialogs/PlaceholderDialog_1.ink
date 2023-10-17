EXTERNAL GoToClassroom(room)

-> main

=== main ===

Test Dialog

    * [Go to class]
        Going to class
        ~ GoToClassroom("Test Room")
        -> DONE
    * [Stay in recess]
        Staying here
        -> DONE

-> END

== function GoToClassroom(room) ==
~ return 1 // placeholder result
