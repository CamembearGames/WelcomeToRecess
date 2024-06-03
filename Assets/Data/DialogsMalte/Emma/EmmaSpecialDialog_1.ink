//Emma Intro
EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL ChangeRelashionship(name, amount)
EXTERNAL AddEndYearInteraction(interactionnumber)
EXTERNAL WateringAcknowledge()
EXTERNAL AkemDefended()

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
* [Sprechen] Woran denkst du gerade? #Player
    ~ talkAlready = true
    ~ UpdateTalkAlready("Emma", talkAlready)
    -> EmmaTalking
* [Verlassen]
    -> END

== EmmaTalking ==

{ EmmaFriendship < 4:
Ich verzichte. Was, wenn uns ein Lehrer sieht und der dann denkt, ich bin so deliquent wie du? Kommt gar nicht in Frage.
    -> END
- else: 
Hast du schon unseren neuen Mitschüler gesehen? Akem, glaube ich, heißt er. 
Frau Hasenbach hat gesagt, dass wir alle sehr nett und hilfsbereit zu ihm sein sollen. Das habe ich auch vor.
Ich wollte ihn gerade etwas herumführen, aber er wollte scheinbar nicht. 
Er hat irgendwas gesagt, aber durch seine echt mieseable Grammatik und Intonation konnte ich leider nichts verstehen.
Außerdem...
Bitte sag das nicht weiter - ich will echt nicht gemein sein!
Aber er hat heute morgen irgendeine Art Fladen gegessen, der echt bestialisch gestunken hat. Ich musste aus den Schulflur raus an die frische Luft. 
So schlimm war das.
Und das ist noch nicht alles! Er hatte nicht mal eine Brotdose dabei, sondern hatte das einfach in eine Plastikfolie und Alupapier eingewickelt.
Das ist schlecht für die Umwelt! Das lernt man doch in der ersten Klasse!
    * [Emma zustimmen]
    Danke. Ich dachte schon, ich werde verrückt. 
    Natürlich weiß ich andere Kulturen sehr wertzuschätzen, aber der Umweltschutz geht selbstredend vor. 
    Auch ist es sehr unverschämt gegenüber den anderen Schülern, so die Luft zu verpesten. Das stört beim Lernen und Konzentrieren. Ich werde mal mit einem Lehrer darüber sprechen.
        ~ EmmaFriendship = EmmaFriendship + 1
        ~ UpdateRelashionship("Emma", EmmaFriendship) 
        ~ TimeSlots = TimeSlots + 1
        ~ UseTimeSlot(TimeSlots)
    -> END
    * [Für Akem Partei ergreifen]
    Hey! Nur damit das klar ist, ich habe nichts gegen Akem.
    Ich mag neue Schüler. Immer. Die Lehrer haben gesagt, wir sollen nichts Böses über sie sagen und das habe ich auch nicht!
    Ich wollte nur...!
    Ach, vergiss es. Du verstehst das doch eh nicht.  
        ~ EmmaFriendship = EmmaFriendship - 1
        ~ UpdateRelashionship("Emma", EmmaFriendship)
        ~ AkemDefended()
        ~ TimeSlots = TimeSlots + 1
        ~ UseTimeSlot(TimeSlots)
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
    ~ StartMiniGame(0)
-> END
}
