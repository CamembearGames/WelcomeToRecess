EXTERNAL PerformActivity()
EXTERNAL ChangeRelashionship(value)

-> main

=== main ===

Do you want to play Ping-Pong with me ?

    * [Yes]
        Great I like you more.
        ~ ChangeRelashionship(1)
        -> DONE
    * [No]
        Hum, not sure I like you
        ~ ChangeRelashionship(-1)
        -> DONE
    * [Leave]
        Bye
        -> DONE

-> END

== function PerformActivity() ==
~ return 1

== function ChangeRelashionship(value) ==
~ return 1
