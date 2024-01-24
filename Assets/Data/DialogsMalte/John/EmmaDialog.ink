//Emma Intro
VAR talkalready = false
VAR EmmaFrindship = 5
VAR minigamewin = true



{talkalready:
    Bitte störe mich nicht. Bevor es klingelt, will ich noch mit dieser Seite fertig werden. 
    -else:
    ~ talkalready = true
    -> Greeting
}


== Greeting ==
 
{ EmmaFrindship:
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
    -> EmmaTalking
* [Lernen] Wollen wir gemeinsam lernen?
    -> EmmaMiniGame
* [Verlassen]
    -> END

== EmmaTalking ==

{ EmmaFrindship < 4:
Ich verzichte. Was, wenn uns ein Lehrer sieht und der dann denkt, ich bin so deliquent wie du? Kommt gar nicht in Frage.
    -> END
- else:
Ich freue mich richtig auf dieses Schuljahr. Ich habe mir gerade den Stoff für die achte Klasse angesehen und das sind so viele gute Themen dabei. Das daltonsche Atommodell, Eireifezyklus und Säkularisation - einfach so viel.
Worauf freust du dich am meisten?
    * Das Atommodell klingt interessant
    Nicht wahr? Ich kann es kaum erwarten. Selbstverständlich habe ich mir schon die entsprechenden Buchseiten durchgelesen. 
    Frau Hasenbach wird so stolz auf mich sein. 
    -> END
    * Der Eireifezyklus könnte spannend werden
    Wirklich? Darauf freust du dich schon? 
    ...
    Ehrlich gesagt habe ich darauf am wenigsten Lust. Ich meine... das Thema interessiert mich schon wirklich. Aber wie werden die Jungen in unserer Klasse damit umgehen?
    Ich kann es schon vor mir hören. Das Lachen, die Hähme. die Witze über... du weißt schon. 
    Danke, dieses Bild werde ich heute nicht mehr aus dem Kopf bekommen. Das hat mir gerade noch gefehlt. Wenn ich deswegen den Test in den Satz setze, ist das *deine* Schuld. 
        ~ EmmaFrindship = EmmaFrindship - 1
    -> END
    * Was ist Säkularisation?
    Um ehrlich zu sein: Das habe ich mich auch gefragt. 
    Natürlich weiß ich, was Säkularisierung ist und im Wörterbuch habe ich selbstredend auch nachgeschlagen, aber es gibt noch so viel zu lernen.
    Ich freue mich schon richtig, wenn wir das im Geschichtsunterricht besprechen können. Das wird richtig aufregend.
    Hast du vielleicht Lust, nach der nächsten Stunden mit mir in die Bibliothek zu gehen? Vielleicht finden wir da ja ein Buch darüber.
    Das wird Herrn Hallmann bestimmt mega beieindrucken, wenn wir bei einem so obskuren Thema etwas vorarbeiten.
        ~ EmmaFrindship = EmmaFrindship + 1
    -> END
}


== EmmaMiniGame ==

{ EmmaFrindship < 4:
Nein, danke. Ich will ja nicht unhöflich sein, aber ich glaube... wir befinden uns auf zwei ganz anderen Lernneveaus. 
    -> END
- else:
Gerne. Wo wollen wir anfangen? 
// Minigame
~ EmmaFrindship = EmmaFrindship + 1
{ minigamewin:
Du bist ein echt schneller Lerner. Vielen Dank, das hat mir sehr geholfen.
Der nächste Test sollte keine Schwierigkeit für uns darstellen. 
Kannst du schon die lobenen Worte von Frau Hasenbach hören, wenn sie uns beiden eine Eins zurückgebiet?
-> END
    -else:
    Hier und da war zwar ein Fehler, aber alles in allem lief es doch alles gut, oder? 
Cicero hat auch schon gesagt: Errare humanum est. Jeder macht mal Fehler. 
Aber ich finde es mega, dass du immer noch dabei bist. Exercitatio artem parat. Danke, das du mit mir gelernt hast.
-> END
}
}
