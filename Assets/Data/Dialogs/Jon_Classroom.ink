EXTERNAL PerformActivity()
EXTERNAL ChangeRelashionship(name, value)

-> main

=== main ===

Hey, are you looking forward to this class ?

    * [Yes, I love math]
        Huh not me. Wish I was outdoors.
        ~ ChangeRelashionship("Jon", -1)
        -> DONE
    * [No, I just want to go out and play sports]
        Me too, I wish we were ourdoors now.
        ~ ChangeRelashionship("Jon", 1)
        -> DONE
    * [Leave]
        Bye
        -> DONE

-> END

== function PerformActivity() ==
~ return 1

== function ChangeRelashionship(name, value) ==
~ return 1
