//Janett Classroom2

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL GoBackToRecess()

VAR talkAlready = false
VAR JanettFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 

{JanettFriendship < 4:

(Janett scheint nicht glücklich darüber zu sein, dass du dich zu ihr gesetzt hast. )
Weg da, das ist Hedijes Platz.
(Janett scheint nicht weiter mit dir sprechen zu wollen.)
     -> enddialog
 
 
    - else:

    Psst! Hast du einen Moment?
    Ich und die Mädchen haben vor, eine kleine Willkommensparty für den Neuen zu schmeißen. Hast du schon Akem kennengelernt? Stiller Junge, aber Jon mag ihn.
    Wenn die Party zeitgemäß stattfinden soll, müssen wir aber ziemlich in die Puschen kommen. Hast du Lust, uns während der Stunde etwas zu unterstützen?
        * [Bei der Planung helfen] 
            Danke, du bist großartig.
            Kannst du diesen Zettel weitergeben? Und lass dich nicht vom Lehrer erwischen!
             ~ JanettFriendship = JanettFriendship + 1          
             ~ UpdateRelashionship("Janett", JanettFriendship)
              -> enddialog
     
    
        * [Im Unterricht aufpassen]  
            Wenn du meinst... 
            Sei aber zumindest kein Spielverderber und sag dem Lehrer nichts, ok? 
            ~ JanettFriendship = JanettFriendship - 1          
            ~ UpdateRelashionship("Janett", JanettFriendship)
                -> enddialog
}

=== enddialog ===
    ~ GoBackToRecess()
    -> DONE 