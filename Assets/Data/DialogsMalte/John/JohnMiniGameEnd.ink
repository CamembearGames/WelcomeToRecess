//Jon Minigame

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL GoBackToRecess()

VAR talkAlready = false
VAR JohnFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 

-> JonMiniGame

== JonMiniGame ==

{ miniGameWin:
Glückwunsch, du hast gewonnen. Spielst ja wie der Teufel.
Hat richtig viel Spaß gemacht mit dir. Das sollten wir auf jeden Fall wiederholen.
~ GoBackToRecess()
-> END
    -else:
    Sieht wohl so aus, als hätte ich gewonnen. Du hast mich aber ganz schön ins Schwitzen gebracht. 
    Wie wäre es mit einer Revanche nächste Pause? Dann steckst du mich bestimmt weg. War jetzt schon richtig geil mit dir abzuhängen.
~ GoBackToRecess()
-> END
}
