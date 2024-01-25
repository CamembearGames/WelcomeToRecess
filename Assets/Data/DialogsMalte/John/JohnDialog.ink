//Jon Intro

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)

VAR talkAlready = false
VAR JohnFriendship = 5
VAR miniGameWin = true

{talkAlready:
    Sorry, können wir vielleicht später reden. Die nächste Runde fängt gleich an. Wir sehen uns in der nächsten Stunde, ok? 
    -else:
    ~ talkAlready = true
    ~ UpdateTalkAlready("John", talkAlready)
    -> Greeting
}


== Greeting ==
 
{ JohnFriendship:
- 1: Wirst du nicht auf der Ersatzbank vermisst?
- 2: Reicht es nicht, wenn wir in der Klasse zusammen sind?
- 3: Brauchst du etwas? Ich bin gleich dran. 
- 4: Alles gut bei dir?
- 5: Yo, was geht?
- 6: Lust auf eine Runde?
- 7: Endlich Pause, was? Boah, war Mathe krass.
- 8: Hey, komm her. Wir haben auf dich gewartet, bevor wir anfangen.
- 9: Bin ich froh, dich zu sehen. Eine Minute länger im Unterricht und ich wäre aus dem Fenster gesprungen.
- 10: Da kommt unser Starspieler! Wie geht es dir?
}
* [Sprechen] Woran denkst du gerade?
    -> JonTalking
* [Tischtennis] Wollen wir gegeneinander spielen?
    -> JonMiniGame
* [Verlassen]
    -> END

== JonTalking ==

{ JohnFriendship < 4:
Ich... komme vielleicht später darauf zurück, okay?
    -> END
- else:
Letzte Sportstunde hat uns Frau Maiglock ziemlich drangenommen, was? Bei den Völkerballrunden hat man sich wie auf 'nem Schlachtfeld gefühlt.
Und mittendrin Adrian, der von jeder Seite einen Ball abkriegt. Hat ja nicht mal versucht, dem ganzen irgendwie auszuweichen.
Junge, hat Frau Maiglock ihn danach traktiert. 
    * Hätte er vernünftig gespielt, hätte er auch keinen Ärger bekommen
    Meinst du? Ich kann mir nichts schlimmeres vorstellen, als im Sport zu verlieren und dann noch deswegen angemeckert zu werden.
    Natürlich hätte er sich etwas mehr anstrengen können, aber wirklich Spaß schien er ja nicht dabei zu haben. 
    Naja, vielleicht sorgt die Tirade ja dafür, dass er das nächste mal etwas besser spielt.
    Oder gar nicht mehr zum Sportunterricht kommt. 
     ~ JohnFriendship = JohnFriendship - 1
     ~ UpdateRelashionship("John", JohnFriendship)
     ~ UseTimeSlot(1)
    -> END
    * Frau Maiglock war schon übertrieben hart zu ihm
    Ja, oder? Voll. Ist ja schon schlimm genug, drei Volleybälle ins Gesicht zu kriegen. Der Gardinenvortrag war da echt nicht nötig. 
    Ich bin froh, dass du es genauso siehst. Manchmal habe ich das Gefühl, ich bin der einzige hier, der keine Lust hat, sich im Unterricht über andere her zu machen. 
    ~ JohnFriendship = JohnFriendship + 1
    ~ UpdateRelashionship("John", JohnFriendship)
    ~ UseTimeSlot(1)
    -> END
}


== JonMiniGame ==

{ JohnFriendship < 4:
Sorry, dieser Tisch ist bereits mit anständigen Spielern besetzt.  
   -> END
- else:
Gerne, Mann. Lass uns reinhauen 
// Minigame
~ JohnFriendship = JohnFriendship + 1
{ miniGameWin:
Glückwunsch, du hast gewonnen. Spielst ja wie der Teufel.
Hat richtig viel Spaß gemacht mit dir. Das sollten wir auf jeden Fall wiederholen. 
~ UpdateRelashionship("John", JohnFriendship)
~ UseTimeSlot(1)
-> END
    -else:
    Sieht wohl so aus, als hätte ich gewonnen. Du hast mich aber ganz schön ins Schwitzen gebracht. 
    Wie wäre es mit einer Revanche nächste Pause? Dann steckst du mich bestimmt weg. War jetzt schon richtig geil mit dir abzuhängen. 
    ~ UpdateRelashionship("John", JohnFriendship)
    ~ UseTimeSlot(1)
-> END
}
}
