//Akem Classroom

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL GoBackToRecess()



VAR AkemFriendship = 5
VAR talkAlready = false
VAR miniGameWin = true
VAR TimeSlots = 0 


{AkemFriendship < 4:

(Akem scheint nicht glücklich darüber zu sein, dass du dich zu ihm gesetzt hast.)
(Er wirft dir einen kurzen Blick zu, richtet dann aber seine Aufmerksamkeit auf seinen Schreibblock.)
(Er macht sich nicht die Mühe, ein Gespräch mit dir anzufangen.)
     -> enddialog 
 
 
    - else:

    (Akem schenkt dir ein Lächeln, während du dich neben ihn setzt.)
    (Während ihr auf den Lehrer wartet, deutet Akem auf die Tafel.)
    (Von der letzten Stunde steht noch eine Sammlung von alten, deutschen Reality-TV Shows angeschrieben.)
    Deut-schland-sucht-den-Superstar? Wir haben auch so etwas zu Hause.
    Bei uns heißt AfghanStar. Ist sehr beliebt. Mein أب wollte singen, aber nein. Nicht möglich.
        * Mochtest du die Serie?
            Nein... Ja... Schwierig.
            Ich interessiere mich nicht für Singen. Aber... 
            Ich gucke Television mit Familie. Mit أب und مومياء.  Zusammen. Jeden Tag.
            Anders als Schule. Anders als Welt. Alles...
            Schön. Kein Streit. Schöne Leute. Sie haben Spaß.
            ...
            Ich finde es schade, dass es kein AfghanStar in Deutschland gibt.
            Ich würde gerne AfghanStar gucken.
             ~ AkemFriendship = AkemFriendship + 1          
             ~ UpdateRelashionship("Akem", AkemFriendship)
                -> enddialog
      
        * Wolltest du selbst teilnehmen?
            Nein, ich nicht singen. 
            Ich... ähmm...
            Mein أب und meine مومياء singen. Können gut singen.
            Ich nicht. 
            Und... 
            Jetzt nicht möglich, teilnehmen. 
            ...
            Später. 
              -> enddialog

}

=== enddialog ===
    ~ GoBackToRecess()
    -> DONE 