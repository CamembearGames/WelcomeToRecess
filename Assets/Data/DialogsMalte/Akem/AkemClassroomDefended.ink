//Akem Classroom

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL ChangeRelashionship(name, amount)
EXTERNAL AddEndYearInteraction(interactionnumber)
EXTERNAL WateringAcknowledge()
EXTERNAL GoBackToRecess()



VAR AkemFriendship = 5
VAR talkAlready = false
VAR miniGameWin = true
VAR TimeSlots = 0 
VAR HasWatered = false

    //If the player was nice 
(Akem nickt dir zu, als du dich dem Tisch näherst.)#Thoughts
(Er hält dir ein knuspriges Stück Brot entgegen.)#Thoughts
(Im Gegensatz zu dem, was Emma gesagt hat, kannst du nichts ausergewöhnliches riechen.)#Thoughts
Möchstest du essen? Das ist Brot. 
(Du nimmst die Naan-Hälfte entgegen.)
(Es erinnert dich an einen gebratenen Wrap. Schwarze Samen, die du nicht direkt einordnen kannst, bedecken die Oberfläche.)#Thoughts
(Du beißt hinein.)#Thoughts
(Im Inneren schmeckst du eine Mischung aus Joghurt, Huhn und Gewürzen.)#Thoughts
(Es ist eine echte Geschmacksexplosion in deinem Mund.)#Thoughts
Schön, dass dir es schmeckt! #Player
             ~ AkemFriendship = AkemFriendship + 1       
             ~ AddEndYearInteraction(0)
             ~ UpdateRelashionship("Akem", AkemFriendship)

{AkemFriendship < 4:

(Akem scheint nicht glücklich darüber zu sein, dass du dich zu ihm gesetzt hast.)#Thoughts
(Er wirft dir einen kurzen Blick zu, richtet dann aber seine Aufmerksamkeit auf seinen Schreibblock.)#Thoughts
(Er macht sich nicht die Mühe, ein Gespräch mit dir anzufangen.)#Thoughts
     -> enddialog 
 
 
    - else:

             
             

    (Akem schenkt dir ein Lächeln, während du dich neben ihn setzt.)#Thoughts
    (Während ihr auf den Lehrer wartet, deutet Akem auf die Tafel.)#Thoughts
    (Von der letzten Stunde steht noch eine Sammlung von alten, deutschen Reality-TV Shows angeschrieben.)#Thoughts
    Deut-schland-sucht-den-Superstar? Wir haben auch so etwas zu Hause.
    Bei uns heißt AfghanStar. Ist sehr beliebt. Mein أب wollte singen, aber nein. Nicht möglich.
        * Mochtest du die Serie?#Player
            Nein... Ja... Schwierig.
            Ich interessiere mich nicht für Singen. Aber... 
            Ich gucke Television mit Familie. Mit أب und مومياء.  Zusammen. Jeden Tag.
            Anders als Schule. Anders als Welt. Alles...
            Schön. Kein Streit. Schöne Leute. Sie haben Spaß.
            ...
            Ich finde es schade, dass es kein AfghanStar in Deutschland gibt.
            Ich würde gerne AfghanStar gucken.
             ~ AkemFriendship = AkemFriendship + 1  
              ~ AddEndYearInteraction(0)
             ~ UpdateRelashionship("Akem", AkemFriendship)
                -> enddialog
      
        * Wolltest du selbst teilnehmen?#Player
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