EXTERNAL PerformActivity()
EXTERNAL ChangeRelashionship(name, value)
EXTERNAL GoBackToClass()
EXTERNAL GoBackToRecess()

-> main

=== main ===

Hey, are you looking forward to this class ?

    * [Yes, I love math]
        Huh not me. Wish I was outdoors.
        ~ ChangeRelashionship("John", -1)
        -> enddialog
    * [No, I just want to go out and play sports]
        Me too, I wish we were ourdoors now.
        ~ ChangeRelashionship("John", 1)
        -> enddialog


=== enddialog ===
    ~ GoBackToRecess()
    -> DONE 
-> END


== function PerformActivity() ==
~ return 1

== function ChangeRelashionship(name, value) ==
~ return 1

== function GoBackToClass() ==
~ return 1

== function GoBackToRecess() ==
~ return 1