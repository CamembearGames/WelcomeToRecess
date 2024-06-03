//Janett Intro

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL ChangeRelashionship(name, amount)
EXTERNAL AddEndYearInteraction(interactionnumber)
EXTERNAL WateringAcknowledge()
EXTERNAL ReAddDialog()

VAR talkAlready = false
VAR JanettFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 
VAR HasWatered = false

{HasWatered:
        ~ WateringAcknowledge()
        ~ JanettFriendship = JanettFriendship + 1
        ~ UpdateRelashionship("Janett", JanettFriendship)
        Ah, ich sehe, du hast die Pflanzen gegossen. Vielen Dank dafür. Ich liebe Pflanzen.
-else:
    (Janett scheint in Gedanken versunken, als sie die trockenen Pflanzen betrachtet.) #Thoughts 
}

{TimeSlots < 2: 
    {talkAlready:
        War schön mit dir zu reden. Wollen wir das nächste Pause wiederholen?
        ~ ReAddDialog()
        -> END
    
    -else:
        -> Greeting
    }

-else:
    Die Pause ist vorbei, wird sollten los zum Unterricht. 
    ~ ReAddDialog()
    -> END
}


== Greeting ==
 
{ JanettFriendship: 
- 1: Passt auf, Leute. Loseralarm. 
- 2: Hey Mädels, Idiot auf zwölf Uhr.
- 3: Kannst du das glauben? Hm, ist was? Ach, nein, ist nicht wichtig. 
- 4: Tja, wen haben wir denn da?
- 5: Hallöchen, siehst du schick aus.
- 6: Dein Referat gestern war richtig toll.
- 7: Na du, willst du dich nicht zu uns setzen?
- 8: Psst, willst du wissen, wer heimlich auf Tim steht?
- 9: Mädels, macht mal Platz für ihn/sie- 
- 10: Hey, hast du Lust, nach der Schule mit mir ins Kino zu gehen?
}

* [Sprechen]
    ~ talkAlready = true
    ~ UpdateTalkAlready("Janett", talkAlready)
    ~ TimeSlots = TimeSlots + 1
    -> JanettTalking
    * [Stapelstecker spielen] 
        {TimeSlots == 0: 
            Entschuldigung. Dies wird erst in der nächsten Version möglich sein.
        -> Greeting
        -else: 
        Ich denke nicht, dass wir genügend Zeit dafür haben.
        -> Greeting
    }

* [Verlassen]
    ~ ReAddDialog()
    -> END

== JanettTalking ==

{ JanettFriendship < 4:
Sorry, ich und meine Mädels sind gerade beschäftigt. Das könnte noch eine Weile dauern, also lass uns erstmal in Ruhe, ja?
    -> END
- else:
Setz dich hin, setzt dich hin! Schön, dich wieder bei uns zu haben.
Findest du nicht auch, dass das Wetter heute einfach großartig ist? Perfekt, um mit seinen Besties ein bisschen Zeit draußen zu verbringen.
Wir hatten überlegt, ob wir nachher noch als Gruppe etwas in die Stadt wollen? Hättest du vielleicht Lust, ein bisschen mitzukommen? Wird bestimmt spaßig!
Aber vorher musst du uns noch bei einer wichtigen Frage helfen. 
Wir haben gerade überlegt, was die beste Jahreszeit ist. 
Ich liebe ja das schöne Wetter im Sommer und im Frühling, aber die Straßen sind so hübsch im Herbst und im Winter kann man so tolle Sachen unternehmen.
Was ist deine Lieblingsjahreszeit?
    * [Frühling]  
    Das kann ich sehr gut nachvollziehen. Die frische Luft, die Blumen, das Leben kommt zurück in die Straßen - der Frühling ist einfach super.
        ~ JanettFriendship = JanettFriendship + 1
        ~ UpdateRelashionship("Janett", JanettFriendship)
        ~ UseTimeSlot(TimeSlots)
    -> END
    
    * [Sommer]  
    Nicht wahr? Wer mag den Sommer nicht? Man kann baden gehen, Eis essen und danach in den Sommer-Blockbuster im Kino. 
    Ich habe auch schon an ein oder zwei Demonstrationen teilgenommen - die im Sommer sind immer die besten! 
        ~ JanettFriendship = JanettFriendship + 1
        ~ UpdateRelashionship("Janett", JanettFriendship)
        ~ UseTimeSlot(TimeSlots)
    -> END
    
    * [Herbst]  
    Ich mag den Herbst auch voll gerne.
    Besonders all die schönen Bilder auf Social Media. Die Blätter, die Kleidung, diese heimelige Gefühl... 
    Ich kann kaum auf den Herbst dieses Jahr warten. 
        ~ JanettFriendship = JanettFriendship + 1
        ~ UpdateRelashionship("Janett", JanettFriendship)
        ~ UseTimeSlot(TimeSlots)
    -> END

}
