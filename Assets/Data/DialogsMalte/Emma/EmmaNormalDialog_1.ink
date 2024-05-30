//Emma Intro
EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL ChangeRelashionship(name, amount)
EXTERNAL AddEndYearInteraction(interactionnumber)
EXTERNAL WateringAcknowledge()

VAR talkAlready = false
VAR EmmaFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 
VAR HasWatered = false

{TimeSlots < 2: 
    {talkAlready:
    Bitte störe mich nicht. Bevor es klingelt, will ich noch mit dieser Seite fertig werden. 
        -> END
    
    -else:
        -> Greeting
    }

-else:
    Die Schulglocke hat geschlagen. Ich habe keine Zeit für Gespräche, ich muss zum Klassenraum. 
    Keinesfalls werde ich wegen irgendwelchen Lapalien zu spät zum Unterricht kommen!
    -> END
}



== Greeting ==
 
{ EmmaFriendship:
- 1: Kretin...
- 2: Wie du nicht sitzen geblieben ist, bleibt mir schleierhaft.  Willst du etwas?
- 3: Im Gegensatz zu dir habe ich noch etwas sinnvolles zu tun.
- 4: Brauchst du was? Halt dich bitte kurz, ich sitze hier gerade an etwas echt wichtigem. 
- 5: Meinst du, wir schreiben gleich einen Test?
- 6: Was hattest du gerade bei Aufgabe Drei raus?
- 7: Wollen wir zusammen lernen?
- 8: Schön dich zu sehen! Hast du Lust, die Köpfe zusammenzustecken?
- 9: Du kommst gerade richtig. Mein Lernzettel für den Test ist fertig. Wollen wir ihn gemeinsam durch gehen?
- 10: Hör mal: Amicus optima vitae possessio. 
}
* [Sprechen] Woran denkst du gerade?
    ~ talkAlready = true
    ~ UpdateTalkAlready("Emma", talkAlready)
    -> EmmaTalking
* [Lernen] Wollen wir gemeinsam lernen?
    {TimeSlots == 0: 
    Entschuldigung. Dies wird erst in der nächsten Version möglich sein.

        -> Greeting
    -else: 
        Um den Lernstoff zufriendstellend zu wiederholen, bräuchten wir mehr Zeit. In dieser Pause schaffen wir das nicht.
        Du hättest echt früher mit dem Lernen anfangen sollen, so wie Hasenbach uns das gesagt hat. 
        -> Greeting
        }
* [Verlassen]
    -> END

== EmmaTalking ==

{ EmmaFriendship < 4:
Ich verzichte. Was, wenn uns ein Lehrer sieht und der dann denkt, ich bin so deliquent wie du? Kommt gar nicht in Frage.
    -> END
- else:
Ich freue mich richtig auf dieses Schuljahr. Ich habe mir gerade den Stoff für die achte Klasse angesehen und das sind so viele gute Themen dabei. Das daltonsche Atommodell, Eireifezyklus und Säkularisation - einfach so viel.
Worauf freust du dich am meisten?
    * Das Atommodell klingt interessant
    Nicht wahr? Ich kann es kaum erwarten. Selbstverständlich habe ich mir schon die entsprechenden Buchseiten durchgelesen. 
    Frau Hasenbach wird so stolz auf mich sein.
    ~ TimeSlots = TimeSlots + 1
    ~ UseTimeSlot(TimeSlots)
    ~ AddEndYearInteraction(0)
    -> END
    * Der Eireifezyklus könnte spannend werden
    Wirklich? Darauf freust du dich schon? 
    ...
    Ehrlich gesagt habe ich darauf am wenigsten Lust. Ich meine... das Thema interessiert mich schon wirklich. Aber wie werden die Jungen in unserer Klasse damit umgehen?
    Ich kann es schon vor mir hören. Das Lachen, die Hähme. die Witze über... du weißt schon. 
    Danke, dieses Bild werde ich heute nicht mehr aus dem Kopf bekommen. Das hat mir gerade noch gefehlt. Wenn ich deswegen den Test in den Satz setze, ist das *deine* Schuld. 
        ~ EmmaFriendship = EmmaFriendship - 1
        ~ UpdateRelashionship("Emma", EmmaFriendship)
        ~ TimeSlots = TimeSlots + 1
        ~ UseTimeSlot(TimeSlots)
        ~ AddEndYearInteraction(0)
    -> END
    * Was ist Säkularisation?
    Um ehrlich zu sein: Das habe ich mich auch gefragt. 
    Natürlich weiß ich, was Säkularisierung ist und im Wörterbuch habe ich selbstredend auch nachgeschlagen, aber es gibt noch so viel zu lernen.
    Ich freue mich schon richtig, wenn wir das im Geschichtsunterricht besprechen können. Das wird richtig aufregend.
    Hast du vielleicht Lust, nach der nächsten Stunden mit mir in die Bibliothek zu gehen? Vielleicht finden wir da ja ein Buch darüber.
    Das wird Herrn Hallmann bestimmt mega beieindrucken, wenn wir bei einem so obskuren Thema etwas vorarbeiten.
        ~ EmmaFriendship = EmmaFriendship + 1
        ~ UpdateRelashionship("Emma", EmmaFriendship)
        ~ TimeSlots = TimeSlots + 1
        ~ UseTimeSlot(TimeSlots)
        ~ AddEndYearInteraction(0)
    -> END
}


== EmmaMiniGame ==

{ EmmaFriendship < 4:
Nein, danke. Ich will ja nicht unhöflich sein, aber ich glaube... wir befinden uns auf zwei ganz anderen Lernneveaus. 
    -> END
- else:
Gerne. Wo wollen wir anfangen? 
// Minigame
    ~ EmmaFriendship = EmmaFriendship + 1
    ~ UpdateRelashionship("Emma", EmmaFriendship)
    ~ talkAlready = true
    ~ UpdateTalkAlready("John", talkAlready)
    ~ TimeSlots = TimeSlots + 2
    ~ UseTimeSlot(TimeSlots)
-> END
}
