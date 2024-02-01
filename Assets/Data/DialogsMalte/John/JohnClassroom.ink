//Jon Classroom

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL GoBackToRecess()

VAR talkAlready = false
VAR JohnFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 


{JohnFriendship < 4:

(Jon scheint nicht glücklich darüber zu sein, dass du dich zu ihm gesetzt hast. )
Als ob Unterricht nicht schlimm genug wäre...
(Er scheint nicht weiter mit dir sprechen zu wollen.)
     -> enddialog
    - else:

    Yo, schön dich als Sitznachbarn zu haben. So muss ich Mathe nicht alleine durchstehen.
    Ich versteh einfach nicht, weshalb Frau Hasenbach das alles so kompliziert erklären muss. 
    Immer heißt es nur auswendig lernen, auswendig lernen, auswendig lernen. 
    Wenn ich noch einmal den verdammten Strahlensatz aufsagen muss, schieße ich mit einem Fußball die Fenster ein.
        * Frau Hasenbach könnte sich echt mehr anstrengen
            Ich glaube nicht mal, dass es an ihr liegt. Sie macht ja auch nur ihren Job.
             Halt nur ziemlich mies.
             Ey, warum muss das alles so kompilziert sein?
                -> enddialog
      
        * Gemeinsam schaffen wir das schon
             Voll, du hast recht. 
            Wenn ich irgendwas nicht weiß, frag ich einfach dich.
            Und wenn du etwas brauchst, werde ich dir helfen.
            Wenn ich das kann.
             So wird das ganze um einiges eträglicher. Danke, Mann.
             ~ JohnFriendship = JohnFriendship + 1          
             ~ UpdateRelashionship("John", JohnFriendship)
              -> enddialog
     
    
        * Später werden wir den Strahlensatz eh nicht brauchen
            Ja, wahrscheinlich nicht.
            Bei richtig vielem denke ich mir so "Alter, wo für?"
            Aber weißt du - ich würde es trotzdem gerne verstehen. Ich will nicht nur in Sport gute Noten haben. 
            Auch wenn das heißt, irgendwelche Matheformeln rauf und runter zu quaseln. 
            Mann, das kann ja eine spaßige Stunde werden...
                -> enddialog
       
}

=== enddialog ===
    ~ GoBackToRecess()
    -> DONE 
-> END