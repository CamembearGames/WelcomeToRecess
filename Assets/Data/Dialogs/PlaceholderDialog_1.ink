EXTERNAL PerformActivity()
EXTERNAL ChangeRelashionship(name, value)


-> main

=== main ===

Do you want to play Ping-Pong with me ?

    * [Yes]
        Great I like you more.
        ~ ChangeRelashionship("Jon", 1)
        -> DONE
    * [No]
        Hum, not sure I like you
        ~ ChangeRelashionship("Jon", 1)
        -> DONE
    * [Leave]
        Bye
        -> DONE

-> END

== function PerformActivity() ==
~ return 1

== function ChangeRelashionship(name,value) ==
~ return 1
