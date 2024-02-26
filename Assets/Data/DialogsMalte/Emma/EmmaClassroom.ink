//Emma Classroom

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL GoBackToRecess()



VAR EmmaFriendship = 5
VAR talkAlready = false
VAR miniGameWin = true
VAR TimeSlots = 0 


{EmmaFriendship < 4:

(Emma scheint nicht glücklich darüber zu sein, dass du dich zu ihr gesetzt hast. )
Eins sei dir gesagt: Wenn du auch nur daran denkst mich während des Unterrichts abzulenken, werde ich *persönlich* dafür sorgen, dass der Elternrat davon erfährt.
(Sie scheint nicht weiter mit dir sprechen zu wollen.)
     -> enddialog 
 
 
    - else:

    Wie es scheint, werden wir die nächsten Wochen zusammensitzen. Ich hoffe auch gute Kooperation in den Partnerarbeiten.
    Bitte tue mir den Gefallen und sei still während des Unterrichts, ja? Ich habe nicht vor, irgendwelche Privatgespräche zu führen.
    Frau Hasenbach mag das gar nicht. 
    Und jetzt, psst, sie fängt gleich an. 
        * Schweigen
            ...
             ~ EmmaFriendship = EmmaFriendship - 1          
             ~ UpdateRelashionship("Emma", EmmaFriendship)
                -> enddialog
      
        * Enspann dich, es geht erst in fünf Minuten los
            Inkorrekt. Das Läuten der Glocke beendet die Pause.
            Ruhe jetzt.
             ~ EmmaFriendship = EmmaFriendship - 1          
             ~ UpdateRelashionship("Emma", EmmaFriendship)
              -> enddialog

}

=== enddialog ===
    ~ GoBackToRecess()
    -> DONE 