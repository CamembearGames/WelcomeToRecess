//Emma Minigame

EXTERNAL UpdateRelashionship(name, value)
EXTERNAL UpdateTalkAlready(name, value)
EXTERNAL UseTimeSlot(numberOfTimeSlots)
EXTERNAL StartMiniGame(miniGameNumber)
EXTERNAL GoBackToRecess()

VAR talkAlready = false
VAR EmmaFriendship = 5
VAR miniGameWin = true
VAR TimeSlots = 0 

-> EmmaMiniGame

== EmmaMiniGame ==

{ miniGameWin:
Du bist ein echt schneller Lerner. Vielen Dank, das hat mir sehr geholfen.
Der nächste Test sollte keine Schwierigkeit für uns darstellen. 
Kannst du schon die lobenen Worte von Frau Hasenbach hören, wenn sie uns beiden eine Eins zurückgebiet?
~ GoBackToRecess()
-> END
    -else:
    Hier und da war zwar ein Fehler, aber alles in allem lief es doch alles gut, oder? 
Cicero hat auch schon gesagt: Errare humanum est. Jeder macht mal Fehler. 
Aber ich finde es mega, dass du immer noch dabei bist. Exercitatio artem parat. Danke, das du mit mir gelernt hast.
~ GoBackToRecess()
-> END
}
