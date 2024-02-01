//Janett Classroom

EXTERNAL UpdateRelashionship(name, value)




VAR JanettFriendship = 5


{JanettFriendship < 4:

(Janett scheint nicht glücklich darüber zu sein, dass du dich zu ihr gesetzt hast. )
Weg da, das ist Hedijes Platz.
(Janett scheint nicht weiter mit dir sprechen zu wollen.)
     -> END
 
 
    - else:

    Na du? Eigentlich wollte ich bei meinen Mädchen sitzen, aber die können ja auf meine andere Seite.
    Dann können wir zwei Hübschen uns ein paar chillige Unterrichtsstunden gestalten.
    Aber ich bin neugierig: Warum hast du dich ausgerechnet neben mich gesetzt?
        * [Gerücht gehört, welches du umbedingt hören musst] Ich habe ein Gerücht gehört, das ich dir erzählen wollte
            Ah, schön. Dann lass mal hören. Das wird Hedije und Leonie bestimmt brennend interessieren. 
            Es ist aber nichts gemeines, oder?
                -> END
      
        * [Anbieten, sie abschreiben zu lassen] Ich dachte, ich könnte dir etwas bei den Hausaufgaben helfen.
            Entschuldige, bitte?
            ...
            Das ist ja ganz nett gemeint, aber ich denke, ich krieg das schon alleine hin.
            Danke.
             ~ JanettFriendship = JanettFriendship - 1          
             ~ UpdateRelashionship("Janett", JanettFriendship)
              -> END
     
    
        * [Sie um Hilfe bei Schularbeit helfen] Könntest du mir etwas in Deutsch helfen? Ich verstehe diese Aufgabe nicht. 
            Oh, natürlich, gerne doch. 
            Wobei brauchst du Hilfe? Wenn du willst, kannst du mit mir und Jon zusammen lernen.
            ~ JanettFriendship = JanettFriendship + 1          
            ~ UpdateRelashionship("Janett", JanettFriendship)
                -> END
       
}