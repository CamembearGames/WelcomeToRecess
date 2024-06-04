//Janett Classroom

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL ChangeRelashionship(name, amount)
EXTERNAL AddEndYearInteraction(interactionnumber)
EXTERNAL WateringAcknowledge()
EXTERNAL GoBackToRecess()

VAR talkAlready = false
VAR JanettFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 
VAR HasWatered = false

{HasWatered:
        ~ WateringAcknowledge()
        Ah, ich sehe, du hast die Pflanzen gegossen. Vielen Dank dafür.
        
        ~ JanettFriendship = JanettFriendship + 1
        ~ UpdateRelashionship("Janett", JanettFriendship)
}


{JanettFriendship < 4:

(Janett scheint nicht glücklich darüber zu sein, dass du dich zu ihr gesetzt hast. ) #Player
Weg da, das ist Hedijes Platz.
(Janett scheint nicht weiter mit dir sprechen zu wollen.)#Player
     -> enddialog
 
 
    - else:

    Na du? Eigentlich wollte ich bei meinen Mädchen sitzen, aber die können ja auf meine andere Seite.
    Dann können wir zwei Hübschen uns ein paar chillige Unterrichtsstunden gestalten.
    Aber ich bin neugierig: Warum hast du dich ausgerechnet neben mich gesetzt?
        * [Gerücht gehört, welches du umbedingt hören musst] Ich habe ein Gerücht gehört, das ich dir erzählen wollte.#Player
            Ah, schön. Dann lass mal hören. Das wird Hedije und Leonie bestimmt brennend interessieren. 
            Es ist aber nichts gemeines, oder?
            ~ AddEndYearInteraction(0)
                -> enddialog
      
        * [Anbieten, sie abschreiben zu lassen] Ich dachte, ich könnte dir etwas bei den Hausaufgaben helfen. #Player
            Entschuldige, bitte?
            ...
            Das ist ja ganz nett gemeint, aber ich denke, ich krieg das schon alleine hin.
            Danke.
             ~ JanettFriendship = JanettFriendship - 1          
             ~ UpdateRelashionship("Janett", JanettFriendship)
             ~ AddEndYearInteraction(0)
              -> enddialog
     
    
        * [Sie um Hilfe bei Schularbeit helfen] Könntest du mir etwas in Deutsch helfen? Ich verstehe diese Aufgabe nicht. #Player
            Oh, natürlich, gerne doch. 
            Wobei brauchst du Hilfe? Wenn du willst, kannst du mit mir und Jon zusammen lernen.
            ~ JanettFriendship = JanettFriendship + 1          
            ~ UpdateRelashionship("Janett", JanettFriendship)
            ~ AddEndYearInteraction(0)
                -> enddialog
}

=== enddialog ===
    ~ GoBackToRecess()
    -> DONE 